﻿using Microsoft.ML.Data;

namespace DonVo.MachineLearning.ConsoleApp.DataModels
{
    public class StrategyPlanPrediction
    {
        //vector to hold alert,score,p-value values
        [VectorType(3)]
        public double[] Prediction { get; set; }
    }
}
