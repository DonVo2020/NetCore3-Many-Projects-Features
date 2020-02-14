using Microsoft.ML.Data;

namespace DonVo.MachineLearning.ConsoleApp.DataModels
{
    public class StrategyPlanMulticlassClassificationsPredict
    {

        [ColumnName("PredictedLabel")]
        public string ProductCategoryName { get; set; }
    }
}
