using System;
using System.Collections.Generic;

namespace DonVo.Persistences.Models
{
    public partial class FactItmachine
    {
        public int Itmachinekey { get; set; }
        public int MachineKey { get; set; }
        public DateTime Datekey { get; set; }
        public decimal? CostAmount { get; set; }
        public string CostType { get; set; }
        public int? EtlloadId { get; set; }
        public DateTime? LoadDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual DimDate DatekeyNavigation { get; set; }
        public virtual DimMachine MachineKeyNavigation { get; set; }
    }
}
