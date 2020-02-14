using Microsoft.ML.Data;

namespace DonVo.MachineLearning.ConsoleApp.DataModels
{
    public class StrategyPlanMulticlassClassificationsModel
    {
        [LoadColumn(0)]
        public string ProductCategoryName { get; set; }

        [LoadColumn(1)]
        public string ProductSubcategory { get; set; }
        [LoadColumn(2)]
        public string Product { get; set; }

        [LoadColumn(3)]
        public string Region { get; set; }
        [LoadColumn(4)]
        public string IncomeGroup { get; set; }
        [LoadColumn(5)]
        public string Age { get; set; }
    }
}
