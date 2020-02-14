using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DonVo.Persistences.Extensions
{
    public static class ExtensionsServiceCollection
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ContosoRetailDWContext>(options => options.UseSqlServer(connectionString));
            return services;
        }
    }
}