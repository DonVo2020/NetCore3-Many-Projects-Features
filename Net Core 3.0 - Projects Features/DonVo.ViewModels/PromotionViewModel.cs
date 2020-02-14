using System;
using System.ComponentModel.DataAnnotations;

namespace DonVo.ViewModels
{
    public class CreateMainPromotionViewModel
    {
        [Required(ErrorMessage = "PromotionKey cannot be blank!")]
        public int PromotionKey { get; set; }
        [Required(ErrorMessage = "PromotionLabel cannot be blank!")]
        public string PromotionLabel { get; set; }
        [Required(ErrorMessage = "PromotionName cannot be blank!")]
        public string PromotionName { get; set; }
        [Required(ErrorMessage = "PromotionDescription cannot be blank!")]
        public string PromotionDescription { get; set; }
        public double? DiscountPercent { get; set; }
        [Required(ErrorMessage = "PromotionType cannot be blank!")]
        public string PromotionType { get; set; }
        [Required(ErrorMessage = "PromotionCategory cannot be blank!")]
        public string PromotionCategory { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? MinQuantity { get; set; }
        public int? MaxQuantity { get; set; }
    }
}
