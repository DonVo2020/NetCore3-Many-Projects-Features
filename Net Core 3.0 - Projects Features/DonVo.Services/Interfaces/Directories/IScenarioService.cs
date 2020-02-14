using DonVo.Services.ActualResults;
using DonVo.ViewModels.DTOs.Directories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.Services.Interfaces.Directories
{
    public interface IScenarioService : IDisposable, IDirectory<ScenarioDTO>
    {
        Task<ActualResult<ScenarioDTO>> GetAsync(int scenarioKey);
        Task<ActualResult<IEnumerable<ScenarioDTO>>> GetScenario(int scenarioKey);

        Task<ActualResult<string>> CreateMainScenarioAsync(CreateScenarioDTO dto);

        Task<ActualResult> DeleteAsync(int scenarioKey);
    }
}
