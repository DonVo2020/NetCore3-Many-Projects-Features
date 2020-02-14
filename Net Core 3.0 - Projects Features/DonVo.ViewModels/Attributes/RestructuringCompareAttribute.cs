using System.ComponentModel.DataAnnotations;

namespace DonVo.ViewModels.Attributes
{
    public class RestructuringCompareAttribute : ValidationAttribute
    {
        public RestructuringCompareAttribute()
        {
            ErrorMessage = "Id does not match!";
        }

        public override bool IsValid(object value)
        {
            return !(value is RestructuringViewModel model) || model.HashIdMain != model.HashIdCustomer;
        }
    }
}
