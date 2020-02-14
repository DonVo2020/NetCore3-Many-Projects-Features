namespace DonVo.MLNet.Configurations
{
    public static class LoadingConfiguration
    {
        public const string ContosoRetailDWConnection = @"Data Source=LAPTOP-ILQS92OM\SQLEXPRESS;Initial Catalog=ContosoRetailDW;Integrated Security=True;Pooling=False";
        public const string CommandTextAnomalyDetections = "SELECT top 100000 EntityKey,ScenarioKey,AccountKey,ProductCategoryKey,CONVERT(real, Amount * 100) from FactStrategyPlan";
        public const string CommandTextMulticlassClassifications = "SELECT ProductCategoryName,ProductSubcategory,Product,Region, IncomeGroup,CONVERT(varchar(5), Age) from V_CustomerOrders";
        public const string CommandTextClustering = "SELECT CONVERT(real, EntityKey), CONVERT(real, ScenarioKey),CONVERT(real, AccountKey), CONVERT(real, ProductCategoryKey),CONVERT(real, Amount) from FactStrategyPlan";


        public const string CustomerOrdersLbfgsMaximumEntropyModelPath = @"C:\DEVELOPMENT\NetCore30_StepsBySteps\DonVo.NetCore3\DonVo.MLNet\MLModels\MulticlassClassifications\CustomerOrdersLbfgsMaximumEntropyModel.zip";
        public const string CustomerOrdersNaiveBayesModelPath = @"C:\DEVELOPMENT\NetCore30_StepsBySteps\DonVo.NetCore3\DonVo.MLNet\MLModels\MulticlassClassifications\CustomerOrdersNaiveBayesModel.zip";
        public const string CustomerOrdersSdcaMaximumEntropyModelPath = @"C:\DEVELOPMENT\NetCore30_StepsBySteps\DonVo.NetCore3\DonVo.MLNet\MLModels\MulticlassClassifications\CustomerOrdersSdcaMaximumEntropyModel.zip";
        public const string CustomerOrdersSdcaNonCalibratedModelPath = @"C:\DEVELOPMENT\NetCore30_StepsBySteps\DonVo.NetCore3\DonVo.MLNet\MLModels\MulticlassClassifications\CustomerOrdersSdcaNonCalibratedModel.zip";
        public const string FactStrategyPlanClusteringModelPath = @"C:\DEVELOPMENT\NetCore30_StepsBySteps\DonVo.NetCore3\DonVo.MLNet\MLModels\Clustering\FactStrategyPlanClusteringModel.zip";
    }
}
