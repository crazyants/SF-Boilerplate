/*******************************************************************************
* 命名空间: SF.Data
*
* 功 能： N/A
* 类 名： IPluginsUnitOfWork
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
using SF.Data.WorkArea;
using SF.Web.Plugins.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Web.Plugins.Data
{
    public interface IPluginsUnitOfWork : IUnitOfWork
    {
        #region Repository

        IPluginRepository Plugin { get; }

        #endregion
    }
}
