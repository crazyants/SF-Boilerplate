/*******************************************************************************
* 命名空间: SF.Core.GenericServices.ServicesAsync
*
* 功 能： N/A
* 类 名： ICreateSetupServiceAsync
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/8 17:38:10 疯狂蚂蚁 初版
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
    public interface ICreateSetupServiceAsync<TEntity, TDto>
       where TEntity : class, new()
       where TDto : EfGenericDtoAsync<TEntity, TDto>, new()
    {
        /// <summary>
        /// This returns the dto with any data that is needs for the view setup in it
        /// </summary>
        /// <returns>An async Task TDto which has had the SetupSecondaryData method called on it</returns>
        Task<TDto> GetDtoAsync();
    }
}
