using DonVo.Services.ActualResults;
using Microsoft.ML;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.MLNet.Services
{
    public interface IMachineLearningService<T> : IDisposable where T: class
    {
        ActualResult<IDataView> LoadData();
        Task<ActualResult<IEnumerable<T>>> GetTableResult(T table);
        Task<ActualResult<T>> GetStatisticsResult(T statisticObject);
        //Task<ActualResult<IEnumerable<ResultSearchItslaDTO>>> SearchITSLA(int storeKey, int machineKey, int outageKey);
    }
}
