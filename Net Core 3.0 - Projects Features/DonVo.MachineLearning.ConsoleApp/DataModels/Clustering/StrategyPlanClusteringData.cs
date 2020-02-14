using Microsoft.ML.Data;

namespace DonVo.MachineLearning.ConsoleApp.DataModels
{
    public class StrategyPlanClusteringData
    {
        //public float Label;
        [LoadColumn(0)]
        public float EntityKey;
        [LoadColumn(1)]
        public float ScenarioKey;
        [LoadColumn(2)]
        public float AccountKey;
        [LoadColumn(3)]
        public float ProductCategoryKey;
        [LoadColumn(4)]
        [ColumnName("Label")]
        public float Amount { get; set; }

    }
}
