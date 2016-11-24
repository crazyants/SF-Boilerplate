
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Web.Navigation
{
    public interface INodeUrlPrefixProvider
    {
        string GetPrefix();
    }

    public class DefaultNodeUrlPrefixProvider : INodeUrlPrefixProvider
    {
        public DefaultNodeUrlPrefixProvider()
        { }

        public string GetPrefix()
        {
            return string.Empty;
        }
    }
}
