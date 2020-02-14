using System;

namespace DonVo.ViewModels.DTOs.Directories
{
    public class ProductCategoryDTO
    {
        public int ProductCategoryKey { get; set; }
        public string ProductCategoryLabel { get; set; }
        public string ProductCategoryName { get; set; }
        public string ProductCategoryDescription { get; set; }
        public int? EtlloadId { get; set; }
        public DateTime? LoadDate { get; set; }
        public DateTime? UpdateDate { get; set; }

    }
}
