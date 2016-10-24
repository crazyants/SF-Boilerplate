using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleFramework.Core.Interceptors
{
    public interface IInterceptor
    {
        void Before(InterceptionContext context);
        void After(InterceptionContext context);
    }
}
