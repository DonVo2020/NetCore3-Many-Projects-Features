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
    public class ScenarioService : IScenarioService
    {
        private readonly ContosoRetailDWContext _context;
        private readonly IMapper _mapper;

        public ScenarioService(ContosoRetailDWContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<ScenarioDTO>>> GetAllAsync()
        {
            try
            {
                var scenarios = await _context.DimScenario.Where(z => !string.IsNullOrWhiteSpace(z.ScenarioName)).OrderBy(x => x.ScenarioName).ToListAsync();
                var result = _mapper.Map<IEnumerable<ScenarioDTO>>(scenarios);
                return new ActualResult<IEnumerable<ScenarioDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<ScenarioDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<ScenarioDTO>> GetAsync(int scenarioKey)
        {
            try
            {
                var scenario = await _context.DimScenario.FindAsync(scenarioKey);
                if (scenario == null)
                {
                    return new ActualResult<ScenarioDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<ScenarioDTO>(scenario);
                return new ActualResult<ScenarioDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<ScenarioDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<IEnumerable<ScenarioDTO>>> GetScenario(int scenarioKey)
        {
            try
            {
                var scenario = await _context.DimScenario.Where(x => x.ScenarioKey == scenarioKey).OrderBy(x => x.ScenarioName).ToListAsync();
                var result = _mapper.Map<IEnumerable<ScenarioDTO>>(scenario);
                return new ActualResult<IEnumerable<ScenarioDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<ScenarioDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateMainScenarioAsync(CreateScenarioDTO dto)
        {
            try
            {
                var scenario = _mapper.Map<DimScenario>(dto);
                await _context.DimScenario.AddAsync(scenario);
                await _context.SaveChangesAsync();
                return new ActualResult<string> { Result = scenario.ScenarioKey.ToString() };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> DeleteAsync(int scenarioKey)
        {
            try
            {
                var result = await _context.DimScenario.FindAsync(scenarioKey);
                if (result != null)
                {
                    _context.DimScenario.Remove(result);
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
