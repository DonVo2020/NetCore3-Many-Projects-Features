using DonVo.SystemAudit.AuditModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DonVo.SystemAudit.Extensions
{
    public static class ExtensionsServiceCollection
    {
        public static IServiceCollection AddSystemAuditContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DonVoSystemAuditContext>(options => options.UseSqlServer(connectionString));
            return services;
        }
    }
}