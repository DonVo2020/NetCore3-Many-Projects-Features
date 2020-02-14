using DonVo.Services.ActualResults;
using DonVo.ViewModels.DTOs.Directories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.Services.Interfaces.Directories
{
    public interface IEmployeeService : IDisposable, IDirectory<EmployeeDTO>
    {
        Task<ActualResult<EmployeeDTO>> GetAsync(int employeeKey);
        Task<ActualResult<IEnumerable<EmployeeDTO>>> GetEmployee(int employeeKey);

        Task<ActualResult<string>> CreateMainEmployeeAsync(CreateEmployeeDTO dto);

        Task<ActualResult> DeleteAsync(int employeeKey);
    }
}
