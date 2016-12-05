/*******************************************************************************
* 命名空间: SF.Module.Localization.Data.Repository
*
* 功 能： N/A
* 类 名： IResourceRepository
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/11 15:18:03 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/

using SF.Core.EFCore.UoW;
using SF.Module.Localization.Models;
 
namespace SF.Module.Localization.Data.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IResourceRepository :
       IEFCoreQueryableRepository<ResourceEntity, long>
    {
         
    }
}
