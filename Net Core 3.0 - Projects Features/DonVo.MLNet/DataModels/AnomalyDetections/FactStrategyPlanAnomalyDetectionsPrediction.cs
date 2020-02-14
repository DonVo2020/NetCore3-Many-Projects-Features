using Microsoft.ML.Data;

namespace DonVo.MLNet.DataModels
{
    public class FactStrategyPlanAnomalyDetectionsPrediction
    {
        //vector to hold alert,score,p-value values
        [VectorType(3)]
        public double[] Prediction { get; set; }
    }
}
