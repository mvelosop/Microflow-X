using Domion.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Domion.Data.Base;
using Tenants.Core.Model;

namespace Tenants.Core.Repositories
{
    public interface ITenantRepository : IEntityRepository<Tenant, Guid>, IEntityQuery<Tenant>
    {
        Task<Tenant> FindByEmailAsync(string email);

        Task<Tenant> FindDuplicateByEmailAsync(Tenant entity);

        Task<int> SaveChangesAsync();

        Task<List<ValidationResult>> TryDeleteAsync(Tenant entity);

        Task<List<ValidationResult>> TryInsertAsync(Tenant entity);

        Task<List<ValidationResult>> TryUpdateAsync(Tenant entity);

        Task<List<ValidationResult>> TryUpsertAsync(Tenant entity);
    }
}