using DonVo.IdentityJwtBearer.EF;
using DonVo.IdentityJwtBearer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DonVo.SpecialConfigurations.Extensions
{
    public static class ExtensionsServiceCollection
    {
        public static IServiceCollection AddIdentityContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DonVoIdentityContext>(options => options.UseSqlServer(connectionString));
            services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<DonVoIdentityContext>();
            return services;
        }
    }
}
