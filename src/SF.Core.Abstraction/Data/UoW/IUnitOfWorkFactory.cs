/*******************************************************************************
* 命名空间: SF.Core.Abstraction.UoW
*
* 功 能： N/A
* 类 名： IUnitOfWorkFactory
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/11 10:38:04 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.Abstraction.UoW
{
    /// <summary>
    /// Interface to a factory of <see cref="IUnitOfWork"/> objects.
    /// </summary>
    public interface IUnitOfWorkFactory
    {
        /// <summary>
        /// Gets a given <see cref="IUnitOfWork"/> of the specified type
        /// </summary>
        /// <typeparam name="TUoW">The <see cref="IUnitOfWork"/> type</typeparam>
        /// <returns>An <see cref="IUnitOfWork"/> instance</returns>
        TUoW Get<TUoW>() where TUoW : IUnitOfWork;

        /// <summary>
        /// Gets a given <see cref="IUnitOfWork"/> of the specified type
        /// </summary>
        /// <param name="uowType">The <see cref="IUnitOfWork"/> type</param>
        /// <returns>An <see cref="IUnitOfWork"/> instance</returns>
        IUnitOfWork Get(Type uowType);

        /// <summary>
        /// Releases a given <see cref="IUnitOfWork"/> 
        /// </summary>
        /// <param name="unitOfWork">The instance to be released</param>
        void Release(IUnitOfWork unitOfWork);
    }
}
