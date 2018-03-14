using Domion.Base;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domion.Data.Base
{
    public interface IEntityQuery<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetListAsync(IQuerySpec<TEntity> querySpec, CancellationToken cancellationToken);
    }
}