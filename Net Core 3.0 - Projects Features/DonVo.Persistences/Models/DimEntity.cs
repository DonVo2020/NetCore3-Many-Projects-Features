using System;
using System.Collections.Generic;

namespace DonVo.Persistences.Models
{
    public partial class DimEntity
    {
        public DimEntity()
        {
            FactStrategyPlan = new HashSet<FactStrategyPlan>();
        }

        public int EntityKey { get; set; }
        public string EntityLabel { get; set; }
        public int? ParentEntityKey { get; set; }
        public string ParentEntityLabel { get; set; }
        public string EntityName { get; set; }
        public string EntityDescription { get; set; }
        public string EntityType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public int? EtlloadId { get; set; }
        public DateTime? LoadDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<FactStrategyPlan> FactStrategyPlan { get; set; }
    }
}
