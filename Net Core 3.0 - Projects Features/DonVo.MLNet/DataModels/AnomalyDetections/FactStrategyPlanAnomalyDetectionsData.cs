using Microsoft.ML.Data;

namespace DonVo.MLNet.DataModels
{
    public class FactStrategyPlanAnomalyDetectionsData
    {
        [LoadColumn(3)]
        public int ProductCategoryKey;

        [LoadColumn(4)]
        public float Amount;
    }
}
