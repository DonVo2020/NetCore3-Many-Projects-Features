using System;
using System.Collections.Generic;

namespace DonVo.CreateDBFirstEFCore.Models
{
    public partial class VOnlineSalesOrderDetail
    {
        public string OrderNumber { get; set; }
        public int? LineNumber { get; set; }
        public string Product { get; set; }
    }
}
