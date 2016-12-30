/*******************************************************************************
* 命名空间: SF.Core.GenericServices.Base
*
* 功 能： N/A
* 类 名： IListService
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/8 17:29:39 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SF.Core.GenericServices.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.GenericServices.Services
{

    public interface IListService<out TEntity> where TEntity : class
    {
        /// <summary>
        /// This returns an IQueryable list of all items of the given type
        /// </summary>
        /// <returns>note: the list items are not tracked</returns>
        IQueryable<TEntity> GetAll();
    }

    public interface IListService<TEntity, out TDto>
        where TEntity : class
        where TDto : EfGenericDtoBase<TEntity, TDto>
    {
        /// <summary>
        /// This returns an IQueryable list of all items of the given type
        /// </summary>
        /// <returns>note: the list items are not tracked</returns>
        IQueryable<TDto> GetAll();
    }
}
