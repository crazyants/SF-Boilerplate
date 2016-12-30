/*******************************************************************************
* 命名空间: SF.Core.GenericServices.ServicesAsync.Implementation
*
* 功 能： N/A
* 类 名： UpdateSetupServiceAsync
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/9 17:35:41 疯狂蚂蚁 初版
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
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SF.Core.GenericServices.ServicesAsync.Implementation
{

    public class UpdateSetupServiceAsync : IUpdateSetupServiceAsync
    {
        private readonly DbContext _db;

        public UpdateSetupServiceAsync(DbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This returns a status which, if Valid, has single entry using the primary keys to find it.
        /// </summary>
        /// <typeparam name="T">The type of the data to output. 
        /// Type must be a type either an EF data class or one of the EfGenericDto's</typeparam>
        /// <param name="keys">The keys must be given in the same order as entity framework has them</param>
        /// <returns>Task with Status. If valid Result holds data (not tracked), otherwise null</returns>
        public async Task<ISuccessOrErrors<T>> GetOriginalAsync<T>(params object[] keys) where T : class
        {
            var service = DecodeToService<UpdateSetupServiceAsync>.CreateCorrectService<T>(WhatItShouldBe.AsyncClassOrSpecificDto, _db);
            return await service.GetOriginalAsync(keys).ConfigureAwait(false);
        }
    }

    //--------------------------------
    //direct version

    public class UpdateSetupServiceAsync<TEntity> : IUpdateSetupServiceAsync<TEntity> where TEntity : class, new()
    {
        private readonly DbContext _db;

        public UpdateSetupServiceAsync(DbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This gets a single entry using the lambda expression as a where part
        /// </summary>
        /// <param name="whereExpression">Should be a 'where' expression that returns one item</param>
        /// <returns>Task with Status. If valid Result holds data (not tracked), otherwise null</returns>
        public async Task<ISuccessOrErrors<TEntity>> GetOriginalUsingWhereAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await _db.Set<TEntity>().Where(whereExpression).AsNoTracking().RealiseSingleWithErrorCheckingAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// This finds an entry using the primary key(s) in the data
        /// </summary>
        /// <param name="keys">The keys must be given in the same order as entity framework has them</param>
        /// <returns>Task with Status. If valid Result holds data (not tracked), otherwise null</returns>
        public async Task<ISuccessOrErrors<TEntity>> GetOriginalAsync(params object[] keys)
        {
            return await GetOriginalUsingWhereAsync(BuildFilter.CreateFilter<TEntity>(_db.GetKeyProperties<TEntity>(), keys)).ConfigureAwait(false);
        }
    }

    //------------------------------------
    //Dto version

    public class UpdateSetupServiceAsync<TEntity, TDto> : IUpdateSetupServiceAsync<TEntity, TDto>
        where TEntity : class, new()
        where TDto : EfGenericDtoAsync<TEntity, TDto>, new()
    {
        private readonly DbContext _db;

        public UpdateSetupServiceAsync(DbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This gets a single entry using the lambda expression as a where part. It also calls
        /// the dto's SetupSecondaryData to setup any extra data needed
        /// </summary>
        /// <param name="whereExpression">Should be a 'where' expression that returns one item</param>
        /// <returns>Task with Status. If valid TDto type with properties copyed over and SetupSecondaryData called 
        /// to set secondary data, otherwise null</returns>
        public async Task<ISuccessOrErrors<TDto>> GetOriginalUsingWhereAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            var dto = new TDto();
            if (!dto.SupportedFunctions.HasFlag(CrudFunctions.Update))
                throw new InvalidOperationException("This DTO does not support update.");

            var status = await dto.DetailDtoFromDataInAsync(_db, whereExpression).ConfigureAwait(false);
            if (!status.IsValid) return status;

            if (!dto.SupportedFunctions.HasFlag(CrudFunctions.DoesNotNeedSetup))
                await status.Result.SetupSecondaryDataAsync(_db, status.Result).ConfigureAwait(false);
            return status;
        }

        /// <summary>
        /// This returns a single entry using the primary keys to find it. It also calls
        /// the dto's SetupSecondaryData to setup any extra data needed
        /// </summary>
        /// <param name="keys">The keys must be given in the same order as entity framework has them</param>
        /// <returns>Task with Status. If valid TDto type with properties copyed over and SetupSecondaryData called 
        /// to set secondary data, otherwise null</returns>
        public async Task<ISuccessOrErrors<TDto>> GetOriginalAsync(params object[] keys)
        {
            return await GetOriginalUsingWhereAsync(BuildFilter.CreateFilter<TEntity>(_db.GetKeyProperties<TEntity>(), keys)).ConfigureAwait(false);
        }
    }
}