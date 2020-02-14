using DonVo.Services.ActualResults;
using DonVo.Services.Interfaces;
using DonVo.ViewModels.DTOs.Directories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.Services.Directories
{
    public interface ICustomerService : IDisposable, IDirectory<CustomerDTO>
    {
        Task<ActualResult<CustomerDTO>> GetAsync(string hashId);
        Task<ActualResult<IEnumerable<CustomerDTO>>> GetCustomer(string hashId);
        Task<Dictionary<string, string>> GetCustomerForMvc(string hashId);
        Task<IEnumerable<TreeCustomersDTO>> GetTreeCustomers();

        Task<ActualResult<string>> CreateMainCustomerAsync(CreateCustomerDTO dto);
        Task<ActualResult<string>> CreateCustomerCustomerAsync(CreateCustomerCustomerDTO dto);

        Task<ActualResult> UpdateCustomerAsync(CustomerDTO dto);
        Task<ActualResult> UpdateCustomerEducationAsync(UpdateCustomerEducationDTO dto);
        Task<ActualResult> UpdateCustomerEmailAddressAsync(CustomerDTO dto);
        Task<ActualResult> UpdateCustomerCompanyNameAsync(CustomerDTO dto);
        Task<ActualResult> RestructuringCustomer(RestructuringCustomerDTO dto);

        Task<ActualResult> DeleteAsync(string hashId);
    }
}
