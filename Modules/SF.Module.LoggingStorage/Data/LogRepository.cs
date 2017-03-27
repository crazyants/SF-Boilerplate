
using Microsoft.EntityFrameworkCore;
using SF.Core.Abstraction.Data;
using SF.Data;
using SF.Module.LoggingStorage.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SF.Module.LoggingStorage.Data
{
    public class LogRepository : ILogRepository, IDisposable
    {
        private readonly DbContext dbContext;

        public LogRepository(CoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
     

        public void AddLogItem(LogItem log)
        {
            // since we are using EF to add to the log we need ot avoid
            // logging EF related things, otherwise every time we log we generate more log events
            // continuously
            // might be better to use the normal mssql ado log repository instead
            // need to decouple logging repos from core repos

            if (log.Logger.Contains("EntityFrameworkCore")) return;

            var logItem = LogItem.FromLogItem(log);

            dbContext.Add(logItem);
            dbContext.SaveChanges();


            // learned by experience for this situation we need to create transient instance of the dbcontext
            // for logging because the dbContext we have passed in is scoped to the request
            // and it causes problems to save changes on the context multiple times during a request
            // since we may log mutliple log items in a given request we need to create the dbcontext as needed
            // we can still use the normal dbContext for querying
            //dbContext.Add(logItem);
            //dbContext.SaveChanges();

            //return logItem.Id;
        }

        public async Task<int> GetCount(string logLevel = "", CancellationToken cancellationToken = default(CancellationToken))
        {
            return await dbContext.Set<LogItem>()
                .Where(l => (logLevel == "" || l.LogLevel == logLevel))
                .CountAsync<LogItem>(cancellationToken);
        }


        public async Task<PagedQueryResult> GetPageAscending(
            int pageNumber,
            int pageSize,
            string logLevel = "",
            CancellationToken cancellationToken = default(CancellationToken))
        {
            int offset = (pageSize * pageNumber) - pageSize;

            var query = dbContext.Set<LogItem>().OrderBy(x => x.LogDateUtc)
                .Where(l => (logLevel == "" || l.LogLevel == logLevel))
                .Skip(offset)
                .Take(pageSize)
                .Select(p => p)
                ;

            var result = new PagedQueryResult();

            result.Items = await query.AsNoTracking().ToListAsync<LogItem>(cancellationToken);
            result.TotalItems = await GetCount(logLevel, cancellationToken);
            return result;
        }

        public async Task<PagedQueryResult> GetPageDescending(
            int pageNumber,
            int pageSize,
            string logLevel = "",
            CancellationToken cancellationToken = default(CancellationToken))
        {
            int offset = (pageSize * pageNumber) - pageSize;

            var query = dbContext.Set<LogItem>().OrderByDescending(x => x.LogDateUtc)
                .Where(l => (logLevel == "" || l.LogLevel == logLevel))
                .Skip(offset)
                .Take(pageSize)
                .Select(p => p)
                ;

            var result = new PagedQueryResult();

            result.Items = await query.AsNoTracking().ToListAsync<LogItem>(cancellationToken);
            result.TotalItems = await GetCount(logLevel, cancellationToken);
            return result;

        }

        public async Task DeleteAll(
            string logLevel = "",
            CancellationToken cancellationToken = default(CancellationToken))
        {

            if (string.IsNullOrWhiteSpace(logLevel))
            {
                dbContext.Set<LogItem>().RemoveAll();
            }
            else
            {
                var query = from l in dbContext.Set<LogItem>()
                            where l.LogLevel == logLevel
                            select l;

                dbContext.Set<LogItem>().RemoveRange(query);
            }


            int rowsAffected = await dbContext.SaveChangesAsync(cancellationToken);


        }

        public async Task Delete(
            Guid logItemId,
            CancellationToken cancellationToken = default(CancellationToken))
        {

            var itemToRemove = await dbContext.Set<LogItem>().SingleOrDefaultAsync(x => x.Id.Equals(logItemId));
            if (itemToRemove != null)
            {
                dbContext.Set<LogItem>().Remove(itemToRemove);
                int rowsAffected = await dbContext.SaveChangesAsync(cancellationToken);

            }


        }

        public async Task DeleteOlderThan(
            DateTime cutoffDateUtc,
            string logLevel = "",
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(logLevel))
            {
                var query = from l in dbContext.Set<LogItem>()
                            where l.LogDateUtc < cutoffDateUtc
                            select l;

                dbContext.Set<LogItem>().RemoveRange(query);
            }
            else
            {
                var query = from l in dbContext.Set<LogItem>()
                            where l.LogDateUtc < cutoffDateUtc
                            && (l.LogLevel == logLevel)
                            select l;

                dbContext.Set<LogItem>().RemoveRange(query);
            }

            int rowsAffected = await dbContext.SaveChangesAsync(cancellationToken);

        }

        #region IDisposable Support

        private void ThrowIfDisposed()
        {
            if (disposedValue)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        private bool disposedValue = false; // To detect redundant calls

        void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects).
                    if (dbContext != null)
                    {
                        dbContext.Dispose();
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }


        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }

        #endregion

    }
}
