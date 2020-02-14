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
    public class MachineService : IMachineService
    {
        private readonly ContosoRetailDWContext _context;
        private readonly IMapper _mapper;

        public MachineService(ContosoRetailDWContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<MachineDTO>>> GetAllAsync()
        {
            try
            {
                var machines = await _context.DimMachine.Where(z => !string.IsNullOrWhiteSpace(z.MachineName)).OrderBy(x => x.MachineLabel).ToListAsync();
                var result = _mapper.Map<IEnumerable<MachineDTO>>(machines);
                return new ActualResult<IEnumerable<MachineDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<MachineDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<MachineDTO>> GetAsync(int machineKey)
        {
            try
            {
                var machine = await _context.DimMachine.FindAsync(machineKey);
                if (machine == null)
                {
                    return new ActualResult<MachineDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<MachineDTO>(machine);
                return new ActualResult<MachineDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<MachineDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<IEnumerable<MachineDTO>>> GetMachine(int machineKey)
        {
            try
            {
                var machine = await _context.DimMachine.Where(x => x.MachineKey == machineKey).OrderBy(x => x.MachineName).ToListAsync();
                var result = _mapper.Map<IEnumerable<MachineDTO>>(machine);
                return new ActualResult<IEnumerable<MachineDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<MachineDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateMainMachineAsync(CreateMachineDTO dto)
        {
            try
            {
                var machine = _mapper.Map<DimMachine>(dto);
                await _context.DimMachine.AddAsync(machine);
                await _context.SaveChangesAsync();
                return new ActualResult<string> { Result = machine.MachineKey.ToString() };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> DeleteAsync(int machineKey)
        {
            try
            {
                var result = await _context.DimMachine.FindAsync(machineKey);
                if (result != null)
                {
                    _context.DimMachine.Remove(result);
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
