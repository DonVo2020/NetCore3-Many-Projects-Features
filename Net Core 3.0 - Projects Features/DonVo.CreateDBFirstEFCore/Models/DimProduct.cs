using System;
using System.Collections.Generic;

namespace DonVo.CreateDBFirstEFCore.Models
{
    public partial class DimProduct
    {
        public DimProduct()
        {
            FactInventory = new HashSet<FactInventory>();
            FactOnlineSales = new HashSet<FactOnlineSales>();
            FactSales = new HashSet<FactSales>();
            FactSalesQuota = new HashSet<FactSalesQuota>();
        }

        public int ProductKey { get; set; }
        public string ProductLabel { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int? ProductSubcategoryKey { get; set; }
        public string Manufacturer { get; set; }
        public string BrandName { get; set; }
        public string ClassId { get; set; }
        public string ClassName { get; set; }
        public string StyleId { get; set; }
        public string StyleName { get; set; }
        public string ColorId { get; set; }
        public string ColorName { get; set; }
        public string Size { get; set; }
        public string SizeRange { get; set; }
        public string SizeUnitMeasureId { get; set; }
        public double? Weight { get; set; }
        public string WeightUnitMeasureId { get; set; }
        public string UnitOfMeasureId { get; set; }
        public string UnitOfMeasureName { get; set; }
        public string StockTypeId { get; set; }
        public string StockTypeName { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? UnitPrice { get; set; }
        public DateTime? AvailableForSaleDate { get; set; }
        public DateTime? StopSaleDate { get; set; }
        public string Status { get; set; }
        public string ImageUrl { get; set; }
        public string ProductUrl { get; set; }
        public int? EtlloadId { get; set; }
        public DateTime? LoadDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual DimProductSubcategory ProductSubcategoryKeyNavigation { get; set; }
        public virtual ICollection<FactInventory> FactInventory { get; set; }
        public virtual ICollection<FactOnlineSales> FactOnlineSales { get; set; }
        public virtual ICollection<FactSales> FactSales { get; set; }
        public virtual ICollection<FactSalesQuota> FactSalesQuota { get; set; }
    }
}
