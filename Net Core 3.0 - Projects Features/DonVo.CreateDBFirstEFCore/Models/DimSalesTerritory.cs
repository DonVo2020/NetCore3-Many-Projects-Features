using System;
using System.Collections.Generic;

namespace DonVo.CreateDBFirstEFCore.Models
{
    public partial class DimSalesTerritory
    {
        public int SalesTerritoryKey { get; set; }
        public int GeographyKey { get; set; }
        public string SalesTerritoryLabel { get; set; }
        public string SalesTerritoryName { get; set; }
        public string SalesTerritoryRegion { get; set; }
        public string SalesTerritoryCountry { get; set; }
        public string SalesTerritoryGroup { get; set; }
        public string SalesTerritoryLevel { get; set; }
        public int? SalesTerritoryManager { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public int? EtlloadId { get; set; }
        public DateTime? LoadDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual DimGeography GeographyKeyNavigation { get; set; }
    }
}
