using System;
using System.Collections.Generic;
using SF.Core.Errors.Internal;

namespace SF.Core.Errors.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException(string message = Defaults.NotFoundException.Title, Exception exception = null, Dictionary < string, IEnumerable<string>> messages = null )
            : base(message, exception, messages)
        {}
    }
}
