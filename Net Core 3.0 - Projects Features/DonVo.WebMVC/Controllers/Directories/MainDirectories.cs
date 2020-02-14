using DonVo.Services.Directories;
using DonVo.Services.Interfaces.Account;
using DonVo.Services.Interfaces.Directories;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DonVo.WebMVC.Controllers.Directories
{
    public class MainDirectories : IMainDirectories
    {
        private readonly IAccountService _accountService;
        private readonly ICustomerService _customerService;
        private readonly IProductCategoryService _productCategoryService;
        private readonly ICurrencyService _currencyService;
        private readonly IStoreService _storeService;
        private readonly IMachineService _machineService;
        private readonly IOutageService _outageService;
        private readonly IChannelService _channelService;
        private readonly IPromotionService _promotionService;
        private readonly IEntityService _entityService;
        private readonly IScenarioService _scenarioService;
        private readonly IAccountingService _accountingService;
        private readonly IProductService _productService;
        private readonly IProductSubcategoryService _productSubcategoryService;

        public MainDirectories( IAccountService accountService, ICustomerService customerService, IProductCategoryService productCategoryService,
                                ICurrencyService currencyService, IStoreService storeService, IProductService productService, 
                                IMachineService machineService, IOutageService outageService, IChannelService channelService,
                                IPromotionService promotionService, IEntityService entityService, IScenarioService scenarioService,
                                IAccountingService accountingService, IProductSubcategoryService productSubcategoryService)
        {
            _accountService = accountService;
            _customerService = customerService;
            _productCategoryService = productCategoryService;
            _currencyService = currencyService;
            _storeService = storeService;
            _machineService = machineService;
            _outageService = outageService;
            _channelService = channelService;
            _promotionService = promotionService;
            _entityService = entityService;
            _scenarioService = scenarioService;
            _accountingService = accountingService;
            _productService = productService;
            _productSubcategoryService = productSubcategoryService;
        }

        public async Task<SelectList> GetEmails()
        {
            var users = await _accountService.GetAllAccountsAsync();
            return users.IsValid ? new SelectList(users.Result.Select(account => account.Email)) : null;
        }

        public async Task<SelectList> GetRoles()
        {
            var roles = await _accountService.GetRoles();
            return roles.IsValid ? new SelectList(roles.Result, "Name", "Name") : null;
        }

        public async Task<SelectList> GetMainCustomer()
        {
            var customers = await _customerService.GetAllAsync();
            return customers.IsValid ? new SelectList(customers.Result, "HashIdMain", "LastName") : null;
        }
        public async Task<SelectList> GetProductCategory(string hashIdSelectedValue = null)
        {
            var productCategories = await _productCategoryService.GetAllAsync();
            return productCategories.IsValid ? new SelectList(productCategories.Result, "ProductCategoryKey", "ProductCategoryName", hashIdSelectedValue) : null;
        }

        public async Task<SelectList> GetProduct(string hashIdSelectedValue = null)
        {
            var products = await _productService.GetAllAsync();
            return products.IsValid ? new SelectList(products.Result, "ProductKey", "ProductName", hashIdSelectedValue) : null;
        }
        public async Task<SelectList> GetCurrency(string hashIdSelectedValue = null)
        {
            var currencies = await _currencyService.GetAllAsync();
            return currencies.IsValid ? new SelectList(currencies.Result, "CurrencyKey", "CurrencyName", hashIdSelectedValue) : null;
        }
        public async Task<SelectList> GetStore(string hashIdSelectedValue = null)
        {
            var stores = await _storeService.GetAllAsync();
            return stores.IsValid ? new SelectList(stores.Result, "StoreKey", "StoreName", hashIdSelectedValue) : null;
        }
        public async Task<SelectList> GetMachine(string hashIdSelectedValue = null)
        {
            var machines = await _machineService.GetAllAsync();
            return machines.IsValid ? new SelectList(machines.Result, "MachineKey", "MachineLabel", hashIdSelectedValue) : null;
        }
        public async Task<SelectList> GetOutage(string hashIdSelectedValue = null)
        {
            var outages = await _outageService.GetAllAsync();
            return outages.IsValid ? new SelectList(outages.Result, "OutageKey", "OutageLabel", hashIdSelectedValue) : null;
        }
        public async Task<SelectList> GetChannel(string hashIdSelectedValue = null)
        {
            var channels = await _channelService.GetAllAsync();
            return channels.IsValid ? new SelectList(channels.Result, "ChannelKey", "ChannelName", hashIdSelectedValue) : null;
        }
        public async Task<SelectList> GetPromotion(string hashIdSelectedValue = null)
        {
            var promotions = await _promotionService.GetAllAsync();
            return promotions.IsValid ? new SelectList(promotions.Result, "PromotionKey", "PromotionLabel", hashIdSelectedValue) : null;
        }
        public async Task<SelectList> GetEntity(string hashIdSelectedValue = null)
        {
            var entities = await _entityService.GetAllAsync();
            return entities.IsValid ? new SelectList(entities.Result, "EntityKey", "EntityName", hashIdSelectedValue) : null;
        }
        public async Task<SelectList> GetScenario(string hashIdSelectedValue = null)
        {
            var scenarios = await _scenarioService.GetAllAsync();
            return scenarios.IsValid ? new SelectList(scenarios.Result, "ScenarioKey", "ScenarioName", hashIdSelectedValue) : null;
        }
        public async Task<SelectList> GetAccount(string hashIdSelectedValue = null)
        {
            var accountings = await _accountingService.GetAllAsync();
            return accountings.IsValid ? new SelectList(accountings.Result, "AccountKey", "AccountName", hashIdSelectedValue) : null;
        }

        public async Task<SelectList> GetProductSubcategory(string hashIdSelectedValue = null)
        {
            var productSubcategories = await _productSubcategoryService.GetAllAsync();
            return productSubcategories.IsValid ? new SelectList(productSubcategories.Result, "ProductSubcategoryKey", "ProductSubcategoryName", hashIdSelectedValue) : null;
        }

        public void Dispose()
        {
            _accountService.Dispose();
            _customerService.Dispose();
            _productCategoryService.Dispose();
            _currencyService.Dispose();
            _storeService.Dispose();
            _machineService.Dispose();
            _outageService.Dispose();
            _channelService.Dispose();
            _promotionService.Dispose();
            _entityService.Dispose();
            _scenarioService.Dispose();
            _accountingService.Dispose();
            _productService.Dispose();
            _productSubcategoryService.Dispose();
        }

    }

    public interface IMainDirectories : IDisposable
    {
        Task<SelectList> GetEmails();
        Task<SelectList> GetRoles();
        Task<SelectList> GetMainCustomer();
        Task<SelectList> GetProductCategory(string hashIdSelectedValue = null);
        Task<SelectList> GetCurrency(string hashIdSelectedValue = null);
        Task<SelectList> GetStore(string hashIdSelectedValue = null);
        Task<SelectList> GetProduct(string hashIdSelectedValue = null);
        Task<SelectList> GetMachine(string hashIdSelectedValue = null);
        Task<SelectList> GetOutage(string hashIdSelectedValue = null);
        Task<SelectList> GetChannel(string hashIdSelectedValue = null);
        Task<SelectList> GetPromotion(string hashIdSelectedValue = null);
        Task<SelectList> GetEntity(string hashIdSelectedValue = null);
        Task<SelectList> GetScenario(string hashIdSelectedValue = null);
        Task<SelectList> GetAccount(string hashIdSelectedValue = null);
        Task<SelectList> GetProductSubcategory(string hashIdSelectedValue = null);
    }
}
