/*******************************************************************************
* 命名空间: SF.Core.GenericServices.Services.Implementation
*
* 功 能： N/A
* 类 名： DetailService
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/9 17:29:09 疯狂蚂蚁 初版
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

namespace SF.Core.GenericServices.Services.Implementation
{

    public class DetailService : IDetailService
    {
        private readonly DbContext _db;

        public DetailService(DbContext db)
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
        public ISuccessOrErrors<T> GetDetail<T>(params object[] keys) where T : class, new()
        {
            var service = DecodeToService<DetailService>.CreateCorrectService<T>(WhatItShouldBe.SyncAnything, _db);
            return service.GetDetail(keys);
        }
    }

    //--------------------------------
    //direct version

    public class DetailService<TEntity> : IDetailService<TEntity> where TEntity : class, new()
    {
        private readonly DbContext _db;

        public DetailService(DbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This gets a single entry using the lambda expression as a where part. Checks for problems
        /// </summary>
        /// <param name="whereExpression">Should be a 'where' expression that returns one item</param>
        /// <returns>Status. If valid Result is data as read from database (not tracked), otherwise null</returns>
        public ISuccessOrErrors<TEntity> GetDetailUsingWhere(Expression<Func<TEntity, bool>> whereExpression)
        {
            return _db.Set<TEntity>().Where(whereExpression).AsNoTracking().RealiseSingleWithErrorChecking();
        }

        /// <summary>
        /// This finds an entry using the primary key(s) in the data
        /// </summary>
        /// <param name="keys">The keys must be given in the same order as entity framework has them</param>
        /// <returns>Status. If valid Result is data as read from database (not tracked), otherwise null</returns>
        public ISuccessOrErrors<TEntity> GetDetail(params object[] keys)
        {
            return GetDetailUsingWhere(BuildFilter.CreateFilter<TEntity>(_db.GetKeyProperties<TEntity>(), keys));
        }
    }

    //---------------------------------------------------------------------
    //DTO version

    public class DetailService<TEntity, TDto> : IDetailService<TEntity, TDto>
        where TEntity : class, new()
        where TDto : EfGenericDto<TEntity, TDto>, new()
    {
        private readonly DbContext _db;

        public DetailService(DbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This gets a single entry using the lambda expression as a where part
        /// </summary>
        /// <param name="whereExpression">Should be a 'where' expression that returns one item</param>
        /// <returns>Status. If Valid then TDto type with properties copyed over, else null</returns>
        public ISuccessOrErrors<TDto> GetDetailUsingWhere(Expression<Func<TEntity, bool>> whereExpression)
        {
            var dto = new TDto();
            if (!dto.SupportedFunctions.HasFlag(CrudFunctions.Detail))
                throw new InvalidOperationException("This DTO does not support a detailed view.");

            return dto.DetailDtoFromDataIn(_db, whereExpression);
        }

        /// <summary>
        /// This finds an entry using the primary key(s) in the data
        /// </summary>
        /// <param name="keys">The keys must be given in the same order as entity framework has them</param>
        /// <returns>Status. If Valid then TDto type with properties copyed over, else null</returns>
        public ISuccessOrErrors<TDto> GetDetail(params object[] keys)
        {
            return GetDetailUsingWhere(BuildFilter.CreateFilter<TEntity>(_db.GetKeyProperties<TEntity>(), keys));
        }
    }
}
