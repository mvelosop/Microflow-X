using System;
using System.Linq;
using System.Threading.Tasks;
using Domion.Data.Base;
using Domion.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Tenants.Core.Model;

namespace Tenants.Data.Extensions
{
    public static class TenantRepositoryExtensions
    {
        public static Task<Tenant> FindByIdAsync(this IEntityQuery<Tenant> repository, Guid id)
        {
            return repository.FirstOrDefaultAsync(t => t.Id == id);
        }

        public static Task<Tenant> FindByEmailAsync(this IEntityQuery<Tenant> repository, string email)
        {
            return repository.FirstOrDefaultAsync(t => t.Email == email);
        }

        public static Task<Tenant> FindDuplicateByEmailAsync(this IEntityQuery<Tenant> repository, Tenant entity)
        {
            IQueryable<Tenant> query = repository.Query(t => t.Email == entity.Email);

            if (entity.Id != Guid.Empty)
            {
                query = query.Where(t => t.Id != entity.Id);
            }

            return query.FirstOrDefaultAsync();
        }
    }
}
