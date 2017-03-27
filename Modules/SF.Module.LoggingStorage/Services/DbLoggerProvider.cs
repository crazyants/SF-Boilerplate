

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using SF.Module.LoggingStorage.Data;

namespace SF.Module.LoggingStorage.Services
{
    public class DbLoggerProvider : ILoggerProvider
    {
        public DbLoggerProvider(
            Func<string, LogLevel, bool> filter,
            IServiceProvider serviceProvider
            ,ILogRepository logRepository = null
            )
        {
            logRepo = logRepository;
            services = serviceProvider;
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            _filter = filter;
        }

        private ILogRepository logRepo;
        private IServiceProvider services;
        private readonly Func<string, LogLevel, bool> _filter;


        public ILogger CreateLogger(string name)
        {
            if(logRepo == null)
            {
                logRepo = services.GetRequiredService<ILogRepository>();
            }
            
            return new DbLogger(name, _filter, services, logRepo);
        }

        public void Dispose()
        {
        }

    }
}
