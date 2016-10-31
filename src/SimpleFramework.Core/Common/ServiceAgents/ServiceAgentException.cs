using SimpleFramework.Core.Errors.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFramework.Core.ServiceAgents
{
    public class ServiceAgentException : BaseException
    {
        public ServiceAgentException(string message = null, Exception exception = null, Dictionary<string, IEnumerable<string>> messages = null)
            : base(message, exception, messages)
        {
        }
    }
}
