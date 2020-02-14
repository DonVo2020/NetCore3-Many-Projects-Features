using DonVo.Services.ActualResults;
using DonVo.ViewModels.DTOs.Directories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.Services.Interfaces.Directories
{
    public interface IProductService : IDisposable, IDirectory<ProductDTO>
    {
        Task<ActualResult<ProductDTO>> GetAsync(int productKey);
        Task<ActualResult<IEnumerable<ProductDTO>>> GetProduct(int productKey);
    }
}
