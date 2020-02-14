using System;
using System.Collections.Generic;

namespace DonVo.CreateDBFirstEFCore.Models
{
    public partial class DimCurrency
    {
        public DimCurrency()
        {
            FactExchangeRate = new HashSet<FactExchangeRate>();
            FactInventory = new HashSet<FactInventory>();
            FactOnlineSales = new HashSet<FactOnlineSales>();
            FactSales = new HashSet<FactSales>();
            FactSalesQuota = new HashSet<FactSalesQuota>();
            FactStrategyPlan = new HashSet<FactStrategyPlan>();
        }

        public int CurrencyKey { get; set; }
        public string CurrencyLabel { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyDescription { get; set; }
        public int? EtlloadId { get; set; }
        public DateTime? LoadDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<FactExchangeRate> FactExchangeRate { get; set; }
        public virtual ICollection<FactInventory> FactInventory { get; set; }
        public virtual ICollection<FactOnlineSales> FactOnlineSales { get; set; }
        public virtual ICollection<FactSales> FactSales { get; set; }
        public virtual ICollection<FactSalesQuota> FactSalesQuota { get; set; }
        public virtual ICollection<FactStrategyPlan> FactStrategyPlan { get; set; }
    }
}
