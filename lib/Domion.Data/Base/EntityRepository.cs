using Domion.Base;
using Domion.Lib;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Domion.Data.Base
{
    /// <summary>
    ///     Generic repository implementation.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class EntityRepository<TEntity> : IEntityQuery<TEntity> where TEntity : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        /// <summary>
        ///     Creates the generic repository instance.
        /// </summary>
        /// <param name="dbContext">The DbContext to get the Entity Type from.</param>
        protected EntityRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        protected virtual DbContext DbContext => _dbContext;

        protected virtual DbSet<TEntity> DbSet => _dbSet;

        /// <summary>
        ///     Returns a query expression that, when enumerated, will retrieve only the objects that satisfy the where condition.
        /// </summary>
        public virtual Task<List<TEntity>> GetListAsync(IQuerySpec<TEntity> querySpec, CancellationToken cancellationToken)
        {
            return QueryInternal(querySpec?.WhereExpression).ToListAsync(cancellationToken);
        }

        /// <summary>
        ///     The base query, internal use only do not expose out of the concrete class
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns>The base for all query operations</returns>
        protected virtual IQueryable<TEntity> QueryInternal(Expression<Func<TEntity, bool>> whereExpression = null)
        {
            return whereExpression == null ? _dbSet : _dbSet.Where(whereExpression);
        }

        /// <summary>
        ///     Saves changes asynchronously from the DbContext's change tracker to the database.
        /// </summary>
        /// <returns>The number of objects written to the underlying database</returns>
        protected virtual Task<int> SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        /// <summary>
        ///     Marks an entity for deletion in the DbContext's change tracker if it passes the ValidateDelete method.
        /// </summary>
        protected virtual async Task<List<ValidationResult>> TryDeleteAsync(TEntity entity)
        {
            List<ValidationResult> errors = await ValidateDeleteAsync(entity);

            if (errors.Any())
            {
                return errors;
            }

            _dbSet.Remove(entity);

            return Errors.NoError;
        }

        /// <summary>
        ///     Adds an entity for insertion in the DbContext's change tracker if it passes the ValidateSave method.
        /// </summary>
        protected virtual async Task<List<ValidationResult>> TryInsertAsync(TEntity entity)
        {
            List<ValidationResult> errors = await ValidateSaveAsync(entity);

            if (errors.Any())
            {
                return errors;
            }

            _dbSet.Add(entity);

            return Errors.NoError;
        }

        /// <summary>
        ///     Marks an entity for update in the DbContext's change tracker if it passes the ValidateSave method.
        /// </summary>
        protected virtual async Task<List<ValidationResult>> TryUpdateAsync(TEntity entity)
        {
            List<ValidationResult> errors = await ValidateSaveAsync(entity);

            if (errors.Any())
            {
                return errors;
            }

            _dbSet.Update(entity);

            return Errors.NoError;
        }

        /// <summary>
        ///     Validates if it's ok to delete the entity from the database.
        /// </summary>
        protected virtual async Task<List<ValidationResult>> ValidateDeleteAsync(TEntity model)
        {
            return Errors.NoError;
        }

        /// <summary>
        ///     Validates if it's ok to save the new or updated entity to the database.
        /// </summary>
        protected virtual async Task<List<ValidationResult>> ValidateSaveAsync(TEntity model)
        {
            return Errors.NoError;
        }
    }
}