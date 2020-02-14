using System;
using System.Collections.Generic;

namespace DonVo.CreateDBFirstEFCore.Models
{
    public partial class DimGeography
    {
        public DimGeography()
        {
            DimCustomer = new HashSet<DimCustomer>();
            DimSalesTerritory = new HashSet<DimSalesTerritory>();
            DimStore = new HashSet<DimStore>();
        }

        public int GeographyKey { get; set; }
        public string GeographyType { get; set; }
        public string ContinentName { get; set; }
        public string CityName { get; set; }
        public string StateProvinceName { get; set; }
        public string RegionCountryName { get; set; }
        public int? EtlloadId { get; set; }
        public DateTime? LoadDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<DimCustomer> DimCustomer { get; set; }
        public virtual ICollection<DimSalesTerritory> DimSalesTerritory { get; set; }
        public virtual ICollection<DimStore> DimStore { get; set; }
    }
}
