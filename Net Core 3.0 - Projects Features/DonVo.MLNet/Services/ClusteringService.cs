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
    public class ClusteringService<T> : IClusteringService<T> where T:class
    {
        private readonly ContosoRetailDWContext _context;
        private MLContext _mlContext;

        private FactStrategyPlanClusteringDTO _factStrategyPlanClusteringDTO;

        public ClusteringService(ContosoRetailDWContext context)
        {
            _context = context;
            _factStrategyPlanClusteringDTO = new FactStrategyPlanClusteringDTO();
        }

        public ActualResult<IDataView> LoadData()
        {
            _mlContext = new MLContext();

            //STEP 1: Load data from SQL Server into IDataView 
            string connectionString = LoadingConfiguration.ContosoRetailDWConnection;
            string commandText = LoadingConfiguration.CommandTextClustering;

            DatabaseLoader loader = _mlContext.Data.CreateDatabaseLoader<FactStrategyPlanClusteringData>();

            DatabaseSource dbSource = new DatabaseSource(SqlClientFactory.Instance,
                                                         connectionString,
                                                         commandText);

            IDataView dataView = loader.Load(dbSource);
            return new ActualResult<IDataView> { Result = dataView };
        }

        public void TrainModel(IDataView dataView)
        {
            //STEP 2: Process data transformations in pipeline
            var dataProcessPipeline = _mlContext.Transforms.Concatenate("Features", nameof(FactStrategyPlanClusteringData.EntityKey), 
                                                                                    nameof(FactStrategyPlanClusteringData.ScenarioKey), 
                                                                                    nameof(FactStrategyPlanClusteringData.AccountKey),
                                                                                    nameof(FactStrategyPlanClusteringData.ProductCategoryKey));

            // STEP 3: Create and train the model     
            var trainer = _mlContext.Clustering.Trainers.KMeans(featureColumnName: "Features", numberOfClusters: 10);
            var trainingPipeline = dataProcessPipeline.Append(trainer);
            var trainedModel = trainingPipeline.Fit(dataView);

            // STEP4: Evaluate accuracy of the model
            IDataView predictions = trainedModel.Transform(dataView);
            var metrics = _mlContext.Clustering.Evaluate(predictions, scoreColumnName: "Score", featureColumnName: "Features");

            //STEP5: Save / persist the model as a.ZIP file
            _mlContext.Model.Save(trainedModel, dataView.Schema, LoadingConfiguration.FactStrategyPlanClusteringModelPath);
        }

        public FactStrategyPlanClusteringDTO GetFactStrategyPlanClusteringDTO(int entityKey, int scenarioKey, int accountKey, int productCategoryKey)
        {
            _factStrategyPlanClusteringDTO.EntityKey = entityKey;
            _factStrategyPlanClusteringDTO.ScenarioKey = scenarioKey;
            _factStrategyPlanClusteringDTO.AccountKey = accountKey;
            _factStrategyPlanClusteringDTO.ProductCategoryKey = productCategoryKey;

            _factStrategyPlanClusteringDTO.EntityName = _context.DimEntity.Where(x => x.EntityKey == entityKey).First().EntityName;
            _factStrategyPlanClusteringDTO.ScenarioName = _context.DimScenario.Where(x => x.ScenarioKey == scenarioKey).First().ScenarioName;
            _factStrategyPlanClusteringDTO.AccountName = _context.DimAccount.Where(x => x.AccountKey == accountKey).First().AccountName;
            _factStrategyPlanClusteringDTO.ProductCategoryName = _context.DimProductCategory.Where(x => x.ProductCategoryKey == productCategoryKey).First().ProductCategoryName;

            var amount = _context.FactStrategyPlan
                .Where(x => x.EntityKey == entityKey && x.AccountKey == accountKey && x.ScenarioKey == scenarioKey && x.ProductCategoryKey == productCategoryKey)
                .GroupBy(i => 1)
                    .Select(g => new {
                        Count = g.Count(),
                        Total = g.Sum(i => Convert.ToDecimal(i.Amount)),
                        Average = g.Average(i => Convert.ToDecimal(i.Amount))
                    }).FirstOrDefault();

            _factStrategyPlanClusteringDTO.ActualAmount = Convert.ToDouble(amount.Average);

            return _factStrategyPlanClusteringDTO;
        }

        public FactStrategyPlanClusteringDTO TestModelKMeans(FactStrategyPlanClusteringDTO factStrategyPlanClusteringDTO)
        {
            _mlContext = new MLContext();

            var sampleFactStrategyPlanClusteringData = new FactStrategyPlanClusteringData()
            {
                EntityKey = factStrategyPlanClusteringDTO.EntityKey,
                ScenarioKey = factStrategyPlanClusteringDTO.ScenarioKey,
                AccountKey = factStrategyPlanClusteringDTO.AccountKey,
                ProductCategoryKey = factStrategyPlanClusteringDTO.ProductCategoryKey
            };

            ITransformer model = _mlContext.Model.Load(LoadingConfiguration.FactStrategyPlanClusteringModelPath, out var modelInputSchema);
            // Create prediction engine related to the loaded trained model
            var predEngine = _mlContext.Model.CreatePredictionEngine<FactStrategyPlanClusteringData, FactStrategyPlanClusteringPredict>(model);

            //Score
            var result = predEngine.Predict(sampleFactStrategyPlanClusteringData);
            factStrategyPlanClusteringDTO.AmountKeys = result.Amount;
            factStrategyPlanClusteringDTO.SelectedClusterId = result.SelectedClusterId;

            predEngine.Dispose();

            return factStrategyPlanClusteringDTO;
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
