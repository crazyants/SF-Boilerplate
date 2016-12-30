/*******************************************************************************
* 命名空间: SF.Core.GenericServices.Base
*
* 功 能： N/A
* 类 名： ICreateSetupService
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/8 17:27:11 疯狂蚂蚁 初版
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
    public interface ICreateSetupService<TEntity, out TDto>
      where TEntity : class, new()
      where TDto : EfGenericDto<TEntity, TDto>, new()
    {
        /// <summary>
        /// This returns the dto with any data that is needs for the view setup in it
        /// </summary>
        /// <returns>A TDto which, if required, will have SetupSecondaryData method called on it</returns>
        TDto GetDto();
    }
}
