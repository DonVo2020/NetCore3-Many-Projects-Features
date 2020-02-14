using DonVo.Services.ActualResults;
using DonVo.ViewModels.DTOs.Directories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.Services.Interfaces.Directories
{
    public interface IStoreService : IDisposable, IDirectory<StoreDTO>
    {
        Task<ActualResult<StoreDTO>> GetAsync(int storeKey);
        Task<ActualResult<IEnumerable<StoreDTO>>> GetStore(int storeKey);

        Task<ActualResult<string>> CreateMainStoreAsync(CreateStoreDTO dto);

        Task<ActualResult> DeleteAsync(int storeKey);
    }
}
