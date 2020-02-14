using System;
using System.Collections.Generic;

namespace DonVo.CreateDBFirstEFCore.Models
{
    public partial class FactSalesQuota
    {
        public int SalesQuotaKey { get; set; }
        public int ChannelKey { get; set; }
        public int StoreKey { get; set; }
        public int ProductKey { get; set; }
        public DateTime DateKey { get; set; }
        public int CurrencyKey { get; set; }
        public int ScenarioKey { get; set; }
        public decimal SalesQuantityQuota { get; set; }
        public decimal SalesAmountQuota { get; set; }
        public decimal GrossMarginQuota { get; set; }
        public int? EtlloadId { get; set; }
        public DateTime? LoadDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual DimChannel ChannelKeyNavigation { get; set; }
        public virtual DimCurrency CurrencyKeyNavigation { get; set; }
        public virtual DimDate DateKeyNavigation { get; set; }
        public virtual DimProduct ProductKeyNavigation { get; set; }
        public virtual DimScenario ScenarioKeyNavigation { get; set; }
        public virtual DimStore StoreKeyNavigation { get; set; }
    }
}
