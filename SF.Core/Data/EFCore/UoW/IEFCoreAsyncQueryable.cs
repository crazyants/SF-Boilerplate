/*******************************************************************************
* 命名空间: SF.Core.EFCore.UoW
*
* 功 能： N/A
* 类 名： IEFCoreAsyncQueryable
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/11 10:53:10 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using SF.Core.Abstraction.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.EFCore.UoW
{
    /// <summary>
    /// Specialized <see cref="IQueryable{T}"/> for async executions using the Entity Framework Core.
    /// </summary>
    /// <typeparam name="T">The entity type</typeparam>
    public interface IEFCoreAsyncQueryable<T> : IAsyncQueryable<T>
    {

    }
}
