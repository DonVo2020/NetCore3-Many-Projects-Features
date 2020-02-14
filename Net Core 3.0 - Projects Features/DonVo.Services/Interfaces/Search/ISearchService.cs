using DonVo.Services.ActualResults;
using DonVo.ViewModels.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DonVo.Services.Interfaces.Search
{
    public interface ISearchService : IDisposable
    {
        Task<ActualResult<IEnumerable<ResultSearchInventoryDTO>>> SearchInventory(int productKey, int storeKey);
        Task<ActualResult<IEnumerable<ResultSearchItslaDTO>>> SearchITSLA(int storeKey, int machineKey, int outageKey);
        Task<ActualResult<IEnumerable<ResultSearchSalesDTO>>> SearchSales(int channelKey, int storeKey, int productKey, int promotionKey);
        Task<ActualResult<IEnumerable<ResultSearchStrategyPlansDTO>>> SearchStrategyPlans(int entityKey, int scenarioKey, int accountKey, int productCategoryKey);
    }
}
