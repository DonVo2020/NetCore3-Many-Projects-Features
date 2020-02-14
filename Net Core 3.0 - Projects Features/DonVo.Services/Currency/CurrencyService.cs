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
    public class CurrencyService : ICurrencyService
    {
        private readonly ContosoRetailDWContext _context;
        private readonly IMapper _mapper;

        public CurrencyService(ContosoRetailDWContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<CurrencyDTO>>> GetAllAsync()
        {
            try
            {
                var currencies = await _context.DimCurrency.Where(z => !string.IsNullOrWhiteSpace(z.CurrencyName)).OrderBy(x => x.CurrencyName).ToListAsync();
                var result = _mapper.Map<IEnumerable<CurrencyDTO>>(currencies);
                return new ActualResult<IEnumerable<CurrencyDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<CurrencyDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<CurrencyDTO>> GetAsync(int currencyKey)
        {
            try
            {
                var currency = await _context.DimCurrency.FindAsync(currencyKey);
                if (currency == null)
                {
                    return new ActualResult<CurrencyDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<CurrencyDTO>(currency);
                return new ActualResult<CurrencyDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<CurrencyDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<IEnumerable<CurrencyDTO>>> GetCurrency(int currencyKey)
        {
            try
            {
                var currency = await _context.DimCurrency.Where(x => x.CurrencyKey == currencyKey).OrderBy(x => x.CurrencyName).ToListAsync();
                var result = _mapper.Map<IEnumerable<CurrencyDTO>>(currency);
                return new ActualResult<IEnumerable<CurrencyDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<CurrencyDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateMainCurrencyAsync(CreateCurrencyDTO dto)
        {
            try
            {
                var currency = _mapper.Map<DimCurrency>(dto);
                var currentDateTime = DateTime.Now;
                currency.LoadDate = currentDateTime;
                currency.UpdateDate = currentDateTime;
                currency.EtlloadId = 1;
                await _context.DimCurrency.AddAsync(currency);
                await _context.SaveChangesAsync();
                return new ActualResult<string> { Result = currency.CurrencyKey.ToString() };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateAsync(CreateCurrencyDTO dto)
        {
            try
            {
                var mapping = _mapper.Map<DimCurrency>(dto);

                mapping.UpdateDate = DateTime.Now;
                _context.Entry(mapping).State = EntityState.Modified;
                _context.Entry(mapping).Property(x => x.CurrencyKey).IsModified = false;
                _context.Entry(mapping).Property(x => x.EtlloadId).IsModified = false;
                _context.Entry(mapping).Property(x => x.LoadDate).IsModified = false;
                await _context.SaveChangesAsync();

                return new ActualResult();
            }
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> DeleteAsync(int currencyKey)
        {
            try
            {
                var result = await _context.DimCurrency.FindAsync(currencyKey);
                if (result != null)
                {
                    _context.DimCurrency.Remove(result);
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
