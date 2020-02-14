using System;
using System.Collections.Generic;

namespace DonVo.Persistences.Models
{
    public partial class DimScenario
    {
        public DimScenario()
        {
            FactSalesQuota = new HashSet<FactSalesQuota>();
            FactStrategyPlan = new HashSet<FactStrategyPlan>();
        }

        public int ScenarioKey { get; set; }
        public string ScenarioLabel { get; set; }
        public string ScenarioName { get; set; }
        public string ScenarioDescription { get; set; }
        public int? EtlloadId { get; set; }
        public DateTime? LoadDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<FactSalesQuota> FactSalesQuota { get; set; }
        public virtual ICollection<FactStrategyPlan> FactStrategyPlan { get; set; }
    }
}
