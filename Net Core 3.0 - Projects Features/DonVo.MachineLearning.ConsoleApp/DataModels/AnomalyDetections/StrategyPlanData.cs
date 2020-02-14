using Microsoft.ML.Data;

namespace DonVo.MachineLearning.ConsoleApp.DataModels
{
    public class StrategyPlanData
    {
        [LoadColumn(3)]
        public int ProductCategoryKey;

        [LoadColumn(4)]
        public float Amount;
    }
}
