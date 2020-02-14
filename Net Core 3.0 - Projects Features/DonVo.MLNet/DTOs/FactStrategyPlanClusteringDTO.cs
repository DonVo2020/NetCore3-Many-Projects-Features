using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.MLNet.DTOs
{
    public class FactStrategyPlanClusteringDTO
    {
        public int EntityKey { get; set; }
        public int ScenarioKey { get; set; }
        public int AccountKey { get; set; }
        public int ProductCategoryKey { get; set; }

        public string EntityName { get; set; }
        public string ScenarioName { get; set; }
        public string AccountName { get; set; }
        public string ProductCategoryName { get; set; }

        public double ActualAmount { get; set; }

        public uint SelectedClusterId { get; set; }
        public float[] AmountKeys;
    }
}
