using System;

namespace DonVo.ViewModels.DTOs.Directories
{
    public class ProductSubcategoryDTO
    {
        public int ProductSubcategoryKey { get; set; }
        public string ProductSubcategoryLabel { get; set; }
        public string ProductSubcategoryName { get; set; }
        public string ProductSubcategoryDescription { get; set; }
        public int? ProductCategoryKey { get; set; }
        public string ProductCategoryName { get; set; }
        public int? EtlloadId { get; set; }
        public DateTime? LoadDate { get; set; }
        public DateTime? UpdateDate { get; set; }
    }

    public class CreateProductSubcategoryDTO
    {
        public int ProductSubcategoryKey { get; set; }
        public string ProductSubcategoryLabel { get; set; }
        public string ProductSubcategoryName { get; set; }
        public string ProductSubcategoryDescription { get; set; }
        public int? ProductCategoryKey { get; set; }
    }

    public class UpdateProductSubcategoryDTO : BaseUpdateDTO
    {
        public int ProductSubcategoryKey { get; set; }
        public string ProductSubcategoryLabel { get; set; }
        public string ProductSubcategoryName { get; set; }
        public string ProductSubcategoryDescription { get; set; }
        public int? ProductCategoryKey { get; set; }
    }
}
