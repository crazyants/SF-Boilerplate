using System;
using System.Collections.Generic;
using SF.Core.Errors.Internal;

namespace SF.Core.Errors.Exceptions
{
    public class ValidationException : BaseException
    {
        public ValidationException(string message = Defaults.ValidationException.Title, Exception exception = null, Dictionary<string, IEnumerable<string>> messages = null)
            : base(message, exception, messages)
        { }
    }
}
