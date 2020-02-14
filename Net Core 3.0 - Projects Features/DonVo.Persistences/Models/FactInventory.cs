using System;
using System.Collections.Generic;

namespace DonVo.Persistences.Models
{
    public partial class FactInventory
    {
        public int InventoryKey { get; set; }
        public DateTime DateKey { get; set; }
        public int StoreKey { get; set; }
        public int ProductKey { get; set; }
        public int CurrencyKey { get; set; }
        public int OnHandQuantity { get; set; }
        public int OnOrderQuantity { get; set; }
        public int? SafetyStockQuantity { get; set; }
        public decimal UnitCost { get; set; }
        public int? DaysInStock { get; set; }
        public int? MinDayInStock { get; set; }
        public int? MaxDayInStock { get; set; }
        public int? Aging { get; set; }
        public int? EtlloadId { get; set; }
        public DateTime? LoadDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual DimCurrency CurrencyKeyNavigation { get; set; }
        public virtual DimDate DateKeyNavigation { get; set; }
        public virtual DimProduct ProductKeyNavigation { get; set; }
        public virtual DimStore StoreKeyNavigation { get; set; }
    }
}
