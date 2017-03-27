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
using SF.Data;
using SF.Core.EFCore.UoW;
using SF.Entitys;
using SF.Module.Backend.Data.Entitys;


namespace SF.Module.Backend.Data.Uow
{
    public interface IBackendUnitOfWork : IEFCoreUnitOfWork
    {
        #region Repository

        IBaseRepository<DataItemEntity> DataItem { get; }

        IBaseRepository<DataItemDetailEntity> DataItemDetail { get; }

        IBaseRepository<AreaEntity> Area { get; }

        IBaseRepository<ModuleEntity> Module { get; }

        IBaseRepository<PermissionEntity> Permission { get; }

        IBaseRepository<OrganizeEntity> Organize { get; }

        IBaseRepository<DepartmentEntity> Department { get; }

        IBaseRepository<DMOSEntity> DMOS { get; }

        #endregion
    }
}
