/*******************************************************************************
* 命名空间: SF.Core.EFCore.UoW
*
* 功 能： N/A
* 类 名： UnitOfWorkFactory
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/11 14:10:52 疯狂蚂蚁 初版
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
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly IServiceProvider _service;

        public UnitOfWorkFactory(IServiceProvider service)
        {
            _service = service;
        }

        public TUoW Get<TUoW>() where TUoW : IUnitOfWork
        {
            return (TUoW)_service.GetService(typeof(TUoW));
        }

        public IUnitOfWork Get(Type uowType)
        {
            return (IUnitOfWork)_service.GetService(uowType);
        }

        public void Release(IUnitOfWork unitOfWork)
        {
            var asDisposable = unitOfWork as IDisposable;
            asDisposable?.Dispose();
        }
    }
}
