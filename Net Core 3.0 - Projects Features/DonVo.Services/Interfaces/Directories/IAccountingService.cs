using DonVo.Services.ActualResults;
using DonVo.ViewModels.DTOs.Directories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.Services.Interfaces.Directories
{
    public interface IAccountingService : IDisposable, IDirectory<AccountingDTO>
    {
        Task<ActualResult<AccountingDTO>> GetAsync(int accountKey);
        Task<ActualResult<IEnumerable<AccountingDTO>>> GetAccounting(int accountKey);

        Task<ActualResult<string>> CreateMainAccountingAsync(CreateAccountingDTO dto);

        Task<ActualResult> DeleteAsync(int accountKey);
    }
}
