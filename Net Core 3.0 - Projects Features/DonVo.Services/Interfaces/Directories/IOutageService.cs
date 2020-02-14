using DonVo.Services.ActualResults;
using DonVo.ViewModels.DTOs.Directories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.Services.Interfaces.Directories
{
    public interface IOutageService : IDisposable, IDirectory<OutageDTO>
    {
        Task<ActualResult<OutageDTO>> GetAsync(int outageKey);
        Task<ActualResult<IEnumerable<OutageDTO>>> GetOutage(int outageKey);

        Task<ActualResult<string>> CreateMainOutageAsync(CreateOutageDTO dto);

        Task<ActualResult> DeleteAsync(int outageKey);
    }
}
