using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SF.Core.Abstraction.Interceptors
{
    /// <summary>
    /// 拦截接口
    /// </summary>
    public interface IInterceptor
    {
        void Before(InterceptionContext context);
        void After(InterceptionContext context);
    }
}
