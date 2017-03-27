/*******************************************************************************
* 命名空间: SF.Module.SimpleData.Data
*
* 功 能： N/A
* 类 名： ActivityUnitOfWork
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
using SF.Core.Interceptors;
using SF.Module.SimpleData.Data.Repository;
using System.Collections.Generic;

namespace SF.Module.SimpleData.Data
{

    public class SimpleDataUnitOfWork : EFCoreUnitOfWork<UnicornsContext>, ISimpleDataUnitOfWork
    {
        public SimpleDataUnitOfWork(UnicornsContext context, IEnumerable<IInterceptor> interceptors) : base(context, interceptors)
        {
            Activity = new LodgingRepository(context);
        }

        public ILodgingRepository Activity { get; }
    }
}
