 
using System.Collections.Generic;

namespace SF.Module.LoggingStorage.Models
{
    public class PagedQueryResult
    {
        public PagedQueryResult()
        {
            Items = new List<LogItem>();
        }

        public List<LogItem> Items { get; set; }

        public int TotalItems { get; set; } = 0;

    }
}
