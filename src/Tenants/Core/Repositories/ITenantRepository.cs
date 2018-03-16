using Domion.Data.Base;
using System;
using System.Threading.Tasks;
using Tenants.Core.Model;

namespace Tenants.Core.Repositories
{
    public interface ITenantRepository : IEntityRepository<Tenant, Guid>, IEntityQuery<Tenant>
    {
        void Delete(Tenant entity);

        Task<Tenant> FindByEmailAsync(string email);

        Task<Tenant> FindByEmailAsync(string email, Guid id);

        void Insert(Tenant entity);

        Task<int> SaveChangesAsync();

        void Update(Tenant entity);

        void Upsert(Tenant entity);
    }
}