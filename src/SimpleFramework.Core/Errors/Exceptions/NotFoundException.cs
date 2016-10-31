using System;
using System.Collections.Generic;
using SimpleFramework.Core.Errors.Internal;

namespace SimpleFramework.Core.Errors.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message = Defaults.NotFoundException.Title, Exception exception = null, Dictionary < string, IEnumerable<string>> messages = null )
            : base(message, exception, messages)
        {}
    }
}
