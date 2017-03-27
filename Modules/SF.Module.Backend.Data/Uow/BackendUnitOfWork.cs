/*******************************************************************************
* 命名空间: SF.Data
*
* 功 能： N/A
* 类 名： PluginsUnitOfWork
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/11 15:14:46 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SF.Core.Abstraction.Interceptors;
using SF.Data;
using SF.Core.EFCore.UoW;
using System.Collections.Generic;
using SF.Module.Backend.Data.Entitys;
using SF.Entitys;

namespace SF.Module.Backend.Data.Uow
{

    public class BackendUnitOfWork : EFCoreUnitOfWork, IBackendUnitOfWork
    {
        public BackendUnitOfWork(CoreDbContext context, IEnumerable<IInterceptor> interceptors) : base(context, interceptors)
        {
            DataItem = new BaseRepository<DataItemEntity>(context);
            DataItemDetail = new BaseRepository<DataItemDetailEntity>(context);
            Area = new BaseRepository<AreaEntity>(context);
            Module = new BaseRepository<ModuleEntity>(context);
            Permission = new BaseRepository<PermissionEntity>(context);
            Organize = new BaseRepository<OrganizeEntity>(context);
            Department = new BaseRepository<DepartmentEntity>(context);
            DMOS = new BaseRepository<DMOSEntity>(context);
        }

        public IBaseRepository<DataItemEntity> DataItem { get; }

        public IBaseRepository<DataItemDetailEntity> DataItemDetail { get; }

        public IBaseRepository<AreaEntity> Area { get; }

        public IBaseRepository<ModuleEntity> Module { get; }

        public IBaseRepository<PermissionEntity> Permission { get; }

        public IBaseRepository<OrganizeEntity> Organize { get; }

        public IBaseRepository<DepartmentEntity> Department { get; }

        public IBaseRepository<DMOSEntity> DMOS { get; }
    }
}
