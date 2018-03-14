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

        //public Task<List<TEntity>> GetListAsync(EntityQuery<TEntity> querySpec = null, CancellationToken cancellationToken = default(CancellationToken) )

        //public static async Task<TEntity> FirstOrDefaultAsync<TEntity>(
        //    this IEntityQuery<TEntity> repository) where TEntity : class
        //{
        //    return await repository.Query().FirstOrDefaultAsync();
        //}

        //public static async Task<TEntity> FirstOrDefaultAsync<TEntity>(
        //    this IEntityQuery<TEntity> repository,
        //    Expression<Func<TEntity, bool>> where) where TEntity : class
        //{
        //    return await repository.Query(where).FirstOrDefaultAsync();
        //}

        //public static IIncludableQueryable<TEntity, TProperty> Include<TEntity, TProperty>(
        //    this IEntityQuery<TEntity> repository,
        //    Expression<Func<TEntity, TProperty>> navigationPropertyPath) where TEntity : class
        //{
        //    return repository.Query().Include(navigationPropertyPath);
        //}
    }
}