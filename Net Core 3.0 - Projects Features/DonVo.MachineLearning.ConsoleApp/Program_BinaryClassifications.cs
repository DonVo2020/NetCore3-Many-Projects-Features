using DonVo.MachineLearning.ConsoleApp.DataModels;
using Microsoft.Data.SqlClient;
using Microsoft.ML;
using Microsoft.ML.Data;
using System;

namespace DonVo.MachineLearning.ConsoleApp
{
    class Program_BinaryClassifications
    {
        private static string ModelPath = @"C:\DEVELOPMENT\NetCore30_StepsBySteps\DonVo.NetCore3\DonVo.MachineLearning.ConsoleApp\MLModels\CustomerPromotionClassificationModel.zip";

        static void Main_BinaryClassifications(string[] args)
        {
            var mlContext = new MLContext();

            string connectionString = @"Data Source=LAPTOP-ILQS92OM\SQLEXPRESS;Initial Catalog=ContosoRetailDW;Integrated Security=True;Pooling=False";

            string commandText = "SELECT CONVERT(real, Age),CONVERT(real, YearlyIncome),CONVERT(real, TotalChildren),CONVERT(real, NumberChildrenAtHome) ," +
                                    "CONVERT(real, NumberCarsOwned),CONVERT(real, ProductKey),CONVERT(real, PromotionKey)," +
                                    "CONVERT(bit, HouseOwnerFlag) FROM V_CustomerPromotion";

            DatabaseLoader loader = mlContext.Data.CreateDatabaseLoader<CustomerPromotionData>();

            DatabaseSource dbSource = new DatabaseSource(SqlClientFactory.Instance,
                                                         connectionString,
                                                         commandText);

            IDataView dataView = loader.Load(dbSource);
            //ConsoleHelper.ShowDataViewInConsole(mlContext, dataView, 500);
            var trainTestData = mlContext.Data.TrainTestSplit(dataView, testFraction:0.2).TestSet;

            //BuildTrainEvaluateAndSaveModel(mlContext, dataView, trainTestData);

            TestPrediction(mlContext);

            Console.WriteLine("=============== End of process, hit any key to finish ===============");
        }

        private static void BuildTrainEvaluateAndSaveModel(MLContext mlContext, IDataView dataView, IDataView trainTestData)
        {

            // STEP 2: Concatenate the features and set the training algorithm
            var pipeline = mlContext.Transforms.Concatenate("Features", "YearlyIncome","TotalChildren", "NumberCarsOwned")
                .Append(mlContext.BinaryClassification.Trainers.SdcaLogisticRegression(labelColumnName: "HouseOwnerFlag", featureColumnName: "Features"));

            Console.WriteLine("=============== Training the model ===============");
            ITransformer trainedModel = pipeline.Fit(dataView);
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("=============== Finish the train model. Push Enter ===============");
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("===== Evaluating Model's accuracy with Test data =====");
            var predictions = trainedModel.Transform(trainTestData);

            var metrics = mlContext.BinaryClassification.Evaluate(data: predictions, labelColumnName: "HouseOwnerFlag", scoreColumnName: "Score");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine($"************************************************************");
            Console.WriteLine($"*       Metrics for {trainedModel.ToString()} binary classification model      ");
            Console.WriteLine($"*-----------------------------------------------------------");
            Console.WriteLine($"*       Accuracy: {metrics.Accuracy:P2}");
            Console.WriteLine($"*       Area Under Roc Curve:      {metrics.AreaUnderRocCurve:P2}");
            Console.WriteLine($"*       Area Under PrecisionRecall Curve:  {metrics.AreaUnderPrecisionRecallCurve:P2}");
            Console.WriteLine($"*       F1Score:  {metrics.F1Score:P2}");
            Console.WriteLine($"*       LogLoss:  {metrics.LogLoss:#.##}");
            Console.WriteLine($"*       LogLossReduction:  {metrics.LogLossReduction:#.##}");
            Console.WriteLine($"*       PositivePrecision:  {metrics.PositivePrecision:#.##}");
            Console.WriteLine($"*       PositiveRecall:  {metrics.PositiveRecall:#.##}");
            Console.WriteLine($"*       NegativePrecision:  {metrics.NegativePrecision:#.##}");
            Console.WriteLine($"*       NegativeRecall:  {metrics.NegativeRecall:P2}");
            Console.WriteLine($"************************************************************");
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("=============== Saving the model to a file ===============");
            mlContext.Model.Save(trainedModel, dataView.Schema, ModelPath);
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("=============== Model Saved ============= ");
        }


        private static void TestPrediction(MLContext mlContext)
        {
            ITransformer trainedModel = mlContext.Model.Load(ModelPath, out var modelInputSchema);

            // Create prediction engine related to the loaded trained model
            var predictionEngine = mlContext.Model.CreatePredictionEngine<CustomerPromotionData, CustomerPromotionPredict>(trainedModel);

            foreach (var customerPromotionData in CustomerPromotionSampleData.customerPromotionDataList)
            {
                var prediction = predictionEngine.Predict(customerPromotionData);

                Console.WriteLine($"=============== Single Prediction  ===============");
                Console.WriteLine($"Age: {customerPromotionData.Age} ");
                //Console.WriteLine($"HouseOwnerFlag: {heartData.HouseOwnerFlag} ");
                Console.WriteLine($"TotalChildren: {customerPromotionData.TotalChildren} ");
                Console.WriteLine($"YearlyIncome: {customerPromotionData.YearlyIncome} ");
                Console.WriteLine($"NumberCarsOwned: {customerPromotionData.NumberCarsOwned} ");
                Console.WriteLine($"Prediction Value: {prediction.Prediction} ");
                Console.WriteLine($"Prediction: {(prediction.Prediction ? "Yes" : "No")} ");
                Console.WriteLine($"Probability: {prediction.Probability} ");
                Console.WriteLine($"==================================================");
                Console.WriteLine("");
                Console.WriteLine("");
            }

        }


    }
}
