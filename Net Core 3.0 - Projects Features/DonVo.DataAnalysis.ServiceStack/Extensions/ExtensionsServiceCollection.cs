using Microsoft.Extensions.DependencyInjection;
using DonVo.DataAnalysis.ServiceStack.Interfaces;
using DonVo.DataAnalysis.ServiceStack.Services;

namespace DonVo.DataAnalysis.ServiceStack.Extensions
{
    public static class ExtensionsServiceCollection
    {
        public static IServiceCollection AddDataAnalysisService(this IServiceCollection services, string url)
        {
            services.AddSingleton(x => new DataAnalysisClient(url));

            services.AddTransient<ITestService, TestService>();
            services.AddTransient<IHomeService, HomeService>();
            services.AddTransient<IForecastingService, ForecastingService>();
            services.AddTransient<IDeterminingService, DeterminingService>();
            services.AddTransient<IOptimizationService, OptimizationService>();
            services.AddTransient<ICheckingService, CheckingService>();

            return services;
        }
    }
}