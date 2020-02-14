using DonVo.MLNet.Configurations;
using DonVo.MLNet.DataModels;
using DonVo.MLNet.DTOs;
using DonVo.Persistences;
using DonVo.Services.ActualResults;
using Microsoft.Data.SqlClient;
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonVo.MLNet.Services
{
    public class MulticlassClassificationsService<T> : IMulticlassClassificationsService<T> where T: CustomerOrdersMulticlassClassificationsModel
    {
        private readonly ContosoRetailDWContext _context;
        private MLContext _mlContext;
        private CustomerOrdersMulticlassClassificationsDTO _customerOrdersMulticlassClassificationsDTO;

        public MulticlassClassificationsService(ContosoRetailDWContext context)
        {
            _context = context;
            _customerOrdersMulticlassClassificationsDTO = new CustomerOrdersMulticlassClassificationsDTO();
        }

        public ActualResult<IDataView> LoadData()
        {
            _mlContext = new MLContext();

            string connectionString = LoadingConfiguration.ContosoRetailDWConnection;

            string commandText = LoadingConfiguration.CommandTextMulticlassClassifications;

            DatabaseLoader loader = _mlContext.Data.CreateDatabaseLoader<CustomerOrdersMulticlassClassificationsModel>();

            DatabaseSource dbSource = new DatabaseSource(SqlClientFactory.Instance,
                                                         connectionString,
                                                         commandText);

            IDataView dataView = loader.Load(dbSource);
            return new ActualResult<IDataView> { Result = dataView };
        }

        public void TrainModel(IDataView dataView)
        {
            // Common data process configuration with pipeline data transformations
            var dataProcessPipeline = _mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(CustomerOrdersMulticlassClassificationsModel.ProductCategoryName))
                .Append(_mlContext.Transforms.Text.FeaturizeText("Features", nameof(CustomerOrdersMulticlassClassificationsModel.Product))
                .Append(_mlContext.Transforms.Text.FeaturizeText("Features", nameof(CustomerOrdersMulticlassClassificationsModel.ProductSubcategory)))
                );

            // Create the selected training algorithm/trainer
            var trainerSdcaMaximumEntropy = _mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy(); // SDCA = Stochastic Dual Coordinate Ascent
            //var trainerNaiveBayes = _mlContext.MulticlassClassification.Trainers.NaiveBayes();
            //var trainerLbfgsMaximumEntropy = _mlContext.MulticlassClassification.Trainers.LbfgsMaximumEntropy();
            //var trainerSdcaNonCalibrated = _mlContext.MulticlassClassification.Trainers.SdcaNonCalibrated();
            // Alternative: LightGbm (GBM = Gradient Boosting Machine)

            // Set the trainer/algorithm and map label to value (original readable state)
            var trainingPipeline = dataProcessPipeline.Append(trainerSdcaMaximumEntropy).Append(
                _mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            // Train the model fitting to the DataSet
            var trainedModel = trainingPipeline.Fit(dataView);

            // Save/persist the trained model to a .ZIP file
            _mlContext.Model.Save(trainedModel, dataView.Schema, LoadingConfiguration.CustomerOrdersSdcaNonCalibratedModelPath);
        }

        public CustomerOrdersMulticlassClassificationsDTO GetMulticlassClassificationsDTO(int productKey, int productSubcategoryKey)
        {
            _customerOrdersMulticlassClassificationsDTO.ProductName = _context.DimProduct.Where(x => x.ProductKey == productKey).First().ProductName;
            _customerOrdersMulticlassClassificationsDTO.ProductSubcategoryName = _context.DimProductSubcategory.Where(x => x.ProductSubcategoryKey == productSubcategoryKey).First().ProductSubcategoryName;
            return _customerOrdersMulticlassClassificationsDTO;
        }

        public CustomerOrdersMulticlassClassificationsPredict TestModelLbfgsMaximumEntropy(CustomerOrdersMulticlassClassificationsDTO customerOrdersMulticlassClassificationsDTO)
        {
            var context = new MLContext();

            var modelLbfgsMaximumEntropy = context.Model.Load(LoadingConfiguration.CustomerOrdersLbfgsMaximumEntropyModelPath, out _);
            var predictionEngineLbfgsMaximumEntropy = context.Model.CreatePredictionEngine<CustomerOrdersMulticlassClassificationsModel, CustomerOrdersMulticlassClassificationsPredict>(modelLbfgsMaximumEntropy);
            var predictionLbfgsMaximumEntropy = predictionEngineLbfgsMaximumEntropy.Predict(new CustomerOrdersMulticlassClassificationsModel 
                                                {   Product = customerOrdersMulticlassClassificationsDTO.ProductName, 
                                                    ProductSubcategory = customerOrdersMulticlassClassificationsDTO.ProductSubcategoryName
                                                });

            return predictionLbfgsMaximumEntropy;
        }

        public CustomerOrdersMulticlassClassificationsPredict TestModelNaiveBayes(CustomerOrdersMulticlassClassificationsDTO customerOrdersMulticlassClassificationsDTO)
        {
            var context = new MLContext();

            var modelNaiveBayes = context.Model.Load(LoadingConfiguration.CustomerOrdersLbfgsMaximumEntropyModelPath, out _);
            var predictionEngineNaiveBayes = context.Model.CreatePredictionEngine<CustomerOrdersMulticlassClassificationsModel, CustomerOrdersMulticlassClassificationsPredict>(modelNaiveBayes);
            var predictionNaiveBayes = predictionEngineNaiveBayes.Predict(new CustomerOrdersMulticlassClassificationsModel
                                        {
                                            Product = customerOrdersMulticlassClassificationsDTO.ProductName,
                                            ProductSubcategory = customerOrdersMulticlassClassificationsDTO.ProductSubcategoryName
                                        });

            return predictionNaiveBayes;
        }

        public CustomerOrdersMulticlassClassificationsPredict TestModelSdcaMaximumEntropy(CustomerOrdersMulticlassClassificationsDTO customerOrdersMulticlassClassificationsDTO)
        {
            var context = new MLContext();

            var modelSdcaMaximumEntropy = context.Model.Load(LoadingConfiguration.CustomerOrdersLbfgsMaximumEntropyModelPath, out _);
            var predictionEngineSdcaMaximumEntropy = context.Model.CreatePredictionEngine<CustomerOrdersMulticlassClassificationsModel, CustomerOrdersMulticlassClassificationsPredict>(modelSdcaMaximumEntropy);
            var predictionSdcaMaximumEntropy = predictionEngineSdcaMaximumEntropy.Predict(new CustomerOrdersMulticlassClassificationsModel
                                                {
                                                    Product = customerOrdersMulticlassClassificationsDTO.ProductName,
                                                    ProductSubcategory = customerOrdersMulticlassClassificationsDTO.ProductSubcategoryName
                                                });

            return predictionSdcaMaximumEntropy;
        }

        public CustomerOrdersMulticlassClassificationsPredict TestModelSdcaNonCalibrated(CustomerOrdersMulticlassClassificationsDTO customerOrdersMulticlassClassificationsDTO)
        {
            var context = new MLContext();

            var modelSdcaNonCalibrated = context.Model.Load(LoadingConfiguration.CustomerOrdersLbfgsMaximumEntropyModelPath, out _);
            var predictionEngineSdcaNonCalibrated = context.Model.CreatePredictionEngine<CustomerOrdersMulticlassClassificationsModel, CustomerOrdersMulticlassClassificationsPredict>(modelSdcaNonCalibrated);
            var predictionSdcaNonCalibrated = predictionEngineSdcaNonCalibrated.Predict(new CustomerOrdersMulticlassClassificationsModel
                                                {
                                                    Product = customerOrdersMulticlassClassificationsDTO.ProductName,
                                                    ProductSubcategory = customerOrdersMulticlassClassificationsDTO.ProductSubcategoryName
                                                });

            return predictionSdcaNonCalibrated;
        }

        public Task<ActualResult<T>> GetStatisticsResult(T statisticObject)
        {
            throw new NotImplementedException();
        }

        public Task<ActualResult<IEnumerable<T>>> GetTableResult(T table)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
