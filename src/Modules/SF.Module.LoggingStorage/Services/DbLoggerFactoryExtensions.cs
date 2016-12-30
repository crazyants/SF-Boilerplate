
using System;
using Microsoft.Extensions.DependencyInjection;
using SF.Module.LoggingStorage.Services;
using SF.Module.LoggingStorage.Data;

namespace Microsoft.Extensions.Logging
{
    public static class DbLoggerFactoryExtensions
    {
        public static ILoggerFactory AddDbLogger(
            this ILoggerFactory factory,
            IServiceProvider serviceProvider,
            LogLevel minimumLogLevel)
        {
            Func<string, LogLevel, bool> logFilter = delegate (string loggerName, LogLevel logLevel)
            {
                if (logLevel < minimumLogLevel) { return false; }
               
                return true;
            };
            factory.AddProvider(new DbLoggerProvider(logFilter, serviceProvider));
            return factory;
        }

        public static ILoggerFactory AddDbLogger(
            this ILoggerFactory factory,
            IServiceProvider serviceProvider,
            Func<string, LogLevel, bool> logFilter)
        {
            factory.AddProvider(new DbLoggerProvider(logFilter, serviceProvider));
            return factory;
        }

        public static ILoggerFactory AddDbLogger(
            this ILoggerFactory factory,
            IServiceProvider serviceProvider,
            Func<string, LogLevel, bool> logFilter
            ,ILogRepository logRepository
            )
        {
            factory.AddProvider(new DbLoggerProvider(logFilter, serviceProvider, logRepository));
            return factory;
        }

    }
}
