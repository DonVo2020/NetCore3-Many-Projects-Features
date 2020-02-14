using DonVo.MLNet.Configurations;
using DonVo.MLNet.DataModels;
using DonVo.Persistences;
using DonVo.Services.ActualResults;
using Microsoft.Data.SqlClient;
using Microsoft.ML;
using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DonVo.MLNet.Services
{
    public class AnomalyDetectionsService<T> : IAnomalyDetectionsService<T> where T:class
    {
        private readonly ContosoRetailDWContext _context;
        private MLContext _mlContext;

        public AnomalyDetectionsService(ContosoRetailDWContext context)
        {
            _context = context;
        }

        public ActualResult<IDataView> LoadData()
        {
            _mlContext = new MLContext();

            //STEP 1: Load data from SQL Server into IDataView 
            string connectionString = LoadingConfiguration.ContosoRetailDWConnection;
            string commandText = LoadingConfiguration.CommandTextAnomalyDetections;

            DatabaseLoader loader = _mlContext.Data.CreateDatabaseLoader<FactStrategyPlanAnomalyDetections>();

            DatabaseSource dbSource = new DatabaseSource(SqlClientFactory.Instance,
                                                         connectionString,
                                                         commandText);

            IDataView dataView = loader.Load(dbSource);
            return new ActualResult<IDataView> { Result = dataView };
        }

        public Task<ActualResult<T>> GetStatisticsResult(T statisticObject)
        {
            throw new NotImplementedException();
        }

        public Task<ActualResult<IEnumerable<T>>> GetTableResult(T table)
        {
            throw new NotImplementedException();
        }

        // Detect temporary changes in pattern
        public IEnumerable<FactStrategyPlanAnomalyDetectionsPrediction> DetectSpike(int size, IDataView dataView)
        {
            //STEP 2: Set the training algorithm    
            var trainingPipeLine = _mlContext.Transforms.DetectIidSpike(outputColumnName: nameof(FactStrategyPlanAnomalyDetectionsPrediction.Prediction), inputColumnName: nameof(FactStrategyPlanAnomalyDetectionsData.Amount), confidence: 95, pvalueHistoryLength: size / 4);

            //STEP 3:Train the model by fitting the dataview
            ITransformer trainedModel = trainingPipeLine.Fit(dataView);

            //STEP 4: Apply data transformation to create predictions.
            IDataView transformedData = trainedModel.Transform(dataView);
            var predictions = _mlContext.Data.CreateEnumerable<FactStrategyPlanAnomalyDetectionsPrediction>(transformedData, reuseRowObject: false);

            return predictions;
        }

        // Detect Persistent changes in pattern
        public IEnumerable<FactStrategyPlanAnomalyDetectionsPrediction> DetectChangePoint(int size, IDataView dataView)
        {
            //STEP 2: Set the training algorithm    
            var trainingPipeLine = _mlContext.Transforms.DetectIidChangePoint(outputColumnName: nameof(FactStrategyPlanAnomalyDetectionsPrediction.Prediction), inputColumnName: nameof(FactStrategyPlanAnomalyDetectionsData.Amount), confidence: 95, changeHistoryLength: size / 4);

            //STEP 3:Train the model by fitting the dataview
            ITransformer trainedModel = trainingPipeLine.Fit(dataView);

            //Apply data transformation to create predictions.
            IDataView transformedData = trainedModel.Transform(dataView);
            var predictions = _mlContext.Data.CreateEnumerable<FactStrategyPlanAnomalyDetectionsPrediction>(transformedData, reuseRowObject: false);

            return predictions;
        }

        public void Dispose()
        {
            _context.Dispose();
        }     
    }
}
