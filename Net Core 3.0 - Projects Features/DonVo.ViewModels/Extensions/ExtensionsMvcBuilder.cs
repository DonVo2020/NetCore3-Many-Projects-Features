using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace DonVo.ViewModels.Extensions
{
    public static class ExtensionsMvcBuilder
    {
        public static IMvcBuilder AddDonVoValidationModule(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.AddFluentValidation();
        }
    }
}
