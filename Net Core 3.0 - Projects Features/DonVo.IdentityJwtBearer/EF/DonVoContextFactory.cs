using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DonVo.IdentityJwtBearer.EF
{
    public class DonVoContextFactory : IDesignTimeDbContextFactory<DonVoIdentityContext>
    {
        public DonVoIdentityContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DonVoIdentityContext>();
            optionsBuilder.UseSqlServer("Server=LAPTOP-ILQS92OM\\SQLEXPRESS;Database=DonVoIdentityJwtBearer;Trusted_Connection=True;MultipleActiveResultSets=true;");
            return new DonVoIdentityContext(optionsBuilder.Options);
        }
    }
}