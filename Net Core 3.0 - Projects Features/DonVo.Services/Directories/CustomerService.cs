using AutoMapper;
using DonVo.Persistences;
using DonVo.Persistences.Models;
using DonVo.Services.ActualResults;
using DonVo.Services.Enums;
using DonVo.Services.Helpers;
using DonVo.SpecialConfigurations.Helpers;
using DonVo.ViewModels.DTOs.Directories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonVo.Services.Directories
{
    public class CustomerService : ICustomerService
    {
        private readonly ContosoRetailDWContext _context;
        private readonly IMapper _mapper;

        public CustomerService(ContosoRetailDWContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<CustomerDTO>>> GetAllAsync()
        {
            try
            {
                var customers = await _context.DimCustomer.Where(z=> !string.IsNullOrWhiteSpace(z.FirstName) && !string.IsNullOrWhiteSpace(z.LastName) && !string.IsNullOrWhiteSpace(z.MiddleName)).OrderBy(x => x.FirstName).Take(700).ToListAsync();
                var result = _mapper.Map<IEnumerable<CustomerDTO>>(customers);
                return new ActualResult<IEnumerable<CustomerDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<CustomerDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<CustomerDTO>> GetAsync(string hashId)
        {
            try
            {
                //var id = HashHelper.DecryptLong(hashId);
                var id = Convert.ToInt32(hashId);// HashHelper.DecryptLong(hashId);
                var customer = await _context.DimCustomer.FindAsync(id);
                if (customer == null)
                {
                    return new ActualResult<CustomerDTO>(Errors.TupleDeleted);
                }
                var result = _mapper.Map<CustomerDTO>(customer);
                return new ActualResult<CustomerDTO> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<CustomerDTO>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<IEnumerable<CustomerDTO>>> GetCustomer(string hashId)
        {
            try
            {
                var id = Convert.ToInt32(hashId);// HashHelper.DecryptLong(hashId);
                var customer = await _context.DimCustomer.Where(x => x.CustomerKey == id).OrderBy(x => x.FirstName).ToListAsync();
                var result = _mapper.Map<IEnumerable<CustomerDTO>>(customer);
                return new ActualResult<IEnumerable<CustomerDTO>> { Result = result };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<CustomerDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<Dictionary<string, string>> GetCustomerForMvc(string hashId)
        {
            var customerForMvc = await GetCustomer(hashId);
            return customerForMvc.Result.ToDictionary(customer => $"{customer.HashIdMain},{customer.FirstName}", customer => customer.LastName);
        }

        public async Task<IEnumerable<TreeCustomersDTO>> GetTreeCustomers()
        {
            var customers = await _context.DimCustomer
                                             .OrderBy(x => x.CompanyName)
                                             .ToListAsync();
            return customers.Select(customer => new TreeCustomersDTO
            {
                GroupName = customer.LastName,
                Customers = FormationTree(customer)
            }).ToList();
        }

        private IEnumerable<CustomerDTO> FormationTree(DimCustomer customer)
        {
            var list = new List<CustomerDTO>
            {
                new CustomerDTO
                {
                    HashIdMain = HashHelper.EncryptLong(customer.CustomerKey),
                    LastName = customer.LastName
                }
            };
            //list.AddRange(customer.InverseIdCustomersNavigation.Select(customer => new CustomerDTO
            //{
            //    HashIdMain = HashHelper.EncryptLong(customer.CustomerKey),
            //    LastName = customer.LastName
            //}));
            return list;
        }

        public async Task<ActualResult<string>> CreateMainCustomerAsync(CreateCustomerDTO dto)
        {
            try
            {
                var customer = _mapper.Map<DimCustomer>(dto);
                await _context.DimCustomer.AddAsync(customer);
                await _context.SaveChangesAsync();
                var hashId = HashHelper.EncryptLong(customer.CustomerKey);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> DeleteAsync(string hashId)
        {
            try
            {
                var id = HashHelper.DecryptLong(hashId);
                var result = await _context.DimCustomer.FindAsync(id);
                if (result != null)
                {
                    _context.DimCustomer.Remove(result);
                    await _context.SaveChangesAsync();
                }
                return new ActualResult();
            }
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<string>> CreateCustomerCustomerAsync(CreateCustomerCustomerDTO dto)
        {
            try
            {
                var customer = _mapper.Map<DimCustomer>(dto);
                await _context.DimCustomer.AddAsync(customer);
                await _context.SaveChangesAsync();
                var hashId = HashHelper.EncryptLong(customer.CustomerKey);
                return new ActualResult<string> { Result = hashId };
            }
            catch (Exception exception)
            {
                return new ActualResult<string>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateCustomerAsync(CustomerDTO dto)
        {
            try
            {
                var mapping = _mapper.Map<DimCustomer>(dto);
                _context.Entry(mapping).State = EntityState.Modified;
                _context.Entry(mapping).Property(x => x.CustomerKey).IsModified = false;
                _context.Entry(mapping).Property(x => x.GeographyKey).IsModified = false;
                _context.Entry(mapping).Property(x => x.CustomerLabel).IsModified = false;
                _context.Entry(mapping).Property(x => x.FirstName).IsModified = false;
                _context.Entry(mapping).Property(x => x.LastName).IsModified = false;
                _context.Entry(mapping).Property(x => x.MiddleName).IsModified = false;
                _context.Entry(mapping).Property(x => x.AddressLine2).IsModified = false;
                _context.Entry(mapping).Property(x => x.BirthDate).IsModified = false;
                _context.Entry(mapping).Property(x => x.CustomerType).IsModified = false;
                _context.Entry(mapping).Property(x => x.DateFirstPurchase).IsModified = false;
                _context.Entry(mapping).Property(x => x.EtlloadId).IsModified = false;
                _context.Entry(mapping).Property(x => x.HouseOwnerFlag).IsModified = false;
                _context.Entry(mapping).Property(x => x.NameStyle).IsModified = false;
                _context.Entry(mapping).Property(x => x.NumberCarsOwned).IsModified = false;
                _context.Entry(mapping).Property(x => x.NumberChildrenAtHome).IsModified = false;
                _context.Entry(mapping).Property(x => x.Suffix).IsModified = false;
                _context.Entry(mapping).Property(x => x.Title).IsModified = false;
                _context.Entry(mapping).Property(x => x.TotalChildren).IsModified = false;
                _context.Entry(mapping).Property(x => x.UpdateDate).IsModified = false;
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateCustomerEducationAsync(UpdateCustomerEducationDTO dto)
        {
            try
            {
                var mapping = _mapper.Map<DimCustomer>(dto);
                _context.Entry(mapping).State = EntityState.Modified;
                _context.Entry(mapping).Property(x => x.CustomerKey).IsModified = false;
                _context.Entry(mapping).Property(x => x.Education).IsModified = false;
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateCustomerEmailAddressAsync(CustomerDTO dto)
        {
            try
            {
                var mapping = _mapper.Map<DimCustomer>(dto);
                _context.Entry(mapping).State = EntityState.Deleted;
                _context.Entry(mapping).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> UpdateCustomerCompanyNameAsync(CustomerDTO dto)
        {
            try
            {
                var mapping = _mapper.Map<DimCustomer>(dto);
                _context.Entry(mapping).State = EntityState.Deleted;
                _context.Entry(mapping).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new ActualResult();
            }
            catch (Exception exception)
            {
                return new ActualResult(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult> RestructuringCustomer(RestructuringCustomerDTO dto)
        {
            try
            {
                var mapping = _mapper.Map<DimCustomer>(dto);
                _context.Entry(mapping).State = EntityState.Modified;
                _context.Entry(mapping).Property(x => x.CustomerKey).IsModified = false;
                _context.Entry(mapping).Property(x => x.CompanyName).IsModified = false;
                _context.Entry(mapping).Property(x => x.Education).IsModified = false;
                _context.Entry(mapping).Property(x => x.FirstName).IsModified = false;
                _context.Entry(mapping).Property(x => x.LastName).IsModified = false;
                _context.Entry(mapping).Property(x => x.MiddleName).IsModified = false;
                _context.Entry(mapping).Property(x => x.MaritalStatus).IsModified = false;
                _context.Entry(mapping).Property(x => x.Gender).IsModified = false;
                _context.Entry(mapping).Property(x => x.EmailAddress).IsModified = false;
                _context.Entry(mapping).Property(x => x.Phone).IsModified = false;
                _context.Entry(mapping).Property(x => x.Occupation).IsModified = false;
                await _context.SaveChangesAsync();
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
