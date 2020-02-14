using AutoMapper;
using DonVo.Persistences;
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
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly ContosoRetailDWContext _context;
        private readonly IMapper _mapper;

        public ProductCategoryService(ContosoRetailDWContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<ProductCategoryDTO>>> GetAllAsync()
        {
            try
            {
                var productCategories = await _context.DimProductCategory.Where(z => !string.IsNullOrWhiteSpace(z.ProductCategoryName)).OrderBy(x => x.ProductCategoryName).ToListAsync();
                var result = _mapper.Map<IEnumerable<ProductCategoryDTO>>(productCategories);
                return new ActualResult<IEnumerable<ProductCategoryDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<ProductCategoryDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<ProductCategoryDTO>> GetAsync(int productKey)
        {
            try
            {
                var productCategory = await _context.DimProductCategory.FindAsync(productKey);
                if (productCategory == null)
                {
                    return new ActualResult<ProductCategoryDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<ProductCategoryDTO>(productCategory);
                return new ActualResult<ProductCategoryDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<ProductCategoryDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
            throw new NotImplementedException();
        }

        public async Task<ActualResult<IEnumerable<ProductCategoryDTO>>> GetProductCategory(int productCategoryKey)
        {
            try
            {
                var productCategory = await _context.DimProductCategory.Where(x => x.ProductCategoryKey == productCategoryKey).OrderBy(x => x.ProductCategoryName).ToListAsync();
                var result = _mapper.Map<IEnumerable<ProductCategoryDTO>>(productCategory);
                return new ActualResult<IEnumerable<ProductCategoryDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<ProductCategoryDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
