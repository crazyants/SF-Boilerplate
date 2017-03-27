
using SF.Entitys.Abstraction;
using System;


namespace SF.Module.LoggingStorage.Models
{
    public class LogItem : EntityWithTypedId<Guid>
    {
        public LogItem()
        {
            Id = Guid.NewGuid();
        }


        public DateTime LogDateUtc { get; set; } = DateTime.UtcNow;


        public string IpAddress { get; set; }


        public string Culture { get; set; }


        public string Url { get; set; }


        public string ShortUrl { get; set; }


        public string Thread { get; set; }


        public string LogLevel { get; set; }


        public string Logger { get; set; }


        public string Message { get; set; }


        public string StateJson { get; set; }

        public int EventId { get; set; }

        public static LogItem FromLogItem(LogItem item)
        {
            if (item is LogItem) { return item as LogItem; }

            var log = new LogItem();
            log.Culture = item.Culture;
            log.IpAddress = item.IpAddress;
            log.LogDateUtc = item.LogDateUtc;
            log.Logger = item.Logger;
            log.LogLevel = item.LogLevel;
            log.Message = item.Message;
            log.ShortUrl = item.ShortUrl;
            log.StateJson = item.StateJson;
            log.Thread = item.Thread;
            log.Url = item.Url;
            log.EventId = item.EventId;

            return log;
        }


    }
}
