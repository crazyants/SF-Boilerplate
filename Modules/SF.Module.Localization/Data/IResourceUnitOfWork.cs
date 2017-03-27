/*******************************************************************************
* 命名空间: SF.Module.Localization.Data
*
* 功 能： N/A
* 类 名： IResourceUnitOfWork
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/11 14:56:44 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SF.Core.Abstraction.UoW;
using SF.Module.Localization.Data.Repository;

namespace SF.Module.Localization.Data
{
    public interface IResourceUnitOfWork : IUnitOfWork
    {
        #region Repository

        IResourceRepository Resource { get; }

        #endregion
    }
}
