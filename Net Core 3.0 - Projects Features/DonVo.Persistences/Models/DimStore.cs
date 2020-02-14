using System;
using System.Collections.Generic;

namespace DonVo.Persistences.Models
{
    public partial class DimStore
    {
        public DimStore()
        {
            DimMachine = new HashSet<DimMachine>();
            FactInventory = new HashSet<FactInventory>();
            FactItsla = new HashSet<FactItsla>();
            FactOnlineSales = new HashSet<FactOnlineSales>();
            FactSales = new HashSet<FactSales>();
            FactSalesQuota = new HashSet<FactSalesQuota>();
        }

        public int StoreKey { get; set; }
        public int GeographyKey { get; set; }
        public int? StoreManager { get; set; }
        public string StoreType { get; set; }
        public string StoreName { get; set; }
        public string StoreDescription { get; set; }
        public string Status { get; set; }
        public DateTime OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public int? EntityKey { get; set; }
        public string ZipCode { get; set; }
        public string ZipCodeExtension { get; set; }
        public string StorePhone { get; set; }
        public string StoreFax { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string CloseReason { get; set; }
        public int? EmployeeCount { get; set; }
        public double? SellingAreaSize { get; set; }
        public DateTime? LastRemodelDate { get; set; }
        public int? EtlloadId { get; set; }
        public DateTime? LoadDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual DimGeography GeographyKeyNavigation { get; set; }
        public virtual ICollection<DimMachine> DimMachine { get; set; }
        public virtual ICollection<FactInventory> FactInventory { get; set; }
        public virtual ICollection<FactItsla> FactItsla { get; set; }
        public virtual ICollection<FactOnlineSales> FactOnlineSales { get; set; }
        public virtual ICollection<FactSales> FactSales { get; set; }
        public virtual ICollection<FactSalesQuota> FactSalesQuota { get; set; }
    }
}
