using System;
using System.Collections.Generic;
using SF.Core.Errors.Internal;

namespace SF.Core.Errors.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException(string message = Defaults.UnauthorizedException.Title, Exception exception = null, Dictionary<string, IEnumerable<string>> messages = null)
            : base(message, exception, messages)
        { }
    }
}
