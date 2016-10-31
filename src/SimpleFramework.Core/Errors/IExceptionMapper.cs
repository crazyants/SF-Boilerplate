using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Errors
{
    public interface IExceptionMapper
    {
        Error Resolve(Exception exception);
    }
}
