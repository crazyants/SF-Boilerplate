/*******************************************************************************
* 命名空间: SF.Core.EFCore.UoW
*
* 功 能： N/A
* 类 名： EFCoreWorkArea
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/11 10:58:56 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using Microsoft.EntityFrameworkCore;
using System;
 

namespace SF.Core.EFCore.UoW
{
    /// <summary>
    /// Represents a work area that can be used for aggregating
    /// UoW properties, specialized for the Entity Framework Core
    /// </summary>
    /// <typeparam name="TDbContext">The database context type</typeparam>
    public abstract class EFCoreWorkArea<TDbContext> : IEFCoreWorkArea<TDbContext>
        where TDbContext : DbContext
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="context">The database context</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected EFCoreWorkArea(TDbContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            Context = context;
        }

        #region Implementation of IEFCoreWorkArea<out TDbContext>

        /// <summary>
        /// The Entity Framework database context
        /// </summary>
        public TDbContext Context { get; }

        #endregion
    }

    /// <summary>
    /// Represents a work area that can be used for aggregating
    /// UoW properties, specialized for the Entity Framework Core
    /// </summary>
    public abstract class EFCoreWorkArea : EFCoreWorkArea<DbContext>, IEFCoreWorkArea
    {
        /// <summary>
        /// Creates a new instance
        /// </summary>
        /// <param name="context">The database context</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected EFCoreWorkArea(DbContext context) : base(context)
        {

        }
    }
}
