/*******************************************************************************
* 命名空间: SF.Core.GenericServices.Base
*
* 功 能： N/A
* 类 名： ICreateService
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/8 17:25:54 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/

namespace SF.Core.GenericServices.Services
{
    public interface ICreateSetupService
    {
        /// <summary>
        /// This returns the dto with any data that is needs for the view setup in it
        /// </summary>
        /// <typeparam name="TDto">The type of the data to output. This must be EfGeneric Dto</typeparam>
        /// <returns>The dto with any secondary data filled in</returns>
        TDto GetDto<TDto>() where TDto : class;
    }
}
