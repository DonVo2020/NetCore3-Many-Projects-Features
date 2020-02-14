using DonVo.Services.ActualResults;
using DonVo.ViewModels.DTOs.Directories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.Services.Interfaces.Directories
{
    public interface IMachineService : IDisposable, IDirectory<MachineDTO>
    {
        Task<ActualResult<MachineDTO>> GetAsync(int machineKey);
        Task<ActualResult<IEnumerable<MachineDTO>>> GetMachine(int machineKey);

        Task<ActualResult<string>> CreateMainMachineAsync(CreateMachineDTO dto);

        Task<ActualResult> DeleteAsync(int machineKey);
    }
}
