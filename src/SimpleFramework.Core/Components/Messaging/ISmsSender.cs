using SimpleFramework.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SimpleFramework.Core.Components.Messaging
{
    public interface ISmsSender
    {
        Task SendSmsAsync(ISiteContext site, string number, string message);
    }
}
