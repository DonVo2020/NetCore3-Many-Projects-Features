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
    public class StoreService : IStoreService
    {
        private readonly ContosoRetailDWContext _context;
        private readonly IMapper _mapper;

        public StoreService(ContosoRetailDWContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<StoreDTO>>> GetAllAsync()
        {
            try
            {
                var stores = await _context.DimStore.Where(z => !string.IsNullOrWhiteSpace(z.StoreName)).OrderBy(x => x.StoreName).ToListAsync();
                var result = _mapper.Map<IEnumerable<StoreDTO>>(stores);
                return new ActualResult<IEnumerable<StoreDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<StoreDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<StoreDTO>> GetAsync(int storeKey)
        {
            try
            {
                var store = await _context.DimStore.FindAsync(storeKey);
                if (store == null)
                {
                    return new ActualResult<StoreDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<StoreDTO>(store);
                return new ActualResult<StoreDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<StoreDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<IEnumerable<StoreDTO>>> GetStore(int storeKey)
        {
            try
            {
                var store = await _context.DimStore.Where(x => x.StoreKey == storeKey).OrderBy(x => x.StoreName).ToListAsync();
                var result = _mapper.Map<IEnumerable<StoreDTO>>(store);
                return new ActualResult<IEnumerable<StoreDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<StoreDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateMainStoreAsync(CreateStoreDTO dto)
        {
            try
            {
                var store = _mapper.Map<DimStore>(dto);
                await _context.DimStore.AddAsync(store);
                await _context.SaveChangesAsync();
                return new ActualResult<string> { Result = store.StoreKey.ToString() };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> DeleteAsync(int storeKey)
        {
            try
            {
                var result = await _context.DimStore.FindAsync(storeKey);
                if (result != null)
                {
                    _context.DimStore.Remove(result);
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
