using System;
using Microsoft.EntityFrameworkCore;
using Tenants.Data.Configuration;

namespace Tenants.Setup
{
    public class TenantsDbSetup
    {
        private readonly string _connectionString;

        private readonly object _lock = new object();

        private DbContextOptions<TenantsDbContext> _options;

        public TenantsDbSetup(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void ConfigureDatabase(bool migrateDatabase = false)
        {
            var optionBuilder = new DbContextOptionsBuilder<TenantsDbContext>();

            optionBuilder.UseSqlServer(_connectionString);

            _options = optionBuilder.Options;

            if (!migrateDatabase) return;

            // This lock avoids conflicts on DB creation, specially during parallel integration tests
            lock (_lock)
            {
                using (var dbContext = CreateDbContext())
                {
                    dbContext.Database.Migrate();
                }
            }
        }

        public TenantsDbContext CreateDbContext()
        {
            if (_options == null)
                throw new InvalidOperationException($"Must run {GetType().Name}.{nameof(ConfigureDatabase)} first!");

            return new TenantsDbContext(_options);
        }
    }
}