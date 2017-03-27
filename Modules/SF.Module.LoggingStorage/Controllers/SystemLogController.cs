
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SF.Module.LoggingStorage.Models;
using SF.Module.LoggingStorage.Services;
using SF.Module.LoggingStorage.ViewModels;
using System;
using System.Threading.Tasks;

namespace SF.Module.LoggingStorage.Controllers
{
    [Authorize(Roles = "Administrators")]
    [Route("SystemLog/")]
    public class SystemLogController : Controller
    {

        public SystemLogController(
            LogManager logManager)
        {
            this.logManager = logManager;
        }

        private LogManager logManager;


        public async Task<IActionResult> Index(
            string logLevel = "",
            int pageNumber = 1,
            int pageSize = -1,
            string sort = "desc")
        {
            ViewData["Title"] = "System Log";
            ViewData["Heading"] = "System Log";

            int itemsPerPage = logManager.LogPageSize;
            if (pageSize > 0)
            {
                itemsPerPage = pageSize;
            }

            var model = new LogListViewModel();
            model.LogLevel = logLevel;
            PagedQueryResult result;
            if (sort == "desc")
            {
                result = await logManager.GetLogsDescending(pageNumber, itemsPerPage, logLevel);
            }
            else
            {
                result = await logManager.GetLogsAscending(pageNumber, itemsPerPage, logLevel);
            }

            //   model.TimeZoneId = await timeZoneIdResolver.GetUserTimeZoneId();

            model.LogPage = result.Items;

            model.Paging.CurrentPage = pageNumber;
            model.Paging.ItemsPerPage = itemsPerPage;
            model.Paging.TotalItems = result.TotalItems;

            return View(model);

        }
        [Route("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogItemDelete(Guid id)
        {
            await logManager.DeleteLogItem(id);

            return RedirectToAction("Index");
        }
        [Route("DeleteAll")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogDeleteAll(string logLevel = "")
        {
            await logManager.DeleteAllLogItems(logLevel);

            return RedirectToAction("Index");
        }
        [Route("DeleteOlderThan")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogDeleteOlderThan(string logLevel = "", int days = 5)
        {
            var cutoffUtc = DateTime.UtcNow.AddDays(-days);

            await logManager.DeleteOlderThan(cutoffUtc, logLevel);

            return RedirectToAction("Index");
        }

    }
}
