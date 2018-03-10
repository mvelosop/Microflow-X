//------------------------------------------------------------------------------
//  TenantRepository.cs
//
//  Implementation of: TenantRepository (Class) <<entity-repository>>
//  Generated by Domion-MDA - http://www.coderepo.blog/domion
//
//  Created on     : 02-jun-2017 10:49:07
//  Original author: Miguel
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domion.Data.Base;
using Domion.Lib;
using Tenants.Core.Model;
using Tenants.Core.Repositories;
using Tenants.Data.Configuration;
using Tenants.Data.Extensions;

namespace Tenants.Data.Repositories
{
    public class TenantRepository : EntityRepository<Tenant>, ITenantRepository
    {
        public static readonly string DuplicateByEmailError = @"There's another Tenant with Email ""{0}"", can't duplicate! (Id={1})";

        /// <inheritdoc />
        public TenantRepository(TenantsDbContext dbContext)
            : base(dbContext)
        {
        }

        public new IQueryable<Tenant> Query(Expression<Func<Tenant, bool>> where = null)
        {
            return base.Query(where);
        }

        public new virtual Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public new virtual Task<List<ValidationResult>> TryDeleteAsync(Tenant entity)
        {
            if (entity.UpdateToken == null || entity.UpdateToken.Length == 0) throw new InvalidOperationException($"Missing {nameof(entity.UpdateToken)} on Delete");

            return base.TryDeleteAsync(entity);
        }

        public new virtual Task<List<ValidationResult>> TryInsertAsync(Tenant entity)
        {
            if (entity.UpdateToken != null && entity.UpdateToken.Length > 0) throw new InvalidOperationException($"Existing {nameof(entity.UpdateToken)} on Insert");

            CommonSaveOperations(entity);

            return base.TryInsertAsync(entity);
        }

        public new virtual Task<List<ValidationResult>> TryUpdateAsync(Tenant entity)
        {
            if (entity.UpdateToken == null || entity.UpdateToken.Length == 0) throw new InvalidOperationException($"Missing {nameof(entity.UpdateToken)} on Update");

            CommonSaveOperations(entity);

            return base.TryUpdateAsync(entity);
        }

        public virtual Task<List<ValidationResult>> TryUpsertAsync(Tenant entity)
        {
            return entity.Id == Guid.Empty ? TryInsertAsync(entity) : TryUpdateAsync(entity);
        }

        /// <summary>
        ///     Performs operations that have to be executed both on inserts and updates.
        /// </summary>
        internal virtual void CommonSaveOperations(Tenant entity)
        {
            TrimStrings(entity);
        }

        protected override Task<List<ValidationResult>> ValidateDeleteAsync(Tenant entity)
        {
            return base.ValidateDeleteAsync(entity);
        }

        /// <inheritdoc />
        protected override async Task<List<ValidationResult>> ValidateSaveAsync(Tenant entity)
        {
            Tenant duplicateByEmail = await this.FindDuplicateByEmailAsync(entity);

            if (duplicateByEmail != null)
            {
                return Errors.ErrorList(DuplicateByEmailError, new object[] { duplicateByEmail.Email, duplicateByEmail.Id }, new[] { "Name" });
            }

            return Errors.NoError;
        }

        private void TrimStrings(Tenant entity)
        {
            entity.Name = entity.Name?.Trim();
        }
    }
}
