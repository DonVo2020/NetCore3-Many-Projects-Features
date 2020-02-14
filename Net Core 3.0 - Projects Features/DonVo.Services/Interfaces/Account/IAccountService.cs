﻿using DonVo.Services.ActualResults;
using DonVo.Services.Enums;
using DonVo.ViewModels.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.Services.Interfaces.Account
{
    public interface IAccountService : IDisposable
    {
        /// <summary>
        /// The method for the authorization API, if the authorization was successful, returns the DTO otherwise null
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="type"></param>
        Task<AccountDTO> SignIn(string email, string password, SignInType type);
        /// <summary>
        /// The method for the authorization MVC, if the authorization was successful, returns true else false
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="rememberMe"></param>
        Task<bool> SignIn(string email, string password, bool rememberMe);
        Task SignOut();

        Task<ActualResult<IEnumerable<AccountDTO>>> GetAllAccountsAsync();
        Task<ActualResult<AccountDTO>> GetAccountAsync(string hashId);
        Task<ActualResult<AccountDTO>> GetAccountRoleAsync(string hashId);
        Task<ActualResult<IEnumerable<RolesDTO>>> GetRoles();
        Task<int?> GetClientRefreshTokenLifeTime(string clientType);
        Task<ProtectedTicketDTO> GetProtectedTicket(string refreshTokenId);

        Task<ActualResult> CreateAsync(CreateAccountDTO dto);
        Task CreateRefreshToken(RefreshTokenDTO dto);
        Task<ActualResult> UpdatePersonalDataAsync(AccountDTO dto);
        Task<ActualResult> UpdateEmailAsync(AccountDTO dto);
        Task<ActualResult> UpdatePasswordAsync(UpdateAccountPasswordDTO dto);
        Task<ActualResult> UpdateRoleAsync(AccountDTO dto);
        Task<ActualResult> DeleteAsync(string hashId);

        Task<bool> CheckEmailAsync(string email);
    }
}