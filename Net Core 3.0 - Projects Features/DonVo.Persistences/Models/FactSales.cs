using System;
using System.Collections.Generic;

namespace DonVo.Persistences.Models
{
    public partial class FactSales
    {
        public int SalesKey { get; set; }
        public DateTime DateKey { get; set; }
        public int ChannelKey { get; set; }
        public int StoreKey { get; set; }
        public int ProductKey { get; set; }
        public int PromotionKey { get; set; }
        public int CurrencyKey { get; set; }
        public decimal UnitCost { get; set; }
        public decimal UnitPrice { get; set; }
        public int SalesQuantity { get; set; }
        public int ReturnQuantity { get; set; }
        public decimal? ReturnAmount { get; set; }
        public int? DiscountQuantity { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal TotalCost { get; set; }
        public decimal SalesAmount { get; set; }
        public int? EtlloadId { get; set; }
        public DateTime? LoadDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual DimChannel ChannelKeyNavigation { get; set; }
        public virtual DimCurrency CurrencyKeyNavigation { get; set; }
        public virtual DimDate DateKeyNavigation { get; set; }
        public virtual DimProduct ProductKeyNavigation { get; set; }
        public virtual DimPromotion PromotionKeyNavigation { get; set; }
        public virtual DimStore StoreKeyNavigation { get; set; }
    }
}
