using System;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

using SF.Module.Backend.Services;
using CacheManager.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SF.Core.Extensions;
using System.Linq;
using SF.Core.Storage;

namespace SF.Module.Backend.Controllers
{
    [Authorize(Roles = "Administrators")]
    [Route("api/common")]
    public class CommonApiController : SF.Web.Base.Controllers.ControllerBase
    {
        private readonly IMediaService mediaService;
        private readonly ICacheManager<object> _cacheManager;
        private readonly IFileStorage _fileStorage;
        public CommonApiController(IServiceCollection collection, ILogger<DataItemController> logger,
            IMediaService mediaService, ICacheManager<object> cacheManager,
            IFileStorage fileStorage) : base(collection, logger)
        {
            this.mediaService = mediaService;
            this._cacheManager = cacheManager;
            this._fileStorage = fileStorage;
        }

        [HttpPost("upload")]
        public IActionResult UploadFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            mediaService.SaveMedia(file.OpenReadStream(), fileName, file.ContentType);
            return Ok(mediaService.GetMediaUrl(fileName));
        }
        [HttpPost("emdupload")]
        public IActionResult EditormdUploadFile()
        {

            var fileItem = Request.Form.Files.FirstOrDefault();
            var originalFileName = ContentDispositionHeaderValue.Parse(fileItem.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            var path = $"upload\\{DateTime.UtcNow.ToString("yyyyMMdd")}\\{fileName}";
            var result = this._fileStorage.SaveFileAsync(path, fileItem.OpenReadStream());
            var data = new
            {
                success = result.Result ? 1 : 0,           // 0 表示上传失败，1 表示上传成功
                message = result.Result ? "" : "提示的信息，上传成功或上传失败及错误信息等。",
                url = "/" + path.Replace("\\", "/")        // 上传成功时才返回
            };

            return Json(data);
        }
        [Route("ClearCache")]
        public IActionResult ClearCache(string previousUrl)
        {
            this._cacheManager.Clear();
            if (previousUrl.HasValue())
                return Redirect(previousUrl);
            return RedirectToAction("Index", "Home");
        }
    }
}
