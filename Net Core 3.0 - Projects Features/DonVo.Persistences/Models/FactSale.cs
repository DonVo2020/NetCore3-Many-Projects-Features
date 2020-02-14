using System;
using System.ComponentModel.DataAnnotations;

namespace DonVo.Persistences.Models
{
    public class FactSale
    {
        [Key]
        public int SalesKey { get; set; }
        public DateTime DateKey { get; set; }
        public int channelKey { get; set; }
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
        public int? ETLLoadID { get; set; }
        public DateTime? LoadDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual DimChannel DimChannel { get; set; }
        public virtual DimCurrency DimCurrency { get; set; }
        public virtual DimDate DimDate { get; set; }
        public virtual DimProduct DimProduct { get; set; }
        public virtual DimPromotion DimPromotion { get; set; }
        public virtual DimStore DimStore { get; set; }
    }
}
