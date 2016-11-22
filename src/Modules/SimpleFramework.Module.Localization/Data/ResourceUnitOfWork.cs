/*******************************************************************************
* 命名空间: SimpleFramework.Module.Localization.Data
*
* 功 能： N/A
* 类 名： ResourceUnitOfWork
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/11 15:14:46 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SimpleFramework.Core.Abstraction.Interceptors;
using SimpleFramework.Core.Data;
using SimpleFramework.Core.EFCore.UoW;
using SimpleFramework.Core.Interceptors;
using SimpleFramework.Module.Localization.Data.Repository;

namespace SimpleFramework.Module.Localization.Data
{

    public class ResourceUnitOfWork : EFCoreUnitOfWork<CoreDbContext>, IResourceUnitOfWork
    {
        public ResourceUnitOfWork(CoreDbContext context, params IInterceptor[] interceptors) : base(context, interceptors)
        {
            Resource = new ResourceRepository(context);
        }

        public IResourceRepository Resource { get; }
    }
}
