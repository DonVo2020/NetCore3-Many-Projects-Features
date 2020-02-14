using System.ComponentModel.DataAnnotations;

namespace DonVo.ViewModels
{
    public class CreateMainCurrencyViewModel
    {
        [Required(ErrorMessage = "CurrencyKey cannot be blank!")]
        public int CurrencyKey { get; set; }
        [Required(ErrorMessage = "CurrencyLabel cannot be blank!")]
        public string CurrencyLabel { get; set; }
        [Required(ErrorMessage = "CurrencyName cannot be blank!")]
        public string CurrencyName { get; set; }
        [Required(ErrorMessage = "CurrencyDescription cannot be blank!")]
        public string CurrencyDescription { get; set; }
    }
}
