using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Tenants.Core.Model;

namespace Tenants.App.Services
{
    public interface ITenantServices
    {
        Task<List<ValidationResult>> AddTenantAsync(Tenant entity);

        Task<Tenant> FindTenantByEmailAsync(string name);

        IQueryable<Tenant> QueryTenants(Expression<Func<Tenant, bool>> where = null);

        Task<List<ValidationResult>> RemoveTenantAsync(Tenant entity);
    }
}