using System;
using System.Collections.Generic;

namespace DonVo.CreateDBFirstEFCore.Models
{
    public partial class DimOutage
    {
        public DimOutage()
        {
            FactItsla = new HashSet<FactItsla>();
        }

        public int OutageKey { get; set; }
        public string OutageLabel { get; set; }
        public string OutageName { get; set; }
        public string OutageDescription { get; set; }
        public string OutageType { get; set; }
        public string OutageTypeDescription { get; set; }
        public string OutageSubType { get; set; }
        public string OutageSubTypeDescription { get; set; }
        public int? EtlloadId { get; set; }
        public DateTime? LoadDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<FactItsla> FactItsla { get; set; }
    }
}
