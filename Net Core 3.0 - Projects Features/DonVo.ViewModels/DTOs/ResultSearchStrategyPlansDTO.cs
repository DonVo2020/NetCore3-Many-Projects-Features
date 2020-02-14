using System;
using System.Collections.Generic;
using System.Text;

namespace DonVo.ViewModels.DTOs
{
    public class ResultSearchStrategyPlansDTO
    {
        public int StrategyPlanKey { get; set; }
        public DateTime Datekey { get; set; }
        public string EntityName { get; set; }
        public string ScenarioName { get; set; }
        public string AccountName { get; set; }
        public string CurrencyName { get; set; }
        public string ProductCategoryName { get; set; }
        public double Amount { get; set; }
    }
}
