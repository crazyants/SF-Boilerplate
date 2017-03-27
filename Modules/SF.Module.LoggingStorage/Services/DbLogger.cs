

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SF.Module.LoggingStorage.Data;
using SF.Module.LoggingStorage.Models;
using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace SF.Module.LoggingStorage.Services
{
    public class DbLogger : ILogger
    {
        public DbLogger(
            string loggerName,
            Func<string, LogLevel, bool> filter,
            IServiceProvider serviceProvider,
            ILogRepository logRepository)
        {
            logger = loggerName;
            logRepo = logRepository;
            services = serviceProvider;
            Filter = filter ?? ((category, logLevel) => true);
        }

        private ILogRepository logRepo;
        private IHttpContextAccessor contextAccessor = null;
        private IServiceProvider services;
        private const int _indentation = 2;
        private string logger = string.Empty;
        private Func<string, LogLevel, bool> _filter;

        public Func<string, LogLevel, bool> Filter
        {
            get { return _filter; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                _filter = value;
            }
        }


        #region ILogger

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception exception,
            Func<TState, Exception, string> formatter
            )
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            var message = formatter(state, exception);

            if (exception != null)
            {
                message += System.Environment.NewLine + exception;
            }
          

            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            contextAccessor = services.GetService<IHttpContextAccessor>();

            var logItem = new LogItem();
            logItem.EventId = eventId.Id;
            logItem.Message = message;
            logItem.IpAddress = GetIpAddress();
            logItem.Culture = CultureInfo.CurrentCulture.Name;
            logItem.Url = GetRequestUrl();

            // lets not log the log viewer
            if (logItem.Url.StartsWith("/SystemLog")) return;
            if (logItem.Url.StartsWith("/systemlog")) return;

            logItem.ShortUrl = GetShortUrl(logItem.Url);
            logItem.Thread = System.Threading.Thread.CurrentThread.Name ;
            logItem.Logger = logger;
            logItem.LogLevel = logLevel.ToString();
            
            try
            {
                logItem.StateJson = JsonConvert.SerializeObject(
                state,
                Formatting.None,
                new JsonSerializerSettings
                {
                    DefaultValueHandling = DefaultValueHandling.Include

                }
                );
            }
            catch(Exception)
            {
                // don't throw exceptions from logger
                //bool foo = true; // just a line to set a breakpoint so I can see the error when debugging
            }

            // an exception is expected here if the db has not yet been populated
            // or if the db is not accessible
            // we cannot allow logging to raise exceptions
            // so we must swallow any exception here
            // would be good if we could only swallow specific exceptions
            try
            {
                logRepo.AddLogItem(logItem);
            }
            catch (Exception)
            {
                //bool foo = true; // just a line to set a breakpoint so I can see the error when debugging
            }
        }


        //private string Serialize(TState obj)
        //{
        //    return JsonConvert.SerializeObject(
        //        obj,
        //        Formatting.None,
        //        new JsonSerializerSettings
        //        {
        //            DefaultValueHandling = DefaultValueHandling.Include
        //            //, DateTimeZoneHandling = DateTimeZoneHandling.Utc  
        //        }
        //        );
        //}



        public bool IsEnabled(LogLevel logLevel)
        {
            //Trace = 1,
            //Debug = 2,
            //Information = 3,
            //Warning = 4,
            //Error = 5,
            //Critical = 6,

            //return (logLevel >= minimumLevel);
            return Filter(logger, logLevel);
        }

        public IDisposable BeginScopeImpl(object state)
        {
            return new NoopDisposable();
        }

        #endregion

        private string GetIpAddress()
        {
            if((contextAccessor != null)&&(contextAccessor.HttpContext != null))
            {
                var connection = contextAccessor.HttpContext.Features.Get<IHttpConnectionFeature>();
                if(connection != null)
                {
                    return connection.RemoteIpAddress.ToString();
                }
                
            }

            return string.Empty;

        }

        private string GetShortUrl(string url)
        {
            if(url.Length > 255)
            {
                return url.Substring(0, 254);
            }

            return url;
        }

        private string GetRequestUrl()
        {
            if ((contextAccessor != null) && (contextAccessor.HttpContext != null))
            {
                return contextAccessor.HttpContext.Request.Path.ToString();
            }

            return string.Empty;
        }

        //private void FormatLogValues(StringBuilder builder, ILogValues logValues, int level, bool bullet)
        //{
        //    var values = logValues.GetValues();
        //    if (values == null)
        //    {
        //        return;
        //    }
        //    var isFirst = true;
        //    foreach (var kvp in values)
        //    {
        //        builder.AppendLine();
        //        if (bullet && isFirst)
        //        {
        //            builder.Append(' ', level * _indentation - 1)
        //                   .Append('-');
        //        }
        //        else
        //        {
        //            builder.Append(' ', level * _indentation);
        //        }
        //        builder.Append(kvp.Key)
        //               .Append(": ");

        //        if (kvp.Value is IEnumerable && !(kvp.Value is string))
        //        {
        //            foreach (var value in (IEnumerable)kvp.Value)
        //            {
        //                if (value is ILogValues)
        //                {
        //                    FormatLogValues(
        //                        builder,
        //                        (ILogValues)value,
        //                        level + 1,
        //                        bullet: true);
        //                }
        //                else
        //                {
        //                    builder.AppendLine()
        //                           .Append(' ', (level + 1) * _indentation)
        //                           .Append(value);
        //                }
        //            }
        //        }
        //        else if (kvp.Value is ILogValues)
        //        {
        //            FormatLogValues(
        //                builder,
        //                (ILogValues)kvp.Value,
        //                level + 1,
        //                bullet: false);
        //        }
        //        else
        //        {
        //            builder.Append(kvp.Value);
        //        }
        //        isFirst = false;
        //    }
        //}

        

        public IDisposable BeginScope<TState>(TState state)
        {
            //throw new NotImplementedException();
            return new NoopDisposable();
        }

        private class NoopDisposable : IDisposable
        {
            public void Dispose()
            {
            }
        }

    }
}
