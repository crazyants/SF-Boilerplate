using SimpleFramework.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFramework.Core.Common.Message.Sms
{
    public interface ISmsSender
    {
        Task SendSmsAsync(ISiteContext site, string number, string message);
    }
}
