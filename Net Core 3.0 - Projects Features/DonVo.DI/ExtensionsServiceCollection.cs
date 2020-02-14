using DonVo.DataAnalysis.ServiceStack;
using DonVo.DataAnalysis.ServiceStack.Interfaces;
using DonVo.DataAnalysis.ServiceStack.Services;
using DonVo.Persistences.Extensions;
using DonVo.Services.Account;
using DonVo.Services.Dashboard;
using DonVo.Services.Directories;
using DonVo.Services.Interfaces.Account;
using DonVo.Services.Interfaces.Dashboard;
using DonVo.Services.Interfaces.Directories;
using DonVo.Services.Interfaces.Search;
using DonVo.Services.Search;
using DonVo.SpecialConfigurations;
using DonVo.SpecialConfigurations.Extensions;
using DonVo.SpecialConfigurations.Helpers;
using Microsoft.Extensions.DependencyInjection;
using DonVo.SystemAudit.Extensions;
using DonVo.Services.Interfaces.SystemAudit;
using DonVo.Services.SystemAudit;
using DonVo.SystemAudit.Repository;
using DonVo.MLNet.Services;
using DonVo.MLNet.DataModels;

namespace DonVo.DI
{
    public static class ExtensionsServiceCollection
    {
        public static IServiceCollection AddDependencyInjectionServiceModule(this IServiceCollection services,
                                                                                  ConnectionStrings connectionStrings,
                                                                                  HashIdConfiguration setting,
                                                                                  RestConnection restConnection)
        {
            services.AddDbContext(connectionStrings.ContosoRetailDWConnection);
            services.AddIdentityContext(connectionStrings.DonVoIdentityJwtBearerConnection);
            services.AddSystemAuditContext(connectionStrings.DonVoSystemAuditConnection);

            HashHelper.Initialize(setting);

            // Injection All Services enter here.
            //---------------------------------------------------------------------------------------------
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IAccountingService, AccountingService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IPromotionService, PromotionService>();
            services.AddTransient<ICurrencyService, CurrencyService>();
            services.AddTransient<IStoreService, StoreService>();
            services.AddTransient<IMachineService, MachineService>();
            services.AddTransient<IOutageService, OutageService>();
            services.AddTransient<IChannelService, ChannelService>();
            services.AddTransient<IEntityService, EntityService>();
            services.AddTransient<IScenarioService, ScenarioService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IProductCategoryService, ProductCategoryService>();
            services.AddTransient<IProductSubcategoryService, ProductSubcategoryService>();

            services.AddTransient<ISearchService, SearchService>();

            services.AddTransient<ISystemAuditRepository, SystemAuditRepository>();
            services.AddTransient<ISystemAuditService, SystemAuditService>();

            services.AddDataAnalysisService(restConnection.DataAnalysisUrl);

            services.AddMachineLearningService();

            //---------------------------------------------------------------------------------------------

            return services;
        }

        public static IServiceCollection AddDataAnalysisService(this IServiceCollection services, string url)
        {
            services.AddSingleton(x => new DataAnalysisClient(url));
            services.AddTransient<ITestService, TestService>();
            services.AddTransient<IDashboardService, DashboardService>();
            services.AddTransient<IForecastingService, ForecastingService>();
            services.AddTransient<IDeterminingService, DeterminingService>();
            services.AddTransient<IOptimizationService, OptimizationService>();
            services.AddTransient<ICheckingService, CheckingService>();

            return services;
        }

        public static IServiceCollection AddMachineLearningService(this IServiceCollection services)
        {
            services.AddTransient<IAnomalyDetectionsService<object>, AnomalyDetectionsService<object>>();
            services.AddTransient<IMulticlassClassificationsService<CustomerOrdersMulticlassClassificationsModel>, MulticlassClassificationsService<CustomerOrdersMulticlassClassificationsModel>>();
            services.AddTransient<IClusteringService<FactStrategyPlanClusteringData>, ClusteringService<FactStrategyPlanClusteringData>>();
            return services;
        }
    }
}
