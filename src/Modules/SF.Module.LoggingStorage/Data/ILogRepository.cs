
using SF.Module.LoggingStorage.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SF.Module.LoggingStorage.Data
{
    public interface ILogRepository
    {
        void AddLogItem(LogItem logItem);

        //Task<int> GetCount(string logLevel = "", CancellationToken cancellationToken = default(CancellationToken));

        Task<PagedQueryResult> GetPageAscending(
            int pageNumber,
            int pageSize,
            string logLevel = "",
            CancellationToken cancellationToken = default(CancellationToken));

        Task<PagedQueryResult> GetPageDescending(
            int pageNumber,
            int pageSize,
            string logLevel = "",
            CancellationToken cancellationToken = default(CancellationToken));

        Task DeleteAll(string logLevel = "", CancellationToken cancellationToken = default(CancellationToken));
        Task Delete(Guid logItemId, CancellationToken cancellationToken = default(CancellationToken));
        Task DeleteOlderThan(DateTime cutoffDateUtc, string logLevel = "", CancellationToken cancellationToken = default(CancellationToken));
        
    }
}
