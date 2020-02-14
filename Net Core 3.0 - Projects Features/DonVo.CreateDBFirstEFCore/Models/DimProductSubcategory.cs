using System;
using System.Collections.Generic;

namespace DonVo.CreateDBFirstEFCore.Models
{
    public partial class DimProductSubcategory
    {
        public DimProductSubcategory()
        {
            DimProduct = new HashSet<DimProduct>();
        }

        public int ProductSubcategoryKey { get; set; }
        public string ProductSubcategoryLabel { get; set; }
        public string ProductSubcategoryName { get; set; }
        public string ProductSubcategoryDescription { get; set; }
        public int? ProductCategoryKey { get; set; }
        public int? EtlloadId { get; set; }
        public DateTime? LoadDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual DimProductCategory ProductCategoryKeyNavigation { get; set; }
        public virtual ICollection<DimProduct> DimProduct { get; set; }
    }
}
