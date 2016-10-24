using SimpleFramework.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using SimpleFramework.Core;
using SimpleFramework.Core.Security;
using SimpleFramework.Core.Abstraction.Data.UnitOfWork;
using SimpleFramework.Core.Data;
using SimpleFramework.Core.Interceptors;
using SimpleFramework.Core.UI;

namespace SimpleFramework.Module.Backend
{
    public class ModuleInitializer : ModuleInitializerBase, IModuleInitializer
    {
        public override IBackendMetadata BackendMetadata
        {
            get
            {
                return new BackendMetadata();
            }
        }

    }
}
