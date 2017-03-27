
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;
using SF.Module.LoggingStorage.Data;
using SF.Module.LoggingStorage.Models;

namespace SF.Module.LoggingStorage.Services
{
    public class LogManager
    {
        public LogManager(
            ILogRepository logRepository,
            IHttpContextAccessor contextAccessor)
        {
            logRepo = logRepository;
            _context = contextAccessor?.HttpContext;
        }

        private readonly HttpContext _context;
        private CancellationToken CancellationToken => _context?.RequestAborted ?? CancellationToken.None;
        private ILogRepository logRepo;

        public int LogPageSize { get; set; } = 10;

        public async Task<PagedQueryResult> GetLogsDescending(int pageNumber, int pageSize, string logLevel = "")
        {
            return await logRepo.GetPageDescending(pageNumber, pageSize, logLevel, CancellationToken);
        }

        public async Task<PagedQueryResult> GetLogsAscending(int pageNumber, int pageSize, string logLevel = "")
        {
            return await logRepo.GetPageAscending(pageNumber, pageSize, logLevel, CancellationToken);
        }

        public async Task DeleteLogItem(Guid id)
        {
            await logRepo.Delete(id, CancellationToken.None);
        }

        public async Task DeleteAllLogItems(string logLevel = "")
        {
            await logRepo.DeleteAll(logLevel, CancellationToken.None);
        }

        public async Task DeleteOlderThan(DateTime cutoffUtc, string logLevel = "")
        {
            await logRepo.DeleteOlderThan(cutoffUtc, logLevel, CancellationToken.None);
        }

    }
}
