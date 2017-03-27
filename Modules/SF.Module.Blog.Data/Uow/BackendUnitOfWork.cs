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
using SF.Module.Blog.Data.Entitys;
using SF.Entitys;
using System;

namespace SF.Module.Blog.Data.Uow
{

    public class BlogUnitOfWork : EFCoreUnitOfWork, IBlogUnitOfWork
    {
        public BlogUnitOfWork(BlogContext context, IEnumerable<IInterceptor> interceptors) : base(context, interceptors)
        {
            Post = new BaseRepository<PostEntity, Guid>(context);
        
        }

        public IBaseRepository<PostEntity,Guid> Post { get; }

     
    }
}
