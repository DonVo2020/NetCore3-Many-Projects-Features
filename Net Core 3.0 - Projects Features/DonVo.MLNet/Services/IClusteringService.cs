using DonVo.MLNet.DataModels;
using DonVo.MLNet.DTOs;
using Microsoft.ML;
using System.Collections.Generic;

namespace DonVo.MLNet.Services
{
    public interface IClusteringService<T> : IMachineLearningService<T> where T:class
    {
        void TrainModel(IDataView dataView);
        FactStrategyPlanClusteringDTO GetFactStrategyPlanClusteringDTO(int entityKey, int scenarioKey, int accountKey, int productCategoryKey);
        FactStrategyPlanClusteringDTO TestModelKMeans(FactStrategyPlanClusteringDTO factStrategyPlanClusteringDTO);
    }
}
