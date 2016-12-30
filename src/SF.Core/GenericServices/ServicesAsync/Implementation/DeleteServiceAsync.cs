/*******************************************************************************
* 命名空间: SF.Core.GenericServices.ServicesAsync.Implementation
*
* 功 能： N/A
* 类 名： DeleteServiceAsync
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/9 17:33:00 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using Microsoft.EntityFrameworkCore;
using SF.Core.GenericServices.Dtos;
using SF.Core.GenericServices.Helper;
using SF.Core.GenericServices.Internal;
using System;
using System.Threading.Tasks;

namespace SF.Core.GenericServices.ServicesAsync.Implementation
{

    public class DeleteServiceAsync : IDeleteServiceAsync
    {
        private readonly DbContext _db;

        public DeleteServiceAsync(DbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This will delete an item from the database
        /// </summary>
        /// <param name="keys">The keys must be given in the same order as entity framework has them</param>
        /// <returns></returns>
        public async Task<ISuccessOrErrors> DeleteAsync<TEntity>(params object[] keys) where TEntity : class
        {
            var entityToDelete = await _db.Set<TEntity>().FindAsync(keys).ConfigureAwait(false);
            if (entityToDelete == null)
                return
                    new SuccessOrErrors().AddSingleError(
                        "Could not delete entry as it was not in the database. Could it have been deleted by someone else?");

            _db.Set<TEntity>().Remove(entityToDelete);
            var result = await _db.SaveChangesWithCheckingAsync().ConfigureAwait(false);
            if (result.IsValid)
                result.SetSuccessMessage("Successfully deleted {0}.", typeof(TEntity).Name);

            return result;
        }

        /// <summary>
        /// This allows a developer to delete an entity plus any of its relationships.
        /// The first part of the method finds the given entity using the provided keys.
        /// It then calls the deleteRelationships method which should remove the extra relationships
        /// </summary>
        /// <param name="removeRelationshipsAsync">method which is handed the DbContext and the found entity.
        /// It should then remove any relationships on this entity that it wants to.
        /// It returns a status, if IsValid then calls SaveChangesWithChecking</param>
        /// <param name="keys">The keys must be given in the same order as entity framework has them</param>
        /// <returns></returns>
        public async Task<ISuccessOrErrors> DeleteWithRelationshipsAsync<TEntity>(Func<DbContext, TEntity, Task<ISuccessOrErrors>> removeRelationshipsAsync,
            params object[] keys) where TEntity : class
        {

            var entityToDelete = await _db.Set<TEntity>().FindAsync(keys).ConfigureAwait(false);
            if (entityToDelete == null)
                return
                    new SuccessOrErrors().AddSingleError(
                        "Could not delete entry as it was not in the database. Could it have been deleted by someone else?");

            var result = await removeRelationshipsAsync(_db, entityToDelete).ConfigureAwait(false);
            if (!result.IsValid) return result;

            _db.Set<TEntity>().Remove(entityToDelete);
            result = await _db.SaveChangesWithCheckingAsync().ConfigureAwait(false);
            if (result.IsValid)
                result.SetSuccessMessage("Successfully deleted {0} and given relationships.", typeof(TEntity).Name);

            return result;

        }

    }
}
