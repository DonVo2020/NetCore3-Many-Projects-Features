using System;
using System.Collections.Generic;

namespace DonVo.CreateDBFirstEFCore.Models
{
    public partial class DimAccount
    {
        public DimAccount()
        {
            FactStrategyPlan = new HashSet<FactStrategyPlan>();
        }

        public int AccountKey { get; set; }
        public int? ParentAccountKey { get; set; }
        public string AccountLabel { get; set; }
        public string AccountName { get; set; }
        public string AccountDescription { get; set; }
        public string AccountType { get; set; }
        public string Operator { get; set; }
        public string CustomMembers { get; set; }
        public string ValueType { get; set; }
        public string CustomMemberOptions { get; set; }
        public int? EtlloadId { get; set; }
        public DateTime? LoadDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<FactStrategyPlan> FactStrategyPlan { get; set; }
    }
}
