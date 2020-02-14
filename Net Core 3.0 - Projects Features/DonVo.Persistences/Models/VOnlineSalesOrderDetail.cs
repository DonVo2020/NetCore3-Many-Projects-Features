using System;
using System.Collections.Generic;

namespace DonVo.Persistences.Models
{
    public partial class VOnlineSalesOrderDetail
    {
        public string OrderNumber { get; set; }
        public int? LineNumber { get; set; }
        public string Product { get; set; }
    }
}
