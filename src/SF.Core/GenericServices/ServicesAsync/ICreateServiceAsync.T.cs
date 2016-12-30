/*******************************************************************************
* 命名空间: SF.Core.GenericServices.ServicesAsync
*
* 功 能： N/A
* 类 名： ICreateServiceAsync
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/8 17:34:12 疯狂蚂蚁 初版
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

namespace SF.Core.GenericServices.ServicesAsync
{

    public interface ICreateServiceAsync<in TEntity> where TEntity : class
    {
        /// <summary>
        /// This adds a new entity class to the database with error checking
        /// </summary>
        /// <param name="newItem"></param>
        /// <returns>status</returns>
        Task<ISuccessOrErrors> CreateAsync(TEntity newItem);
    }

    public interface ICreateServiceAsync<TEntity, TDto>
        where TEntity : class, new()
        where TDto : EfGenericDtoAsync<TEntity, TDto>, new()
    {

        /// <summary>
        /// This uses a dto to create a data class which it writes to the database with error checking
        /// </summary>
        /// <param name="dto">If an error then its resets any secondary data so that you can reshow the dto</param>
        /// <returns>status</returns>
        Task<ISuccessOrErrors> CreateAsync(TDto dto);

        /// <summary>
        /// This is available to reset any secondary data in the dto. Call this if the ModelState was invalid and
        /// you need to display the view again with errors
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<TDto> ResetDtoAsync(TDto dto);
    }
}
