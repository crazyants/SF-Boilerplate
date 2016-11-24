 
using System.Collections.Generic;

namespace SimpleFramework.Module.EFLogging.Models
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
