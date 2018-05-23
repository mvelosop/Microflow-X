using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Tenants.Infrastructure.Data.Configuration;

namespace Scripts.Cli
{
    public class TenantsDbContextDesignTimeFactory : IDesignTimeDbContextFactory<TenantsDbContext>
    {
        public TenantsDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<TenantsDbContext>();

            builder.UseSqlServer("x");

            return new TenantsDbContext(builder.Options);
        }
    }
}
