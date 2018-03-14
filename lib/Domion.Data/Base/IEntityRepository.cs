using Domion.Base;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domion.Data.Base
{
    public interface IEntityRepository<TEntity, in TKey> where TEntity : class
    {
        Task<TEntity> FindByIdAsync(TKey id);
    }
}