using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.Infrastructure.Plugins
{
    public abstract class BasePlugin
    {
        
        public string AssemblyName
        {
            get
            {
               return this.GetType().Namespace;
            }
        }

    }
}
