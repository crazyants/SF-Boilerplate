using System;
using System.Collections.Generic;
using System.Text;

namespace SF.Core.Abstraction.GenericServices
{
    /// <summary>
    /// 基础服务基类
    /// </summary>
    public abstract class ServiceBase : IServiceBase
    {
        public virtual void Dispose()
        {

        }
    }
}
