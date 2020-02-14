using AutoMapper;
using DonVo.Persistences;
using DonVo.Persistences.Models;
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
    public class ProductSubcategoryService : IProductSubcategoryService
    {
        private readonly ContosoRetailDWContext _context;
        private readonly IMapper _mapper;

        public ProductSubcategoryService(ContosoRetailDWContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<ProductSubcategoryDTO>>> GetAllAsync()
        {
            try
            {
                var productSubcategories = await _context.DimProductSubcategory
                                                 .Join(
                                                   _context.DimProductCategory,
                                                   dimProductSubcategory => dimProductSubcategory.ProductCategoryKey,
                                                   dimeProductCategory => dimeProductCategory.ProductCategoryKey,
                                                   (dimProductSubcategory, dimeProductCategory) => new { DimProductSubcategory = dimProductSubcategory, DimProductCategory = dimeProductCategory })
                                                .Where(z => !string.IsNullOrWhiteSpace(z.DimProductSubcategory.ProductSubcategoryName))
                                                .OrderBy(x => x.DimProductSubcategory.ProductSubcategoryName)
                                                 .Select(c => new ProductSubcategoryDTO
                                                 {
                                                    ProductSubcategoryKey = c.DimProductSubcategory.ProductSubcategoryKey,
                                                    ProductSubcategoryName = c.DimProductSubcategory.ProductSubcategoryName,
                                                    ProductSubcategoryDescription = c.DimProductSubcategory.ProductSubcategoryDescription,
                                                    ProductSubcategoryLabel = c.DimProductSubcategory.ProductSubcategoryLabel,
                                                    ProductCategoryKey = c.DimProductSubcategory.ProductCategoryKey,
                                                    ProductCategoryName = c.DimProductCategory.ProductCategoryName

                                                 }).ToListAsync();
                //var result = _mapper.Map<IEnumerable<ProductSubcategoryDTO>>(productSubcategories);
                return new ActualResult<IEnumerable<ProductSubcategoryDTO>> { Result = productSubcategories };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<ProductSubcategoryDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<ProductSubcategoryDTO>> GetAsync(int productKey)
        {
            try
            {
                var productSubcategory = await _context.DimProductSubcategory.FindAsync(productKey);
                if (productSubcategory == null)
                {
                    return new ActualResult<ProductSubcategoryDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<ProductSubcategoryDTO>(productSubcategory);
                return new ActualResult<ProductSubcategoryDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<ProductSubcategoryDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
            throw new NotImplementedException();
        }

        public async Task<ActualResult<IEnumerable<ProductSubcategoryDTO>>> GetProductCategory(int productSubcategoryKey)
        {
            try
            {
                var productSubcategory = await _context.DimProductSubcategory.Where(x => x.ProductSubcategoryKey == productSubcategoryKey).OrderBy(x => x.ProductSubcategoryName).ToListAsync();
                var result = _mapper.Map<IEnumerable<ProductSubcategoryDTO>>(productSubcategory);
                return new ActualResult<IEnumerable<ProductSubcategoryDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<ProductSubcategoryDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateMainProductSubcategoryAsync(CreateProductSubcategoryDTO dto)
        {
            try
            {
                var productSubcategory = _mapper.Map<DimProductSubcategory>(dto);
                var currentDateTime = DateTime.Now;
                productSubcategory.LoadDate = currentDateTime;
                productSubcategory.UpdateDate = currentDateTime;
                productSubcategory.EtlloadId = 1;
                await _context.DimProductSubcategory.AddAsync(productSubcategory);
                await _context.SaveChangesAsync();
                return new ActualResult<string> { Result = productSubcategory.ProductSubcategoryKey.ToString() };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateProductSubcategoryAsync(UpdateProductSubcategoryDTO dto)
        {
            try
            {
                var mapping = _mapper.Map<DimProductSubcategory>(dto);
                _context.Entry(mapping).State = EntityState.Deleted;
                _context.Entry(mapping).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> DeleteAsync(int productSubcategoryKey)
        {
            try
            {
                var result = await _context.DimProductSubcategory.FindAsync(productSubcategoryKey);
                if (result != null)
                {
                    _context.DimProductSubcategory.Remove(result);
                    await _context.SaveChangesAsync();
                }
                return new ActualResult();
            }
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
