/*******************************************************************************
* 命名空间: SF.Core.GenericServices.Base
*
* 功 能： N/A
* 类 名： IUpdateSetupService
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/8 17:31:57 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SF.Core.GenericServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SF.Core.GenericServices.Services
{

    public interface IUpdateSetupService<TEntity> where TEntity : class
    {
        /// <summary>
        /// This gets a single entry using the lambda expression as a where part
        /// </summary>
        /// <param name="whereExpression">Should be a 'where' expression that returns one item</param>
        /// <returns>Status. If valid Result holds data (not tracked), otherwise null</returns>
        ISuccessOrErrors<TEntity> GetOriginalUsingWhere(Expression<Func<TEntity, bool>> whereExpression);

        /// <summary>
        /// This finds an entry using the primary key(s) in the data
        /// </summary>
        /// <param name="keys">The keys must be given in the same order as entity framework has them</param>
        /// <returns>Status. If valid Result holds data (not tracked), otherwise null</returns>
        ISuccessOrErrors<TEntity> GetOriginal(params object[] keys);
    }


    public interface IUpdateSetupService<TEntity, TDto>
        where TEntity : class, new()
        where TDto : EfGenericDto<TEntity, TDto>, new()
    {
        /// <summary>
        /// This returns a single entry using the primary keys to find it. It also calls
        /// the dto's SetupSecondaryData to setup any extra data needed
        /// </summary>
        /// <param name="keys">The keys must be given in the same order as entity framework has them</param>
        /// <returns>Status. If valid TDto type with properties copyed over and SetupSecondaryData called 
        /// to set secondary data, otherwise null</returns>
        ISuccessOrErrors<TDto> GetOriginal(params object[] keys);

        /// <summary>
        /// This gets a single entry using the lambda expression as a where part. It also calls
        /// the dto's SetupSecondaryData to setup any extra data needed
        /// </summary>
        /// <param name="whereExpression">Should be a 'where' expression that returns one item</param>
        /// <returns>TDto type with properties copyed over and SetupSecondaryData called to set secondary data</returns>
        ISuccessOrErrors<TDto> GetOriginalUsingWhere(Expression<Func<TEntity, bool>> whereExpression);
    }
}
