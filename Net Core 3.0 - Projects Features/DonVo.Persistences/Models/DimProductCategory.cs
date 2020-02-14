using System;
using System.Collections.Generic;

namespace DonVo.Persistences.Models
{
    public partial class DimProductCategory
    {
        public DimProductCategory()
        {
            DimProductSubcategory = new HashSet<DimProductSubcategory>();
            FactStrategyPlan = new HashSet<FactStrategyPlan>();
        }

        public int ProductCategoryKey { get; set; }
        public string ProductCategoryLabel { get; set; }
        public string ProductCategoryName { get; set; }
        public string ProductCategoryDescription { get; set; }
        public int? EtlloadId { get; set; }
        public DateTime? LoadDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<DimProductSubcategory> DimProductSubcategory { get; set; }
        public virtual ICollection<FactStrategyPlan> FactStrategyPlan { get; set; }
    }
}
