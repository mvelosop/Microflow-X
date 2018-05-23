using Domion.Base;
using Domion.Data.Base;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domion.Data.Extensions
{
    public static class RepositoryExtensions
    {
        public static Task<List<TEntity>> GetListAsync<TEntity>(
            this IEntityQuery<TEntity> repo) where TEntity : class
        {
            return repo.GetListAsync(null, default(CancellationToken));
        }

        public static Task<List<TEntity>> GetListAsync<TEntity>(
            this IEntityQuery<TEntity> repo,
            IQuerySpec<TEntity> querySpec) where TEntity : class
        {
            return repo.GetListAsync(querySpec, default(CancellationToken));
        }

        public static Task<List<TEntity>> GetListAsync<TEntity>(
            this IEntityQuery<TEntity> repo,
            CancellationToken cancellationToken) where TEntity : class
        {
            return repo.GetListAsync(null, cancellationToken);
        }
    }
}