using DonVo.MachineLearning.ConsoleApp.DataModels;
using Microsoft.Data.SqlClient;
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.IO;
using static Microsoft.ML.DataOperationsCatalog;

namespace DonVo.MachineLearning.ConsoleApp
{
    class Program_MulticlassClassifications
    {
        private static string ModelPath = @"C:\DEVELOPMENT\NetCore30_StepsBySteps\DonVo.NetCore3\DonVo.MachineLearning.ConsoleApp\MLModels\MulticlassClassifications\CustomerOrdersSdcaMaximumEntropyModel.zip";
        private static List<StrategyPlanMulticlassClassificationsModel> productData = new List<StrategyPlanMulticlassClassificationsModel>();
        static void Main_MulticlassClassifications(string[] args)
        {
            TestData();

            //if (!File.Exists(ModelPath))
            //{
            //    TrainModel();
            //}

            TrainModel();
            TestModel();

            Console.WriteLine("=============== End of process, hit any key to finish ===============");
        }

        private static void TestData()
        {
            var model1 = new StrategyPlanMulticlassClassificationsModel
            {
                Product = "Fabrikam Laptop15.4 M5400 White",
                ProductSubcategory = "Laptops"
            };
            var model2 = new StrategyPlanMulticlassClassificationsModel
            {
                Product = "Proseware Chandelier M0615 White",
                ProductSubcategory = "Lamps"
            };
            var model3 = new StrategyPlanMulticlassClassificationsModel
            {
                Product = "NT Washer & Dryer 15.5in E1550 Blue",
                ProductSubcategory = "Washers & Dryers"
            };
            var model4 = new StrategyPlanMulticlassClassificationsModel
            {
                Product = "MGS Zoo Tycoon 2: Extinct Animals 2008 E129",
                ProductSubcategory = "Download Games"
            };
            var model5 = new StrategyPlanMulticlassClassificationsModel
            {
                Product = "Litware Home Theater System 5.1 Channel M515 Black",
                ProductSubcategory = "Home Theater System"
            };

            productData.Add(model1);
            productData.Add(model2);
            productData.Add(model3);
            productData.Add(model4);
            productData.Add(model5);
        }

        private static void TestModel()
        {
            var context = new MLContext();
            var model = context.Model.Load(ModelPath, out _);
            var predictionEngine = context.Model.CreatePredictionEngine<StrategyPlanMulticlassClassificationsModel, StrategyPlanMulticlassClassificationsPredict>(model);
            foreach (var product in productData)
            {
                var prediction = predictionEngine.Predict(new StrategyPlanMulticlassClassificationsModel { Product = product.Product, ProductSubcategory = product.ProductSubcategory });
                Console.WriteLine(new string('-', 60));
                Console.WriteLine($"Product: {product.Product} | ProductSubcategory: {product.ProductSubcategory}");
                Console.WriteLine($"Prediction of Product Category: {prediction.ProductCategoryName}");
            }
        }

        private static void TrainModel()
        {
            var mlContext = new MLContext();

            string connectionString = @"Data Source=LAPTOP-ILQS92OM\SQLEXPRESS;Initial Catalog=ContosoRetailDW;Integrated Security=True;Pooling=False";

            string commandText = "SELECT ProductCategoryName,ProductSubcategory,Product,Region, IncomeGroup,CONVERT(varchar(5), Age) from V_CustomerOrders";

            DatabaseLoader loader = mlContext.Data.CreateDatabaseLoader<StrategyPlanMulticlassClassificationsModel>();

            DatabaseSource dbSource = new DatabaseSource(SqlClientFactory.Instance,
                                                         connectionString,
                                                         commandText);

            IDataView dataView = loader.Load(dbSource);
            //ConsoleHelper.ShowDataViewInConsole(mlContext, dataView, 500);
            //var trainTestData = mlContext.Data.TrainTestSplit(dataView, testFraction:0.1).TestSet;

            // Common data process configuration with pipeline data transformations
            Console.WriteLine("Map raw input data columns to ML.NET data");
            var dataProcessPipeline = mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(StrategyPlanMulticlassClassificationsModel.ProductCategoryName))
                .Append(mlContext.Transforms.Text.FeaturizeText("Features", nameof(StrategyPlanMulticlassClassificationsModel.Product))
                .Append(mlContext.Transforms.Text.FeaturizeText("Features", nameof(StrategyPlanMulticlassClassificationsModel.ProductSubcategory)))
                );

            // Create the selected training algorithm/trainer
            Console.WriteLine("Create and configure the selected training algorithm (trainer)");
            var trainer = mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy(); // SDCA = Stochastic Dual Coordinate Ascent
            //var trainer = mlContext.MulticlassClassification.Trainers.NaiveBayes();
            //var trainer = mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy();
            //var trainer = mlContext.MulticlassClassification.Trainers.SdcaNonCalibrated();
            // Alternative: LightGbm (GBM = Gradient Boosting Machine)

            // Set the trainer/algorithm and map label to value (original readable state)
            var trainingPipeline = dataProcessPipeline.Append(trainer).Append(
                mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            // Train the model fitting to the DataSet
            Console.WriteLine("Train the model fitting to the DataSet");
            var trainedModel = trainingPipeline.Fit(dataView);

            // Save/persist the trained model to a .ZIP file
            Console.WriteLine($"Save the model to a file ({ModelPath})");
            mlContext.Model.Save(trainedModel, dataView.Schema, ModelPath);
        }
    }
}
