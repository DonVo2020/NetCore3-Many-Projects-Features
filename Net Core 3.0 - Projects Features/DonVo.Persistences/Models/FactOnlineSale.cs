using System;
using System.ComponentModel.DataAnnotations;

namespace DonVo.Persistences.Models
{
    public class FactOnlineSale
    {
        [Key]
        public int OnlineSalesKey { get; set; }
        public DateTime DateKey { get; set; }
        public int StoreKey { get; set; }
        public int ProductKey { get; set; }
        public int PromotionKey { get; set; }
        public int CurrencyKey { get; set; }
        public int CustomerKey { get; set; }
        public string SalesOrderNumber { get; set; }
        public int? SalesOrderLineNumber { get; set; }
        public int SalesQuantity { get; set; }
        public decimal SalesAmount { get; set; }
        public int ReturnQuantity { get; set; }
        public decimal? ReturnAmount { get; set; }
        public int? DiscountQuantity { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal TotalCost { get; set; }
        public decimal? UnitCost { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? ETLLoadID { get; set; }
        public DateTime? LoadDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual DimCurrency DimCurrency { get; set; }
        public virtual DimCustomer DimCustomer { get; set; }
        public virtual DimDate DimDate { get; set; }
        public virtual DimProduct DimProduct { get; set; }
        public virtual DimPromotion DimPromotion { get; set; }
        public virtual DimStore DimStore { get; set; }
    }
}
