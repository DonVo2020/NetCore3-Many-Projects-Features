using AutoMapper;
using DonVo.IdentityJwtBearer.EF;
using DonVo.IdentityJwtBearer.Entities;
using DonVo.Services.ActualResults;
using DonVo.Services.Enums;
using DonVo.Services.Helpers;
using DonVo.Services.Interfaces.Account;
using DonVo.ViewModels.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonVo.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly DonVoIdentityContext _context;
        private readonly IMapper _mapper;

        public AccountService(UserManager<User> userManager,
                              RoleManager<IdentityRole> roleManager,
                              SignInManager<User> signInManager,
                              DonVoIdentityContext context,
                              IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _context = context;
            _mapper = mapper;
        }

        public async Task<AccountDTO> SignIn(string email, string password, SignInType type)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                var isAuthorize = type switch
                {
                    SignInType.Credentials => await _userManager.CheckPasswordAsync(user, password),
                    SignInType.RefreshToken => user != null,
                    _ => throw new ArgumentOutOfRangeException(nameof(type), type, null),
                };
                if (isAuthorize)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    return new AccountDTO { Email = user.Email, Role = roles.First() };
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> SignIn(string email, string password, bool rememberMe)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, false);
                return result.Succeeded;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task SignOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<ActualResult<IEnumerable<AccountDTO>>> GetAllAccountsAsync()
        {
            try
            {
                var users = await _userManager.Users.ToListAsync();
                var mapping = _mapper.Map<IEnumerable<AccountDTO>>(users);
                return new ActualResult<IEnumerable<AccountDTO>> { Result = mapping };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<AccountDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<AccountDTO>> GetAccountAsync(string hashId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(hashId);
                if (user != null)
                {
                    var mapping = _mapper.Map<AccountDTO>(user);
                    return new ActualResult<AccountDTO> { Result = mapping };
                }
                return new ActualResult<AccountDTO>(Errors.UserNotFound);
            }
            catch (Exception exception)
            {
                return new ActualResult<AccountDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<AccountDTO>> GetAccountRoleAsync(string hashId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(hashId);
                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var result = new AccountDTO { HashId = hashId, Role = roles.FirstOrDefault() };
                    return new ActualResult<AccountDTO> { Result = result };
                }
                return new ActualResult<AccountDTO>(Errors.UserNotFound);
            }
            catch (Exception exception)
            {
                return new ActualResult<AccountDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<IEnumerable<RolesDTO>>> GetRoles()
        {
            try
            {
                var roles = await _roleManager.Roles.ToListAsync();
                var result = _mapper.Map<IEnumerable<RolesDTO>>(roles);
                return new ActualResult<IEnumerable<RolesDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<RolesDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<int?> GetClientRefreshTokenLifeTime(string clientType)
        {
            try
            {
                int id = 1; // Default:  clientType = "WEB-APPLICATION"
                if (clientType == "DESKTOP - APPLICATION")
                    id = 2;
                else if (clientType == "MOBILE-APPLICATION")
                    id = 3;

                var client = await _context.Clients.FindAsync(id);
                return client?.RefreshTokenLifeTime;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<ProtectedTicketDTO> GetProtectedTicket(string refreshTokenId)
        {
            try
            {
                var refreshToken = await _context.RefreshTokens.FindAsync(refreshTokenId);
                if (refreshToken != null)
                {
                    if (DateTime.Now < refreshToken.ExpiresUtc)
                    {
                        return new ProtectedTicketDTO
                        {
                            ClientType = refreshToken.ClientId,
                            Email = refreshToken.Subject
                        };
                    }
                    _context.RefreshTokens.Remove(refreshToken);
                    await _context.SaveChangesAsync();
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ActualResult> CreateAsync(CreateAccountDTO dto)
        {
            try
            {
                if (!await CheckEmailAsync(dto.Email))
                {
                    var user = new User
                    {
                        Email = dto.Email,
                        UserName = dto.Email,
                        FirstName = dto.FirstName,
                        LastName = dto.LastName,
                        Patronymic = dto.Patronymic
                    };
                    var result = await _userManager.CreateAsync(user, dto.Password);
                    if (result.Succeeded)
                    {
                        return await UpdateUserRoleAsync(user, dto.Role);
                    }
                    return new ActualResult(Errors.DataBaseError);
                }
                return new ActualResult(Errors.DuplicateEmail);
            }
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task CreateRefreshToken(RefreshTokenDTO dto)
        {
            try
            {
                var existingToken = await _context.RefreshTokens.SingleOrDefaultAsync(x => x.Subject == dto.Subject && x.ClientId == dto.ClientType);
                if (existingToken != null)
                {
                    _context.RefreshTokens.Remove(existingToken);
                    await _context.SaveChangesAsync();
                }
                await _context.RefreshTokens.AddAsync(_mapper.Map<RefreshToken>(dto));
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public async Task<ActualResult> UpdatePersonalDataAsync(AccountDTO dto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(dto.HashId);
                if (user != null)
                {
                    user.FirstName = dto.FirstName;
                    user.LastName = dto.LastName;
                    user.Patronymic = dto.Patronymic;
                    var result = await _userManager.UpdateAsync(user);
                    return result.Succeeded ? new ActualResult() : new ActualResult(Errors.DataBaseError);
                }
                return new ActualResult(Errors.UserNotFound);
            }
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateEmailAsync(AccountDTO dto)
        {
            try
            {
                if (!await CheckEmailAsync(dto.Email))
                {
                    var user = await _userManager.FindByIdAsync(dto.HashId);
                    if (user != null)
                    {
                        user.Email = dto.Email;
                        user.UserName = dto.Email;
                        var result = await _userManager.UpdateAsync(user);
                        return result.Succeeded ? new ActualResult() : new ActualResult(Errors.DataBaseError);
                    }
                    return new ActualResult(Errors.UserNotFound);
                }
                return new ActualResult(Errors.DuplicateEmail);
            }
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdatePasswordAsync(UpdateAccountPasswordDTO dto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(dto.HashId);
                if (user != null)
                {
                    var result = await _userManager.ChangePasswordAsync(user, dto.OldPassword, dto.Password);
                    return result.Succeeded ? new ActualResult() : new ActualResult(Errors.DataBaseError);
                }
                return new ActualResult(Errors.UserNotFound);
            }
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateRoleAsync(AccountDTO dto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(dto.HashId);
                if (user != null)
                {
                    return await UpdateUserRoleAsync(user, dto.Role);
                }
                return new ActualResult(Errors.UserNotFound);
            }
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        private async Task<ActualResult> UpdateUserRoleAsync(User user, string role)
        {
            try
            {
                var roles = new List<string> { role };
                var userRoles = await _userManager.GetRolesAsync(user);
                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);
                var addToRole = await _userManager.AddToRolesAsync(user, addedRoles);
                var removeFromRole = await _userManager.RemoveFromRolesAsync(user, removedRoles);
                if (addToRole.Succeeded && removeFromRole.Succeeded)
                {
                    return new ActualResult();
                }
                return new ActualResult(Errors.DataBaseError);
            }
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(hashId);
                if (user != null)
                {
                    var result = await _userManager.DeleteAsync(user);
                    return result.Succeeded ? new ActualResult() : new ActualResult(Errors.DataBaseError);
                }
                return new ActualResult(Errors.UserNotFound);
            }
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<bool> CheckEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null;
        }

        public void Dispose()
        {
            _userManager.Dispose();
            _roleManager.Dispose();
        }
    }
}
