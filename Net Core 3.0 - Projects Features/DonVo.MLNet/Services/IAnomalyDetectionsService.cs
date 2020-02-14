using DonVo.MLNet.DataModels;
using Microsoft.ML;
using System.Collections.Generic;

namespace DonVo.MLNet.Services
{
    public interface IAnomalyDetectionsService<T> : IMachineLearningService<T> where T:class
    {
        IEnumerable<FactStrategyPlanAnomalyDetectionsPrediction> DetectSpike(int size, IDataView dataView);
        IEnumerable<FactStrategyPlanAnomalyDetectionsPrediction> DetectChangePoint(int size, IDataView dataView);
    }
}
