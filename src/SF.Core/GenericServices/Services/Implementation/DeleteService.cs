/*******************************************************************************
* 命名空间: SF.Core.GenericServices.Services.Implementation
*
* 功 能： N/A
* 类 名： DeleteService
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/9 17:28:37 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using Microsoft.EntityFrameworkCore;
using SF.Core.GenericServices.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.GenericServices.Services.Implementation
{
    /// <summary>
    /// This holds the methods to delete an entry from the database
    /// </summary>
    public class DeleteService : IDeleteService
    {
        private readonly DbContext _db;

        public DeleteService(DbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This will delete an item from the database
        /// </summary>
        /// <param name="keys">The keys must be given in the same order as entity framework has them</param>
        /// <returns></returns>
        public ISuccessOrErrors Delete<TEntity>(params object[] keys) where TEntity : class
        {

            var entityToDelete = _db.Set<TEntity>().Find(keys);
            if (entityToDelete == null)
                return
                    new SuccessOrErrors().AddSingleError(
                        "Could not delete entry as it was not in the database. Could it have been deleted by someone else?");

            _db.Set<TEntity>().Remove(entityToDelete);
            var result = _db.SaveChangesWithChecking();
            if (result.IsValid)
                result.SetSuccessMessage("Successfully deleted {0}.", typeof(TEntity).Name);

            return result;

        }

        /// <summary>
        /// This allows a developer to delete an entity plus any of its relationships.
        /// The first part of the method finds the given entity using the provided keys.
        /// It then calls the deleteRelationships method which should remove the extra relationships
        /// </summary>
        /// <param name="removeRelationships">method which is handed the DbContext and the found entity.
        /// It should then remove any relationships on this entity that it wants to.
        /// It returns a status, if IsValid then calls SaveChangesWithChecking</param>
        /// <param name="keys">The keys must be given in the same order as entity framework has them</param>
        /// <returns></returns>
        public ISuccessOrErrors DeleteWithRelationships<TEntity>(Func<DbContext, TEntity, ISuccessOrErrors> removeRelationships,
            params object[] keys) where TEntity : class
        {

            var entityToDelete = _db.Set<TEntity>().Find(keys);
            if (entityToDelete == null)
                return
                    new SuccessOrErrors().AddSingleError(
                        "Could not delete entry as it was not in the database. Could it have been deleted by someone else?");

            var result = removeRelationships(_db, entityToDelete);
            if (!result.IsValid) return result;

            _db.Set<TEntity>().Remove(entityToDelete);
            result = _db.SaveChangesWithChecking();
            if (result.IsValid)
                result.SetSuccessMessage("Successfully deleted {0} and given relationships.", typeof(TEntity).Name);

            return result;

        }

    }
}
