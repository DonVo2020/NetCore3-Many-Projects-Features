using DonVo.Services.ActualResults;
using DonVo.ViewModels.DTOs.Directories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.Services.Interfaces.Directories
{
    public interface ICurrencyService : IDisposable, IDirectory<CurrencyDTO>
    {
        Task<ActualResult<CurrencyDTO>> GetAsync(int currencyKey);
        Task<ActualResult<IEnumerable<CurrencyDTO>>> GetCurrency(int currencyKey);

        Task<ActualResult<string>> CreateMainCurrencyAsync(CreateCurrencyDTO dto);
        Task<ActualResult> UpdateAsync(CreateCurrencyDTO dto);

        Task<ActualResult> DeleteAsync(int currencyKey);
    }
}
