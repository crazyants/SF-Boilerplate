
using SF.Module.LoggingStorage.Models;
using SF.Web.Control.Pagination;
using System.Collections.Generic;

namespace SF.Module.LoggingStorage.ViewModels
{
    public class LogListViewModel
    {
        public LogListViewModel()
        {
            LogPage = new List<LogItem>();
            Paging = new PaginationSettings();
        }

        public string LogLevel { get; set; } = string.Empty;
        public List<LogItem> LogPage { get; set; }
        public PaginationSettings Paging { get; set; }
        public string TimeZoneId { get; set; } = "PRC";

    }
}
