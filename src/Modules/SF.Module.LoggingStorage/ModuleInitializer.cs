using SF.Core.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SF.Module.LoggingStorage.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using SF.Module.LoggingStorage.Data;
using SF.Module.LoggingStorage.Models;

using Microsoft.AspNetCore.Builder;
using SF.Core;

namespace SF.Module.LoggingStorage
{
    public class ModuleInitializer :  ModuleInitializerBase, IModuleInitializer
    {

        public override IEnumerable<KeyValuePair<int, Action<IServiceCollection>>> ConfigureServicesActionsByPriorities
        {
            get
            {
                return new Dictionary<int, Action<IServiceCollection>>()
                {
                    [0] = this.AddEFLoggingService
                };
            }
        }

        public override IEnumerable<KeyValuePair<int, Action<IApplicationBuilder>>> ConfigureActionsByPriorities
        {
            get
            {
                return new Dictionary<int, Action<IApplicationBuilder>>()
                {
                    [0] = this.UserEFLogging
                };
            }
        }

        private void AddEFLoggingService(IServiceCollection serviceCollection)
        {

            serviceCollection.AddScoped<ILogRepository, LogRepository>();
            serviceCollection.AddScoped<LogManager>();

        }

        private void UserEFLogging(IApplicationBuilder applicationBuilder)
        {
            var loggerFactory = applicationBuilder.ApplicationServices.GetService<ILoggerFactory>();

            var logRepo = applicationBuilder.ApplicationServices.GetService<ILogRepository>();
            // a customizable filter for logging
            LogLevel minimumLevel = LogLevel.Information;

            // add exclusions to remove noise in the logs
            var excludedLoggers = new List<string>
            {
                "Microsoft.AspNetCore.StaticFiles.StaticFileMiddleware",
                "Microsoft.AspNetCore.Hosting.Internal.WebHost",
            };

            Func<string, LogLevel, bool> logFilter = (string loggerName, LogLevel logLevel) =>
            {
                if (logLevel < minimumLevel)
                {
                    return false;
                }

                if (excludedLoggers.Contains(loggerName))
                {
                    return false;
                }

                return true;
            };

            loggerFactory.AddDbLogger(applicationBuilder.ApplicationServices, logFilter, logRepo);



        }
    }
}
