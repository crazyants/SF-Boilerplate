/*******************************************************************************
* 命名空间: SF.Core.GenericServices.Base
*
* 功 能： N/A
* 类 名： ICreateService
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/8 17:25:54 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace SF.Core.GenericServices.ServicesAsync
{
    public interface IDeleteServiceAsync
    {
        /// <summary>
        /// This will delete an item from the database
        /// </summary>
        /// <param name="keys">The keys must be given in the same order as entity framework has them</param>
        /// <returns></returns>
        Task<ISuccessOrErrors> DeleteAsync<TEntity>(params object[] keys) where TEntity : class;

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
        Task<ISuccessOrErrors> DeleteWithRelationshipsAsync<TEntity>(
            Func<DbContext, TEntity, Task<ISuccessOrErrors>> removeRelationshipsAsync,
            params object[] keys) where TEntity : class;
    }
}
