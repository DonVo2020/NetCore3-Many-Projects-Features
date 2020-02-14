using System;
using System.Collections.Generic;

namespace DonVo.CreateDBFirstEFCore.Models
{
    public partial class DimChannel
    {
        public DimChannel()
        {
            FactSales = new HashSet<FactSales>();
            FactSalesQuota = new HashSet<FactSalesQuota>();
        }

        public int ChannelKey { get; set; }
        public string ChannelLabel { get; set; }
        public string ChannelName { get; set; }
        public string ChannelDescription { get; set; }
        public int? EtlloadId { get; set; }
        public DateTime? LoadDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<FactSales> FactSales { get; set; }
        public virtual ICollection<FactSalesQuota> FactSalesQuota { get; set; }
    }
}
