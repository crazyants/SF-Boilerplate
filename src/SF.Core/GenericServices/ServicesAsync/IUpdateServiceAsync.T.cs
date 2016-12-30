/*******************************************************************************
* 命名空间: SF.Core.GenericServices.ServicesAsync
*
* 功 能： N/A
* 类 名： IUpdateServiceAsync
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/8 17:38:58 疯狂蚂蚁 初版
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

    public interface IUpdateServiceAsync<in TEntity> where TEntity : class
    {
        /// <summary>
        /// This updates the entity data class directly
        /// </summary>
        /// <param name="itemToUpdate"></param>
        /// <returns>status</returns>
        Task<ISuccessOrErrors> UpdateAsync(TEntity itemToUpdate);
    }

    public interface IUpdateServiceAsync<TEntity, TDto>
        where TEntity : class, new()
        where TDto : EfGenericDtoAsync<TEntity, TDto>, new()
    {
        /// <summary>
        /// This updates the entity data by copying over the relevant dto data.
        /// If it fails it resets the dto in case it is going to be shown again
        /// </summary>
        /// <param name="dto">If an error then its resets any secondary data so that you can reshow the dto</param>
        /// <returns>status</returns>
        Task<ISuccessOrErrors> UpdateAsync(TDto dto);

        /// <summary>
        /// This is available to reset any secondary data in the dto. Call this if the ModelState was invalid and
        /// you need to display the view again with errors
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<TDto> ResetDtoAsync(TDto dto);
    }
}
