/*******************************************************************************
* 命名空间: SimpleFramework.Module.Localization.Data.Repository
*
* 功 能： N/A
* 类 名： IResourceRepository
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/11 15:18:03 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/

using SimpleFramework.Core.EFCore.UoW;
using SimpleFramework.Module.Localization.Models;
 
namespace SimpleFramework.Module.Localization.Data.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IResourceRepository :
       IEFCoreQueryableRepository<ResourceEntity, long>
    {
         
    }
}
