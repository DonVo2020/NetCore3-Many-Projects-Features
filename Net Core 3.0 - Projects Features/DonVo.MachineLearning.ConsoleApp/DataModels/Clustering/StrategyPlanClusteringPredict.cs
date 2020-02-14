using Microsoft.ML.Data;

namespace DonVo.MachineLearning.ConsoleApp.DataModels
{
    public class StrategyPlanClusteringPredict
    {
        [ColumnName("PredictedLabel")]
        public uint SelectedClusterId;

        [ColumnName("Score")]
        public float[] Amount;
    }
}
