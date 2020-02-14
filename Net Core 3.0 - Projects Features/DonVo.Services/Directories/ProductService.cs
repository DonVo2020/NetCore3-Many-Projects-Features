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
    public class ProductService : IProductService
    {
        private readonly ContosoRetailDWContext _context;
        private readonly IMapper _mapper;

        public ProductService(ContosoRetailDWContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<ProductDTO>>> GetAllAsync()
        {
            try
            {
                var products = await _context.DimProduct.Where(z => !string.IsNullOrWhiteSpace(z.ProductName)).OrderBy(x => x.ProductName).ToListAsync();
                var result = _mapper.Map<IEnumerable<ProductDTO>>(products);
                return new ActualResult<IEnumerable<ProductDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<ProductDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<ProductDTO>> GetAsync(int productKey)
        {
            try
            {
                var product = await _context.DimProduct.FindAsync(productKey);
                if (product == null)
                {
                    return new ActualResult<ProductDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<ProductDTO>(product);
                return new ActualResult<ProductDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<ProductDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
            throw new NotImplementedException();
        }

        public async Task<ActualResult<IEnumerable<ProductDTO>>> GetProduct(int productKey)
        {
            try
            {
                var product = await _context.DimProduct.Where(x => x.ProductKey == productKey).OrderBy(x => x.ProductName).ToListAsync();
                var result = _mapper.Map<IEnumerable<ProductDTO>>(product);
                return new ActualResult<IEnumerable<ProductDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<ProductDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
