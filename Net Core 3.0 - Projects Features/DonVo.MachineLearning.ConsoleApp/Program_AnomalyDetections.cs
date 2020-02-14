using DonVo.MachineLearning.ConsoleApp.DataModels;
using Microsoft.Data.SqlClient;
using Microsoft.ML;
using Microsoft.ML.Data;
using System;

namespace DonVo.MachineLearning.ConsoleApp
{
    class Program_AnomalyDetections
    {
        static void Main_AnomalyDetections(string[] args)
        {
            var mlContext = new MLContext();

            string connectionString = @"Data Source=LAPTOP-ILQS92OM\SQLEXPRESS;Initial Catalog=ContosoRetailDW;Integrated Security=True;Pooling=False";

            string commandText = "SELECT top 100000 EntityKey,ScenarioKey,AccountKey,ProductCategoryKey,CONVERT(real, Amount * 100) from FactStrategyPlan";

            DatabaseLoader loader = mlContext.Data.CreateDatabaseLoader<StrategyPlan>();

            DatabaseSource dbSource = new DatabaseSource(SqlClientFactory.Instance,
                                                         connectionString,
                                                         commandText);

            IDataView dataView = loader.Load(dbSource);
            //ConsoleHelper.ShowDataViewInConsole(mlContext, dataView, 500);
            var trainTestData = mlContext.Data.TrainTestSplit(dataView);

            const int size = 100;

            ////To detech temporay changes in the pattern
            DetectSpike(mlContext, size, dataView);

            //To detect persistent change in the pattern
            //DetectChangepoint(mlContext, size, dataView);

            Console.WriteLine("=============== End of process, hit any key to finish ===============");
        }

        static void DetectSpike(MLContext mlcontext, int size, IDataView dataView)
        {
            Console.WriteLine("Detect temporary changes in pattern");

            //STEP 2: Set the training algorithm    
            var trainingPipeLine = mlcontext.Transforms.DetectIidSpike(outputColumnName: nameof(StrategyPlanPrediction.Prediction), inputColumnName: nameof(StrategyPlanData.Amount), confidence: 95, pvalueHistoryLength: size / 4);

            //STEP 3:Train the model by fitting the dataview
            Console.WriteLine("=============== Training the model using Spike Detection algorithm ===============");
            ITransformer trainedModel = trainingPipeLine.Fit(dataView);
            Console.WriteLine("=============== End of training process ===============");

            //Apply data transformation to create predictions.
            IDataView transformedData = trainedModel.Transform(dataView);
            var predictions = mlcontext.Data.CreateEnumerable<StrategyPlanPrediction>(transformedData, reuseRowObject: false);
            Console.WriteLine("Alert\tScore\tP-Value");
            foreach (var p in predictions)
            {
                if (p.Prediction[0] == 1)
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine("{0}\t{1:0.00}\t{2:0.00}", p.Prediction[0], p.Prediction[1], p.Prediction[2]);
                }
                //Console.WriteLine("{0}\t{1:0.00}\t{2:0.00}", p.Prediction[0], p.Prediction[1], p.Prediction[2]);
                Console.ResetColor();
            }
            Console.WriteLine("");
        }

        static void DetectChangepoint(MLContext mlcontext, int size, IDataView dataView)
        {
            Console.WriteLine("Detect Persistent changes in pattern");

            //STEP 2: Set the training algorithm    
            var trainingPipeLine = mlcontext.Transforms.DetectIidChangePoint(outputColumnName: nameof(StrategyPlanPrediction.Prediction), inputColumnName: nameof(StrategyPlanData.Amount), confidence: 95, changeHistoryLength: size / 4);

            //STEP 3:Train the model by fitting the dataview
            Console.WriteLine("=============== Training the model Using Change Point Detection Algorithm===============");
            ITransformer trainedModel = trainingPipeLine.Fit(dataView);
            Console.WriteLine("=============== End of training process ===============");

            //Apply data transformation to create predictions.
            IDataView transformedData = trainedModel.Transform(dataView);
            var predictions = mlcontext.Data.CreateEnumerable<StrategyPlanPrediction>(transformedData, reuseRowObject: false);

            Console.WriteLine($"{nameof(StrategyPlanPrediction.Prediction)} column obtained post-transformation.");
            Console.WriteLine("Alert\tScore\tP-Value\tMartingale value");

            foreach (var p in predictions)
            {
                if (p.Prediction[0] == 1)
                {
                    Console.WriteLine("{0}\t{1:0.00}\t{2:0.00}\t{3:0.00}  <-- alert is on, predicted changepoint", p.Prediction[0], p.Prediction[1], p.Prediction[2], p.Prediction[3]);
                }
                //else
                //{
                //    Console.WriteLine("{0}\t{1:0.00}\t{2:0.00}\t{3:0.00}", p.Prediction[0], p.Prediction[1], p.Prediction[2], p.Prediction[3]);
                //}
            }
            Console.WriteLine("");
        }


    }
}
