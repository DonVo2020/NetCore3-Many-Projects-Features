using System;
using System.Collections.Generic;

namespace DonVo.Persistences.Models
{
    public partial class VOnlineSalesOrder
    {
        public string OrderNumber { get; set; }
        public int CustomerKey { get; set; }
        public string Region { get; set; }
        public string IncomeGroup { get; set; }
    }
}
