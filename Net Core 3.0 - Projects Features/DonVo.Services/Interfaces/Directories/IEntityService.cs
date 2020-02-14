using DonVo.Services.ActualResults;
using DonVo.ViewModels.DTOs.Directories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.Services.Interfaces.Directories
{
    public interface IEntityService : IDisposable, IDirectory<EntityDTO>
    {
        Task<ActualResult<EntityDTO>> GetAsync(int entityKey);
        Task<ActualResult<IEnumerable<EntityDTO>>> GetEntity(int entityKey);

        Task<ActualResult<string>> CreateMainEntityAsync(CreateEntityDTO dto);

        Task<ActualResult> DeleteAsync(int entityKey);
    }
}
