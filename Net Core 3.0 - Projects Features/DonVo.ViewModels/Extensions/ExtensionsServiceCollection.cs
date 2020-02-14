using Microsoft.Extensions.DependencyInjection;

namespace DonVo.ViewModels.Extensions
{
    public static class ExtensionsServiceCollection
    {
        public static IServiceCollection AddDonVoViewModelsModule(this IServiceCollection services)
        {
            //services.AddTransient<IValidator<CreateEmployeeViewModel>, CreateEmployeeValidation>();
            return services;
        }
    }
}
