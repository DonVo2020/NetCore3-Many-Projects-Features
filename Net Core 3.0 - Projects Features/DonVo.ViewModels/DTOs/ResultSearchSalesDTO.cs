using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.ViewModels.DTOs
{
    public class ResultSearchSalesDTO
    {
        public int SalesKey { get; set; }
        public DateTime DateKey { get; set; }
        public string ChannelName { get; set; }
        public string StoreName { get; set; }
        public string ProductName { get; set; }
        public string PromotionName { get; set; }
        public string CurrencyName { get; set; }
        public decimal UnitCost { get; set; }
        public decimal UnitPrice { get; set; }
        public int SalesQuantity { get; set; }
        public int ReturnQuantity { get; set; }
        public decimal? ReturnAmount { get; set; }
        public int? DiscountQuantity { get; set; }
        public decimal? DiscountAmount { get; set; }
        public decimal TotalCost { get; set; }
        public decimal SalesAmount { get; set; }
    }
}
