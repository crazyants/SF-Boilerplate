/*******************************************************************************
* 命名空间: SF.Data
*
* 功 能： N/A
* 类 名： ExampleUnitOfWork
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/11 15:14:46 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SF.Core.EFCore.UoW;
using SF.Data.WorkArea;
using SF.Core.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SF.Core.Abstraction.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace SF.Data
{

    public class BaseUnitOfWork : EFCoreUnitOfWork, IBaseUnitOfWork
    {
        public BaseUnitOfWork(CoreDbContext context, IEnumerable<IInterceptor> interceptors) : base(context, interceptors)
        {
            BaseWorkArea = new BaseWorkArea(context);
        }

        public IBaseWorkArea BaseWorkArea { get; }

    
    }
}
