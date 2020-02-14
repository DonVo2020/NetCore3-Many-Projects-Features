using DonVo.MachineLearning.ConsoleApp.Common;
using DonVo.MachineLearning.ConsoleApp.DataModels;
using Microsoft.Data.SqlClient;
using Microsoft.ML;
using Microsoft.ML.Data;
using System;

namespace DonVo.MachineLearning.ConsoleApp
{
    class Program
    {
        private static string ModelPath = @"C:\DEVELOPMENT\NetCore30_StepsBySteps\DonVo.NetCore3\DonVo.MachineLearning.ConsoleApp\MLModels\Clustering\FactStrategyPlanClusteringModel.zip";
        static void Main(string[] args)
        {

            var mlContext = new MLContext();

            string connectionString = @"Data Source=LAPTOP-ILQS92OM\SQLEXPRESS;Initial Catalog=ContosoRetailDW;Integrated Security=True;Pooling=False";

            string commandText = "SELECT CONVERT(real, EntityKey), CONVERT(real, ScenarioKey),CONVERT(real, AccountKey), CONVERT(real, ProductCategoryKey),CONVERT(real, Amount) from FactStrategyPlan";

            DatabaseLoader loader = mlContext.Data.CreateDatabaseLoader<StrategyPlanClusteringData>();

            DatabaseSource dbSource = new DatabaseSource(SqlClientFactory.Instance,
                                                         connectionString,
                                                         commandText);

            IDataView dataView = loader.Load(dbSource);
            //ConsoleHelper.ShowDataViewInConsole(mlContext, dataView, 500);
            var trainTestData = mlContext.Data.TrainTestSplit(dataView).TestSet;


            //STEP 2: Process data transformations in pipeline
            var dataProcessPipeline = mlContext.Transforms.Concatenate("Features", nameof(StrategyPlanClusteringData.EntityKey), nameof(StrategyPlanClusteringData.ScenarioKey), nameof(StrategyPlanClusteringData.AccountKey),
                                                                                    nameof(StrategyPlanClusteringData.ProductCategoryKey));

            // (Optional) Peek data in training DataView after applying the ProcessPipeline's transformations  
            ConsoleHelper.PeekDataViewInConsole(mlContext, dataView, dataProcessPipeline, 10);
            ConsoleHelper.PeekVectorColumnDataInConsole(mlContext, "Features", dataView, dataProcessPipeline, 10);

            // STEP 3: Create and train the model     
            var trainer = mlContext.Clustering.Trainers.KMeans(featureColumnName: "Features", numberOfClusters: 10);
            var trainingPipeline = dataProcessPipeline.Append(trainer);
            var trainedModel = trainingPipeline.Fit(dataView);

            // STEP4: Evaluate accuracy of the model
            IDataView predictions = trainedModel.Transform(dataView);
            var metrics = mlContext.Clustering.Evaluate(predictions, scoreColumnName: "Score", featureColumnName: "Features");

            ConsoleHelper.PrintClusteringMetrics(trainer.ToString(), metrics);

            //STEP5: Save / persist the model as a.ZIP file
            mlContext.Model.Save(trainedModel, dataView.Schema, ModelPath);

            Console.WriteLine("=============== End of training process ===============");

            Console.WriteLine("=============== Predict a cluster for a single case (Single StrategyPlanClusteringData sample) ===============");

            // Test with one sample text 
            var sampleStrategyPlanClusteringData = new StrategyPlanClusteringData()
            {
                EntityKey = 701,
                ScenarioKey = 3,
                AccountKey = 10,
                ProductCategoryKey = 1
            };

            ITransformer model = mlContext.Model.Load(ModelPath, out var modelInputSchema);
            // Create prediction engine related to the loaded trained model
            var predEngine = mlContext.Model.CreatePredictionEngine<StrategyPlanClusteringData, StrategyPlanClusteringPredict>(model);

            //Score
            var resultprediction = predEngine.Predict(sampleStrategyPlanClusteringData);

            Console.WriteLine($"Cluster assigned for Strategy Plan is " + resultprediction.SelectedClusterId);

            Console.WriteLine("=============== End of process, hit any key to finish ===============");
        }




    }
}
