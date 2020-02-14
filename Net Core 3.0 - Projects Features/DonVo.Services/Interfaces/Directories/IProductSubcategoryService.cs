using DonVo.Services.ActualResults;
using DonVo.ViewModels.DTOs.Directories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.Services.Interfaces.Directories
{
    public interface IProductSubcategoryService : IDisposable, IDirectory<ProductSubcategoryDTO>
    {
        Task<ActualResult<ProductSubcategoryDTO>> GetAsync(int productSubcategoryKey);
        Task<ActualResult<IEnumerable<ProductSubcategoryDTO>>> GetProductCategory(int productSubcategoryKey);
        Task<ActualResult<string>> CreateMainProductSubcategoryAsync(CreateProductSubcategoryDTO dto);
        Task<ActualResult> UpdateProductSubcategoryAsync(UpdateProductSubcategoryDTO dto);

        Task<ActualResult> DeleteAsync(int productSubcategoryKey);
    }
}
