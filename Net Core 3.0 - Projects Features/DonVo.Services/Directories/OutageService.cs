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
    public class OutageService : IOutageService
    {
        private readonly ContosoRetailDWContext _context;
        private readonly IMapper _mapper;

        public OutageService(ContosoRetailDWContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<OutageDTO>>> GetAllAsync()
        {
            try
            {
                var outages = await _context.DimOutage.Where(z => !string.IsNullOrWhiteSpace(z.OutageName)).OrderBy(x => x.OutageLabel).ToListAsync();
                var result = _mapper.Map<IEnumerable<OutageDTO>>(outages);
                return new ActualResult<IEnumerable<OutageDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<OutageDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<OutageDTO>> GetAsync(int outageKey)
        {
            try
            {
                var outage = await _context.DimOutage.FindAsync(outageKey);
                if (outage == null)
                {
                    return new ActualResult<OutageDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<OutageDTO>(outage);
                return new ActualResult<OutageDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<OutageDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<IEnumerable<OutageDTO>>> GetOutage(int outageKey)
        {
            try
            {
                var outage = await _context.DimOutage.Where(x => x.OutageKey == outageKey).OrderBy(x => x.OutageLabel).ToListAsync();
                var result = _mapper.Map<IEnumerable<OutageDTO>>(outage);
                return new ActualResult<IEnumerable<OutageDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<OutageDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateMainOutageAsync(CreateOutageDTO dto)
        {
            try
            {
                var outage = _mapper.Map<DimOutage>(dto);
                await _context.DimOutage.AddAsync(outage);
                await _context.SaveChangesAsync();
                return new ActualResult<string> { Result = outage.OutageKey.ToString() };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> DeleteAsync(int outageKey)
        {
            try
            {
                var result = await _context.DimOutage.FindAsync(outageKey);
                if (result != null)
                {
                    _context.DimOutage.Remove(result);
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
