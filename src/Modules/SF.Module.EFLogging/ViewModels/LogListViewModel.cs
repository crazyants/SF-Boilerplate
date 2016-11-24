
using SimpleFramework.Module.EFLogging.Models;
using SimpleFramework.Web.Pagination;
using System.Collections.Generic;

namespace SimpleFramework.Module.EFLogging.ViewModels
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
