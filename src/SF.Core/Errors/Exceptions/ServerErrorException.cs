using System;
using System.Collections.Generic;
using SF.Core.Errors.Internal;

namespace SF.Core.Errors.Exceptions
{
    public class ServerErrorException : BaseException
    {
        public ServerErrorException(string message = Defaults.ServerErrorException.Title, Exception exception = null, Dictionary < string, IEnumerable<string>> messages = null )
            : base(message, exception, messages)
        {}
    }
}
