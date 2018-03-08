using Domion.Data.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Tenants.Core.Model;

namespace Tenants.Core.Repositories
{
    public interface ITenantRepository : IEntityQuery<Tenant>
    {
        Task<int> SaveChangesAsync();

        Task<List<ValidationResult>> TryDeleteAsync(Tenant entity);

        Task<List<ValidationResult>> TryInsertAsync(Tenant entity);

        Task<List<ValidationResult>> TryUpdateAsync(Tenant entity);

        Task<List<ValidationResult>> TryUpsertAsync(Tenant entity);
    }
}