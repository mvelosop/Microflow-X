using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domion.Lib;
using Tenants.Core.Model;
using Tenants.Data.Extensions;
using Tenants.Data.Repositories;

namespace Tenants.App.Services
{
    public class TenantServices : ITenantServices
    {
        private readonly Lazy<TenantRepository> _lazyTenantRepo;

        public TenantServices(
            Lazy<TenantRepository> lazyTenantRepo)
        {
            _lazyTenantRepo = lazyTenantRepo;
        }

        private TenantRepository TenantRepo => _lazyTenantRepo.Value;

        public async Task<List<ValidationResult>> AddTenantAsync(Tenant entity)
        {
            List<ValidationResult> errors = await TenantRepo.TryInsertAsync(entity);

            if (errors.Any()) return errors;

            await TenantRepo.SaveChangesAsync();

            return Errors.NoError;
        }

        public async Task<Tenant> FindTenantByEmailAsync(string name)
        {
            return await TenantRepo.FindByEmailAsync(name);
        }

        public IQueryable<Tenant> QueryTenants(Expression<Func<Tenant, bool>> where = null)
        {
            return TenantRepo.Query(where);
        }

        public async Task<List<ValidationResult>> RemoveTenantAsync(Tenant entity)
        {
            List<ValidationResult> errors = await TenantRepo.TryDeleteAsync(entity);

            if (errors.Any()) return errors;

            await TenantRepo.SaveChangesAsync();

            return Errors.NoError;
        }
    }
}
