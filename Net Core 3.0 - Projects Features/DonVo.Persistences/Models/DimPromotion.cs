using System;
using System.Collections.Generic;

namespace DonVo.Persistences.Models
{
    public partial class DimPromotion
    {
        public DimPromotion()
        {
            FactOnlineSales = new HashSet<FactOnlineSales>();
            FactSales = new HashSet<FactSales>();
        }

        public int PromotionKey { get; set; }
        public string PromotionLabel { get; set; }
        public string PromotionName { get; set; }
        public string PromotionDescription { get; set; }
        public double? DiscountPercent { get; set; }
        public string PromotionType { get; set; }
        public string PromotionCategory { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? MinQuantity { get; set; }
        public int? MaxQuantity { get; set; }
        public int? EtlloadId { get; set; }
        public DateTime? LoadDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        public virtual ICollection<FactOnlineSales> FactOnlineSales { get; set; }
        public virtual ICollection<FactSales> FactSales { get; set; }
    }
}
