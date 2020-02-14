using Microsoft.ML.Data;

namespace DonVo.MLNet.DataModels
{
    public class FactStrategyPlanClusteringPredict
    {
        [ColumnName("PredictedLabel")]
        public uint SelectedClusterId;

        [ColumnName("Score")]
        public float[] Amount;
    }
}
