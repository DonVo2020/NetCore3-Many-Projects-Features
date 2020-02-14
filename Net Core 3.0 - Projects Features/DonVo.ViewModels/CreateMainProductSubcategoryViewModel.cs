using System.ComponentModel.DataAnnotations;

namespace DonVo.ViewModels
{
    public class CreateMainProductSubcategoryViewModel
    {
        [Required(ErrorMessage = "ProductSubcategoryKey cannot be blank!")]
        public int ProductSubcategoryKey { get; set; }
        [Required(ErrorMessage = "Label cannot be blank!")]
        public string ProductSubcategoryLabel { get; set; }
        [Required(ErrorMessage = "Name cannot be blank!")]
        public string ProductSubcategoryName { get; set; }
        [Required(ErrorMessage = "Description cannot be blank!")]
        public string ProductSubcategoryDescription { get; set; }
        [Required(ErrorMessage = "Product cannot be blank!")]
        public int? ProductCategoryKey { get; set; }
    }

    public class UpdateProductSubcategoryViewModel
    {
        [Required(ErrorMessage = "ProductSubcategoryKey cannot be blank!")]
        public int ProductSubcategoryKey { get; set; }
        [Required(ErrorMessage = "Label cannot be blank!")]
        public string ProductSubcategoryLabel { get; set; }
        [Required(ErrorMessage = "Name cannot be blank!")]
        public string ProductSubcategoryName { get; set; }
        [Required(ErrorMessage = "Description cannot be blank!")]
        public string ProductSubcategoryDescription { get; set; }
        [Required(ErrorMessage = "Product cannot be blank!")]
        public int? ProductCategoryKey { get; set; }
    }
}
