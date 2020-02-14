using AutoMapper;
using DonVo.Persistences;
using DonVo.Persistences.Models;
using DonVo.Services.ActualResults;
using DonVo.Services.Enums;
using DonVo.Services.Helpers;
using DonVo.Services.Interfaces.Directories;
using DonVo.ViewModels.DTOs.Directories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonVo.Services.Directories
{
    public class AccountingService : IAccountingService
    {
        private readonly ContosoRetailDWContext _context;
        private readonly IMapper _mapper;

        public AccountingService(ContosoRetailDWContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<AccountingDTO>>> GetAllAsync()
        {
            try
            {
                var accountings = await _context.DimAccount.Where(z => !string.IsNullOrWhiteSpace(z.AccountName)).OrderBy(x => x.AccountName).ToListAsync();
                var result = _mapper.Map<IEnumerable<AccountingDTO>>(accountings);
                return new ActualResult<IEnumerable<AccountingDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<AccountingDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<AccountingDTO>> GetAsync(int acccountKey)
        {
            try
            {
                var accounting = await _context.DimAccount.FindAsync(acccountKey);
                if (accounting == null)
                {
                    return new ActualResult<AccountingDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<AccountingDTO>(accounting);
                return new ActualResult<AccountingDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<AccountingDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<IEnumerable<AccountingDTO>>> GetAccounting(int accountKey)
        {
            try
            {
                var accounting = await _context.DimAccount.Where(x => x.AccountKey == accountKey).OrderBy(x => x.AccountName).ToListAsync();
                var result = _mapper.Map<IEnumerable<AccountingDTO>>(accounting);
                return new ActualResult<IEnumerable<AccountingDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<AccountingDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateMainAccountingAsync(CreateAccountingDTO dto)
        {
            try
            {
                var accounting = _mapper.Map<DimAccount>(dto);
                await _context.DimAccount.AddAsync(accounting);
                await _context.SaveChangesAsync();
                //var hashId = HashHelper.EncryptLong(accounting.AccountKey);
                return new ActualResult<string> { Result = accounting.AccountKey.ToString() };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> DeleteAsync(int accountKey)
        {
            try
            {
                var result = await _context.DimAccount.FindAsync(accountKey);
                if (result != null)
                {
                    _context.DimAccount.Remove(result);
                    await _context.SaveChangesAsync();
                }
                return new ActualResult();
            }
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
