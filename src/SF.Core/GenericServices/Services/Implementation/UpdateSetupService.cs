/*******************************************************************************
* 命名空间: SF.Core.GenericServices.Services.Implementation
*
* 功 能： N/A
* 类 名： UpdateSetupService
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/9 17:31:07 疯狂蚂蚁 初版
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

    public class UpdateSetupService : IUpdateSetupService
    {
        private readonly DbContext _db;

        public UpdateSetupService(DbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This returns a status which, if Valid, has single entry using the primary keys to find it.
        /// </summary>
        /// <typeparam name="T">The type of the data to output. 
        /// Type must be a type either an EF data class or one of the EfGenericDto's</typeparam>
        /// <param name="keys">The keys must be given in the same order as entity framework has them</param>
        /// <returns>Status. If valid Result holds data (not tracked), otherwise null</returns>
        public ISuccessOrErrors<T> GetOriginal<T>(params object[] keys) where T : class
        {
            var service = DecodeToService<UpdateSetupService>.CreateCorrectService<T>(WhatItShouldBe.SyncClassOrSpecificDto, _db);
            return service.GetOriginal(keys);
        }
    }

    //--------------------------------
    //direct version

    public class UpdateSetupService<TEntity> : IUpdateSetupService<TEntity> where TEntity : class, new()
    {
        private readonly DbContext _db;

        public UpdateSetupService(DbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This gets a single entry using the lambda expression as a where part
        /// </summary>
        /// <param name="whereExpression">Should be a 'where' expression that returns one item</param>
        /// <returns>Status. If valid Result holds data (not tracked), otherwise null</returns>
        public ISuccessOrErrors<TEntity> GetOriginalUsingWhere(Expression<Func<TEntity, bool>> whereExpression)
        {
            return _db.Set<TEntity>().Where(whereExpression).AsNoTracking().RealiseSingleWithErrorChecking();
        }

        /// <summary>
        /// This finds an entry using the primary key(s) in the data
        /// </summary>
        /// <param name="keys">The keys must be given in the same order as entity framework has them</param>
        /// <returns>Status. If valid Result holds data (not tracked), otherwise null</returns>
        public ISuccessOrErrors<TEntity> GetOriginal(params object[] keys)
        {
            return GetOriginalUsingWhere(BuildFilter.CreateFilter<TEntity>(_db.GetKeyProperties<TEntity>(), keys));
        }
    }

    //--------------------------------
    //dto version

    public class UpdateSetupService<TEntity, TDto> : IUpdateSetupService<TEntity, TDto>
        where TEntity : class, new()
        where TDto : EfGenericDto<TEntity, TDto>, new()
    {
        private readonly DbContext _db;

        public UpdateSetupService(DbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// This returns a single entry using the primary keys to find it. It also calls
        /// the dto's SetupSecondaryData to setup any extra data needed
        /// </summary>
        /// <param name="keys">The keys must be given in the same order as entity framework has them</param>
        /// <returns>Status. If valid TDto type with properties copyed over and SetupSecondaryData called 
        /// to set secondary data, otherwise null</returns>
        public ISuccessOrErrors<TDto> GetOriginal(params object[] keys)
        {
            return GetOriginalUsingWhere(BuildFilter.CreateFilter<TEntity>(_db.GetKeyProperties<TEntity>(), keys));
        }

        /// <summary>
        /// This gets a single entry using the lambda expression as a where part. It also calls
        /// the dto's SetupSecondaryData to setup any extra data needed
        /// </summary>
        /// <param name="whereExpression">Should be a 'where' expression that returns one item</param>
        /// <returns>Status. If valid TDto type with properties copyed over and SetupSecondaryData called 
        /// to set secondary data, otherwise null</returns>
        public ISuccessOrErrors<TDto> GetOriginalUsingWhere(Expression<Func<TEntity, bool>> whereExpression)
        {
            var dto = new TDto();
            if (!dto.SupportedFunctions.HasFlag(CrudFunctions.Update))
                throw new InvalidOperationException("This DTO does not support update.");

            var status = dto.DetailDtoFromDataIn(_db, whereExpression);
            if (!status.IsValid) return status;

            if (!dto.SupportedFunctions.HasFlag(CrudFunctions.DoesNotNeedSetup))
                status.Result.SetupSecondaryData(_db, status.Result);
            return status;
        }
    }
}
