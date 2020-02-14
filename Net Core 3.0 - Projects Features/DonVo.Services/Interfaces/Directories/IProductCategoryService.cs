using DonVo.Services.ActualResults;
using DonVo.ViewModels.DTOs.Directories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.Services.Interfaces.Directories
{
    public interface IProductCategoryService : IDisposable, IDirectory<ProductCategoryDTO>
    {
        Task<ActualResult<ProductCategoryDTO>> GetAsync(int productCategoryKey);
        Task<ActualResult<IEnumerable<ProductCategoryDTO>>> GetProductCategory(int productCategoryKey);
    }
}
