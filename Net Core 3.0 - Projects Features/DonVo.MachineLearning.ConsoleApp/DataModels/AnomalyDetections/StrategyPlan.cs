using Microsoft.ML.Data;

namespace DonVo.MachineLearning.ConsoleApp.DataModels
{
    public class StrategyPlan
    {
        [LoadColumn(0)]
        public int EntityKey { get; set; }
        [LoadColumn(1)]
        public int ScenarioKey { get; set; }
        [LoadColumn(2)]
        public int AccountKey { get; set; }
        [LoadColumn(3)]
        public int ProductCategoryKey { get; set; }
        [LoadColumn(4)]
        public float Amount { get; set; }

        //[LoadColumn(5)]
        //[ColumnName("Label")]
        //public float Score { get; set; }
    }
}
