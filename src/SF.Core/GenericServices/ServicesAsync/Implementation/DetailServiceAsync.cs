/*******************************************************************************
* 命名空间: SF.Core.GenericServices.ServicesAsync.Implementation
*
* 功 能： N/A
* 类 名： DetailServiceAsync
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/9 17:33:38 疯狂蚂蚁 初版
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

    public class DetailServiceAsync : IDetailServiceAsync
    {
        private readonly DbContext _db;

        public DetailServiceAsync(DbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This returns a status which, if Valid, contains a single entry found using its primary keys.
        /// </summary>
        /// <typeparam name="T">The type of the data to output. 
        /// Type must be a type either an EF data class or one of the EfGenericDto's</typeparam>
        /// <param name="keys">The keys must be given in the same order as entity framework has them</param>
        /// <returns>Status. If valid Result holds data (not tracked), otherwise null</returns>
        public async Task<ISuccessOrErrors<T>> GetDetailAsync<T>(params object[] keys) where T : class, new()
        {
            var service = DecodeToService<DetailServiceAsync>.CreateCorrectService<T>(WhatItShouldBe.AsyncAnything, _db);
            return await service.GetDetailAsync(keys).ConfigureAwait(false);
        }
    }

    //--------------------------------
    //direct

    public class DetailServiceAsync<TEntity> : IDetailServiceAsync<TEntity>
        where TEntity : class, new()
    {
        private readonly DbContext _db;

        public DetailServiceAsync(DbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This gets a single entry using the lambda expression as a where part
        /// </summary>
        /// <param name="whereExpression">Should be a 'where' expression that returns one item</param>
        /// <returns>Task with Status. If valid Result is data as read from database (not tracked), otherwise null</returns>
        public async Task<ISuccessOrErrors<TEntity>> GetDetailUsingWhereAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await _db.Set<TEntity>().Where(whereExpression).AsNoTracking().RealiseSingleWithErrorCheckingAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// This finds an entry using the primary key(s) in the data
        /// </summary>
        /// <param name="keys">The keys must be given in the same order as entity framework has them</param>
        /// <returns>Task with Status. If valid Result is data as read from database (not tracked), otherwise null</returns>
        public async Task<ISuccessOrErrors<TEntity>> GetDetailAsync(params object[] keys)
        {
            return await GetDetailUsingWhereAsync(BuildFilter.CreateFilter<TEntity>(_db.GetKeyProperties<TEntity>(), keys)).ConfigureAwait(false);
        }


    }

    //---------------------------------------------------------------------
    //DTO version

    public class DetailServiceAsync<TEntity, TDto> : IDetailServiceAsync<TEntity, TDto>
        where TEntity : class, new()
        where TDto : EfGenericDtoAsync<TEntity, TDto>, new()
    {
        private readonly DbContext _db;

        public DetailServiceAsync(DbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This gets a single entry using the lambda expression as a where part
        /// </summary>
        /// <param name="whereExpression">Should be a 'where' expression that returns one item</param>
        /// <returns>Task with Status. If valid Result is data as read from database (not tracked), otherwise null</returns>
        public async Task<ISuccessOrErrors<TDto>> GetDetailUsingWhereAsync(Expression<Func<TEntity, bool>> whereExpression)
        {
            var dto = new TDto();
            if (!dto.SupportedFunctions.HasFlag(CrudFunctions.Detail))
                throw new InvalidOperationException("This DTO does not support a detailed view.");

            return await dto.DetailDtoFromDataInAsync(_db, whereExpression).ConfigureAwait(false);
        }

        /// <summary>
        /// This finds an entry using the primary key(s) in the data
        /// </summary>
        /// <param name="keys">The keys must be given in the same order as entity framework has them</param>
        /// <returns>Task with Status. If valid Result is data as read from database (not tracked), otherwise null</returns>
        public async Task<ISuccessOrErrors<TDto>> GetDetailAsync(params object[] keys)
        {
            return await GetDetailUsingWhereAsync(BuildFilter.CreateFilter<TEntity>(_db.GetKeyProperties<TEntity>(), keys)).ConfigureAwait(false);
        }
    }
}
