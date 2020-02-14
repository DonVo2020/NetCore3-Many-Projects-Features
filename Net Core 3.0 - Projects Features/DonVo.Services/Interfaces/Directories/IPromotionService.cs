using DonVo.Services.ActualResults;
using DonVo.ViewModels.DTOs.Directories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.Services.Interfaces.Directories
{
    public interface IPromotionService : IDisposable, IDirectory<PromotionDTO>
    {
        Task<ActualResult<PromotionDTO>> GetAsync(int promotionKey);
        Task<ActualResult<IEnumerable<PromotionDTO>>> GetPromotion(int promotionKey);

        Task<ActualResult<string>> CreateMainPromotionAsync(CreatePromotionDTO dto);

        Task<ActualResult> DeleteAsync(int promotionKey);
    }
}
