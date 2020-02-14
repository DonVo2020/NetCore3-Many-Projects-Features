using AutoMapper;
using DonVo.Persistences;
using DonVo.Services.ActualResults;
using DonVo.Services.Helpers;
using DonVo.Services.Interfaces.Search;
using DonVo.ViewModels.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonVo.Services.Search
{
    public class SearchService : ISearchService
    {
        private readonly ContosoRetailDWContext _context;
        private readonly IMapper _mapper;

        public SearchService(ContosoRetailDWContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActualResult<IEnumerable<ResultSearchInventoryDTO>>> SearchInventory(int productKey, int storeKey)
        {
            try
            {
                var result = _context.FactInventory
                                .Join(
                                    _context.DimProduct,
                                    f => f.ProductKey,
                                    p => p.ProductKey,
                                    (f, p) => new { FactInventory = f, Product = p })
                                 .Join(
                                   _context.DimStore,
                                   combine2 => combine2.FactInventory.StoreKey,
                                   store => store.StoreKey,
                                   (combine2, store) => new { combine2.FactInventory, combine2.Product, Store = store });

                if (productKey > 0)
                    result = result.Where(x => x.Product.ProductKey == productKey);
                if (storeKey > 0)
                    result = result.Where(x => x.Store.StoreKey == storeKey);
                if (productKey == 0 && storeKey == 0)
                    result = result.Where(x => x.FactInventory.InventoryKey <= 1000);

                var finalResult = result
                                .OrderBy(o=>o.Store.StoreName).ThenBy(o=>o.Product.ProductName)
                                .Select(c => new ResultSearchInventoryDTO
                                {
                                    ProductName = c.Product.ProductName,
                                    DateKey = c.FactInventory.DateKey,
                                    StoreName = c.Store.StoreName,
                                    DaysInStock = c.FactInventory.DaysInStock,
                                    MaxDayInStock = c.FactInventory.MaxDayInStock,
                                    MinDayInStock = c.FactInventory.MinDayInStock,
                                    OnHandQuantity = c.FactInventory.OnHandQuantity,
                                    OnOrderQuantity = c.FactInventory.OnOrderQuantity,
                                    UnitCost = c.FactInventory.UnitCost
                                }).Distinct();

                var mapping = _mapper.Map<IEnumerable<ResultSearchInventoryDTO>>(await finalResult.Take(5000).ToListAsync());
                return new ActualResult<IEnumerable<ResultSearchInventoryDTO>> { Result = mapping };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<ResultSearchInventoryDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<IEnumerable<ResultSearchItslaDTO>>> SearchITSLA(int storeKey, int machineKey, int outageKey)
        {
            try
            {
                var result = _context.FactItsla
                                .Join(
                                    _context.DimStore,
                                    f => f.StoreKey,
                                    s => s.StoreKey,
                                    (f, s) => new { FactItsla = f, Store = s })
                                 .Join(
                                    _context.DimMachine,
                                    combine1 => combine1.FactItsla.MachineKey,
                                    m => m.MachineKey,
                                    (combine1, m) => new { combine1.FactItsla, combine1.Store, Machine = m })
                                 .Join(
                                   _context.DimOutage,
                                   combine2 => combine2.FactItsla.OutageKey,
                                   o => o.OutageKey,
                                   (combine2, o) => new { combine2.FactItsla, combine2.Store, combine2.Machine, Outage = o });

                if (storeKey > 0)
                    result = result.Where(x => x.Store.StoreKey == storeKey);
                if (machineKey > 0)
                    result = result.Where(x => x.Machine.MachineKey == machineKey);
                if (outageKey > 0)
                    result = result.Where(x => x.Outage.OutageKey == outageKey);
                if (storeKey == 0 && outageKey == 0 && machineKey == 0)
                    result = result.Where(x => x.FactItsla.Itslakey <= 1000);

                var finalResult = result
                                .OrderBy(o => o.Store.StoreName).ThenBy(o => o.Machine.MachineLabel).ThenBy(o => o.Outage.OutageLabel)
                                .Select(c => new ResultSearchItslaDTO
                                {
                                    StoreName = c.Store.StoreName,
                                    MachineName = c.Machine.MachineName,
                                    OutageName = c.Outage.OutageName,
                                    DateKey = c.FactItsla.DateKey,
                                    DownTime = c.FactItsla.DownTime,
                                    OutageEndTime = c.FactItsla.OutageEndTime,
                                    OutageStartTime = c.FactItsla.OutageStartTime
                                }).Distinct();

                var mapping = _mapper.Map<IEnumerable<ResultSearchItslaDTO>>(await finalResult.Take(5000).ToListAsync());
                return new ActualResult<IEnumerable<ResultSearchItslaDTO>> { Result = mapping };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<ResultSearchItslaDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<IEnumerable<ResultSearchSalesDTO>>> SearchSales(int channelKey, int storeKey, int productKey, int promotionKey)
        {
            try
            {
                var result = _context.FactSales
                                .Join(
                                    _context.DimStore,
                                    f => f.StoreKey,
                                    s => s.StoreKey,
                                    (f, s) => new { FactSales = f, Store = s })
                                 .Join(
                                    _context.DimChannel,
                                    combine1 => combine1.FactSales.ChannelKey,
                                    c => c.ChannelKey,
                                    (combine1, c) => new { combine1.FactSales, combine1.Store, Channel = c })
                                 .Join(
                                   _context.DimProduct,
                                   combine2 => combine2.FactSales.ProductKey,
                                   p => p.ProductKey,
                                   (combine2, p) => new { combine2.FactSales, combine2.Store, combine2.Channel, Product = p })
                                 .Join(
                                   _context.DimPromotion,
                                   combine3 => combine3.FactSales.PromotionKey,
                                   p2 => p2.PromotionKey,
                                   (combine3, p2) => new { combine3.FactSales, combine3.Store, combine3.Channel, combine3.Product, Promotion = p2 });

                if (channelKey > 0)
                    result = result.Where(x => x.Channel.ChannelKey == channelKey);
                if (storeKey > 0)
                    result = result.Where(x => x.Store.StoreKey == storeKey);
                if (productKey > 0)
                    result = result.Where(x => x.Product.ProductKey == productKey);
                if (promotionKey > 0)
                    result = result.Where(x => x.Promotion.PromotionKey == promotionKey);
                if (storeKey == 0 && channelKey == 0 && productKey == 0 && promotionKey == 0)
                    result = result.Where(x => x.FactSales.SalesKey <= 1000);

                var finalResult = result
                                .OrderBy(o => o.Channel.ChannelName).ThenBy(o => o.Store.StoreName).ThenBy(o => o.Product.ProductLabel).ThenBy(o => o.Promotion.PromotionLabel)
                                .Select(c => new ResultSearchSalesDTO
                                {
                                    StoreName = c.Store.StoreName,
                                    ChannelName = c.Channel.ChannelName,
                                    ProductName = c.Product.ProductName,
                                    PromotionName = c.Promotion.PromotionName,
                                    DateKey = c.FactSales.DateKey,
                                    DiscountAmount = c.FactSales.DiscountAmount,
                                    DiscountQuantity = c.FactSales.DiscountQuantity,
                                    ReturnAmount = c.FactSales.ReturnAmount,
                                    ReturnQuantity = c.FactSales.ReturnQuantity,
                                    SalesAmount = c.FactSales.SalesAmount,
                                    SalesQuantity = c.FactSales.SalesQuantity,
                                    TotalCost = c.FactSales.TotalCost,
                                    UnitCost = c.FactSales.UnitCost,
                                    UnitPrice = c.FactSales.UnitPrice
                                }).Distinct();

                var mapping = _mapper.Map<IEnumerable<ResultSearchSalesDTO>>(await finalResult.Take(5000).ToListAsync());
                return new ActualResult<IEnumerable<ResultSearchSalesDTO>> { Result = mapping };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<ResultSearchSalesDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public async Task<ActualResult<IEnumerable<ResultSearchStrategyPlansDTO>>> SearchStrategyPlans(int entityKey, int scenarioKey, int accountKey, int productCategoryKey)
        {
            try
            {
                var result = _context.FactStrategyPlan
                                .Join(
                                    _context.DimEntity,
                                    f => f.EntityKey,
                                    e => e.EntityKey,
                                    (f, e) => new { FactStrategyPlan = f, Entity = e })
                                 .Join(
                                    _context.DimScenario,
                                    combine1 => combine1.FactStrategyPlan.ScenarioKey,
                                    s => s.ScenarioKey,
                                    (combine1, s) => new { combine1.FactStrategyPlan, combine1.Entity, Scenario = s })
                                 .Join(
                                   _context.DimAccount,
                                   combine2 => combine2.FactStrategyPlan.AccountKey,
                                   a => a.AccountKey,
                                   (combine2, a) => new { combine2.FactStrategyPlan, combine2.Entity, combine2.Scenario, Account = a })
                                 .Join(
                                   _context.DimProductCategory,
                                   combine3 => combine3.FactStrategyPlan.ProductCategoryKey,
                                   p => p.ProductCategoryKey,
                                   (combine3, p) => new { combine3.FactStrategyPlan, combine3.Entity, combine3.Scenario, combine3.Account, ProductCategory = p });

                if (entityKey > 0)
                    result = result.Where(x => x.Entity.EntityKey == entityKey);
                if (scenarioKey > 0)
                    result = result.Where(x => x.Scenario.ScenarioKey == scenarioKey);
                if (accountKey > 0)
                    result = result.Where(x => x.Account.AccountKey == accountKey);
                if (productCategoryKey > 0)
                    result = result.Where(x => x.ProductCategory.ProductCategoryKey == productCategoryKey);
                if (entityKey == 0 && scenarioKey == 0 && accountKey == 0 && productCategoryKey == 0)
                    result = result.Where(x => x.FactStrategyPlan.StrategyPlanKey <= 1000);

                var finalResult = result
                                .OrderBy(o => o.Entity.EntityName).ThenBy(o => o.Scenario.ScenarioLabel).ThenBy(o => o.Account.AccountLabel).ThenBy(o => o.ProductCategory.ProductCategoryLabel)
                                .Select(c => new ResultSearchStrategyPlansDTO
                                {
                                    EntityName = c.Entity.EntityName,
                                    AccountName = c.Account.AccountName,
                                    ProductCategoryName = c.ProductCategory.ProductCategoryName,
                                    Amount = Convert.ToDouble(c.FactStrategyPlan.Amount),
                                    Datekey = c.FactStrategyPlan.Datekey,
                                    ScenarioName = c.Scenario.ScenarioName

                                }).Distinct();

                var mapping = _mapper.Map<IEnumerable<ResultSearchStrategyPlansDTO>>(await finalResult.Take(5000).ToListAsync());
                return new ActualResult<IEnumerable<ResultSearchStrategyPlansDTO>> { Result = mapping };
            }
            catch (Exception exception)
            {
                return new ActualResult<IEnumerable<ResultSearchStrategyPlansDTO>>(DescriptionExceptionHelper.GetDescriptionError(exception));
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }      
    }
}
