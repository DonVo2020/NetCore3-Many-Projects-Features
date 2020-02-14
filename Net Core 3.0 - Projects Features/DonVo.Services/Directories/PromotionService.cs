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
    public class PromotionService : IPromotionService
    {
        private readonly ContosoRetailDWContext _context;
        private readonly IMapper _mapper;

        public PromotionService(ContosoRetailDWContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<PromotionDTO>>> GetAllAsync()
        {
            try
            {
                var promotions = await _context.DimPromotion.Where(z => !string.IsNullOrWhiteSpace(z.PromotionName)).OrderBy(x => x.PromotionLabel).ToListAsync();
                var result = _mapper.Map<IEnumerable<PromotionDTO>>(promotions);
                return new ActualResult<IEnumerable<PromotionDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<PromotionDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<PromotionDTO>> GetAsync(int promotionKey)
        {
            try
            {
                var promotion = await _context.DimPromotion.FindAsync(promotionKey);
                if (promotion == null)
                {
                    return new ActualResult<PromotionDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<PromotionDTO>(promotion);
                return new ActualResult<PromotionDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<PromotionDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<IEnumerable<PromotionDTO>>> GetPromotion(int promotionKey)
        {
            try
            {
                var promotion = await _context.DimPromotion.Where(x => x.PromotionKey == promotionKey).OrderBy(x => x.PromotionName).ToListAsync();
                var result = _mapper.Map<IEnumerable<PromotionDTO>>(promotion);
                return new ActualResult<IEnumerable<PromotionDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<PromotionDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateMainPromotionAsync(CreatePromotionDTO dto)
        {
            try
            {
                var promotion = _mapper.Map<DimPromotion>(dto);
                await _context.DimPromotion.AddAsync(promotion);
                await _context.SaveChangesAsync();
                return new ActualResult<string> { Result = promotion.PromotionKey.ToString() };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> DeleteAsync(int promotionKey)
        {
            try
            {
                var result = await _context.DimPromotion.FindAsync(promotionKey);
                if (result != null)
                {
                    _context.DimPromotion.Remove(result);
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
