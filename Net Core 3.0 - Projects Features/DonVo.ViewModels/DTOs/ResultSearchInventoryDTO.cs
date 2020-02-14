using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.ViewModels.DTOs
{
    public class ResultSearchInventoryDTO
    {
        public int InventoryKey { get; set; }
        public string ProductName { get; set; }
        public string CurrencyName { get; set; }
        public string StoreName { get; set; }
        public int OnHandQuantity { get; set; }
        public int OnOrderQuantity { get; set; }
        public decimal UnitCost { get; set; }
        public int? DaysInStock { get; set; }
        public int? MinDayInStock { get; set; }
        public int? MaxDayInStock { get; set; }
        public DateTime DateKey { get; set; }
    }
}
