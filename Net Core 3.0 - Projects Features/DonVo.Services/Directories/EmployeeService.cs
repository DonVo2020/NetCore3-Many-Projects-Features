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
    public class EmployeeService : IEmployeeService
    {
        private readonly ContosoRetailDWContext _context;
        private readonly IMapper _mapper;

        public EmployeeService(ContosoRetailDWContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<EmployeeDTO>>> GetAllAsync()
        {
            try
            {
                var employees = await _context.DimEmployee.Where(z => !string.IsNullOrWhiteSpace(z.FirstName) && !string.IsNullOrWhiteSpace(z.LastName)).OrderBy(x => x.FirstName).ToListAsync();
                var result = _mapper.Map<IEnumerable<EmployeeDTO>>(employees);
                return new ActualResult<IEnumerable<EmployeeDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<EmployeeDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<EmployeeDTO>> GetAsync(int acccountKey)
        {
            try
            {
                var employee = await _context.DimEmployee.FindAsync(acccountKey);
                if (employee == null)
                {
                    return new ActualResult<EmployeeDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<EmployeeDTO>(employee);
                return new ActualResult<EmployeeDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<EmployeeDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<IEnumerable<EmployeeDTO>>> GetEmployee(int accountKey)
        {
            try
            {
                var employee = await _context.DimEmployee.Where(x => x.EmployeeKey == accountKey).OrderBy(x => x.FirstName).ToListAsync();
                var result = _mapper.Map<IEnumerable<EmployeeDTO>>(employee);
                return new ActualResult<IEnumerable<EmployeeDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<EmployeeDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateMainEmployeeAsync(CreateEmployeeDTO dto)
        {
            try
            {
                var employee = _mapper.Map<DimEmployee>(dto);
                await _context.DimEmployee.AddAsync(employee);
                await _context.SaveChangesAsync();
                return new ActualResult<string> { Result = employee.EmployeeKey.ToString() };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> DeleteAsync(int employeeKey)
        {
            try
            {
                var result = await _context.DimEmployee.FindAsync(employeeKey);
                if (result != null)
                {
                    _context.DimEmployee.Remove(result);
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
