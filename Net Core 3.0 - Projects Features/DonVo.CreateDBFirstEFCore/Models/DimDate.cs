using System;
using System.Collections.Generic;

namespace DonVo.CreateDBFirstEFCore.Models
{
    public partial class DimDate
    {
        public DimDate()
        {
            FactExchangeRate = new HashSet<FactExchangeRate>();
            FactInventory = new HashSet<FactInventory>();
            FactItmachine = new HashSet<FactItmachine>();
            FactItsla = new HashSet<FactItsla>();
            FactOnlineSales = new HashSet<FactOnlineSales>();
            FactSales = new HashSet<FactSales>();
            FactSalesQuota = new HashSet<FactSalesQuota>();
            FactStrategyPlan = new HashSet<FactStrategyPlan>();
        }

        public DateTime Datekey { get; set; }
        public string FullDateLabel { get; set; }
        public string DateDescription { get; set; }
        public int CalendarYear { get; set; }
        public string CalendarYearLabel { get; set; }
        public int CalendarHalfYear { get; set; }
        public string CalendarHalfYearLabel { get; set; }
        public int CalendarQuarter { get; set; }
        public string CalendarQuarterLabel { get; set; }
        public int CalendarMonth { get; set; }
        public string CalendarMonthLabel { get; set; }
        public int CalendarWeek { get; set; }
        public string CalendarWeekLabel { get; set; }
        public int CalendarDayOfWeek { get; set; }
        public string CalendarDayOfWeekLabel { get; set; }
        public int FiscalYear { get; set; }
        public string FiscalYearLabel { get; set; }
        public int FiscalHalfYear { get; set; }
        public string FiscalHalfYearLabel { get; set; }
        public int FiscalQuarter { get; set; }
        public string FiscalQuarterLabel { get; set; }
        public int FiscalMonth { get; set; }
        public string FiscalMonthLabel { get; set; }
        public string IsWorkDay { get; set; }
        public int IsHoliday { get; set; }
        public string HolidayName { get; set; }
        public string EuropeSeason { get; set; }
        public string NorthAmericaSeason { get; set; }
        public string AsiaSeason { get; set; }

        public virtual ICollection<FactExchangeRate> FactExchangeRate { get; set; }
        public virtual ICollection<FactInventory> FactInventory { get; set; }
        public virtual ICollection<FactItmachine> FactItmachine { get; set; }
        public virtual ICollection<FactItsla> FactItsla { get; set; }
        public virtual ICollection<FactOnlineSales> FactOnlineSales { get; set; }
        public virtual ICollection<FactSales> FactSales { get; set; }
        public virtual ICollection<FactSalesQuota> FactSalesQuota { get; set; }
        public virtual ICollection<FactStrategyPlan> FactStrategyPlan { get; set; }
    }
}
