/*******************************************************************************
* 命名空间: SF.Web.Control.Editor.Controllers
*
* 功 能： N/A
* 类 名： QiniuFileController
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/12/9 9:59:33 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SimpleFramework 版权所有
* Description: SimpleFramework快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Qiniu.Conf;
using Qiniu.IO;
using Qiniu.RS;
using Qiniu.RSF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SF.Web.Control.Editor.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    public class UEditorController : Controller
    {
        #region Private
        private UEditorService ue;
        private JObject setting { set; get; }

        #endregion

        #region Init
        
        public UEditorController(UEditorService ue, IHostingEnvironment env)
        {
            this.ue = ue;
            Qiniu.Conf.Config.ACCESS_KEY = Config.AccessKey;
            Qiniu.Conf.Config.SECRET_KEY = Config.SecretKey;
            Qiniu.Conf.Config.RS_HOST = Config.Domain;

            setting = JObject.Parse(System.IO.File.ReadAllText(Path.Combine(env.ContentRootPath, "config", "config_qiniu.json")));
        }

        #endregion

        #region 本地空间保存
        [Route("Default")]
        public void Default()
        {
            ue.DoAction(HttpContext);
        }

        #endregion

        #region 七牛空间保存
        /// <summary>
        /// 获取七牛令牌信息，主要用于外部使用，如js
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        [Route("GetToken")]
        public IActionResult GetToken(string fileType)
        {
            if (string.IsNullOrEmpty(fileType)) fileType = "file";

            var fileName = GetFileName(fileType);

            var token = new PutPolicy(Config.Bucket).Token();

            return Json(new
            {
                updomain = Qiniu.Conf.Config.UP_HOST,
                fsdomain = Qiniu.Conf.Config.RS_HOST,
                filename = fileName,
                token = token,
                suffix = Config.Suffix
            });
        }
        /// <summary>
        /// 编辑器
        /// </summary>
        /// <returns></returns>
        [Route("QUpload")]
        public async Task<IActionResult> QUpload()
        {
            string action = Request.Query["action"];

            switch (action)
            {
                case "config":
                    return Json(setting);

                case "uploadimage":
                    return Json(await Upload("image"));

                case "uploadfile":
                    return Json(await Upload("file"));

                case "uploadvideo":
                    return Json(await Upload("video"));

                case "uploadscrawl":
                    return Json(await UploadScrawl("scrawl"));

                case "catchimage":
                    return Json(await CatchImage());

                case "listimage":
                    return Json(await ListFile("image"));

                case "listfile":
                    return Json(await ListFile("file"));

                default:
                    return Content("action 参数为空或者 action 不被支持。");
            }
        }

        #region Private

        private async Task<object> Upload(string uploadType = "image")
        {
            var filedName = setting[uploadType + "FieldName"].ToString();
            var maxSize = long.Parse(setting[uploadType + "MaxSize"].ToString());
            var allowTypes = setting[uploadType + "AllowFiles"].Select(p => p.ToString()).ToList();
            var file = Request.Form.Files.FirstOrDefault(p => p.Name == filedName);
            var ext = Path.GetExtension(file.FileName);

            if (file.Length > maxSize) return new { state = "文件大小超出限制。" };
            if (!allowTypes.Contains(ext)) return new { state = "不支持的文件类型。" };

            var fileName = await UploadToQiniu(file.OpenReadStream(), uploadType, uploadType != "image" ? ext : "");    // 图片文件没有加扩展名

            return new
            {
                state = "SUCCESS",
                url = string.Format("{0}/{1}", Config.Domain, fileName),
                title = Path.GetFileNameWithoutExtension(file.FileName),
                original = Path.GetFileNameWithoutExtension(file.FileName)
            };
        }

        private async Task<object> UploadScrawl(string uploadType = "scrawl")
        {
            var filedName = setting["scrawlFieldName"].ToString();
            var maxSize = long.Parse(setting["scrawlMaxSize"].ToString());
            var bytes = Convert.FromBase64String(Request.Form[filedName].ToString());

            if (bytes.Length > maxSize) return new { state = "文件大小超出限制。" };

            var fileName = await UploadToQiniu(new MemoryStream(bytes), uploadType);

            return new
            {
                state = "SUCCESS",
                url = string.Format("{0}/{1}", Config.Domain, fileName)
            };
        }

        private async Task<object> CatchImage()
        {
            var sources = Request.Form["source[]"];
            var list = new List<object>();

            foreach (var item in sources)
            {
                try
                {
                    using (var request = new HttpRequestMessage(HttpMethod.Get, item))
                    {
                        using (var client = new HttpClient())
                        {
                            var response = await client.SendAsync(request);
                            var stream = await response.Content.ReadAsStreamAsync();
                            var fileName = await UploadToQiniu(stream, "image");

                            list.Add(new
                            {
                                state = "SUCCESS",
                                source = item,
                                url = string.Format("{0}/{1}", Config.Domain, fileName)
                            });
                        }
                    }
                }
                catch (Exception e)
                {
                    list.Add(new
                    {
                        state = e.Message,
                        source = item
                    });
                }
            }

            return new
            {
                state = "SUCCESS",
                list = list
            };
        }

        private async Task<object> ListFile(string prefix = "image")
        {
            var start = int.Parse(Request.Query["start"].ToString());
            var size = int.Parse(Request.Query["size"].ToString());

            var rsf = new RSFClient(Config.Bucket);
            var files = await rsf.ListPrefixAsync(Config.Bucket, prefix);

            var list = files.Items.Select(p => string.Format("{0}/{1}{2}", Config.Domain, p.Key, prefix == "image" ? Config.Suffix : "")).OrderByDescending(p => p).Skip(start).Take(size).ToList();

            return new
            {
                state = "SUCCESS",
                list = list == null ? null : list.Select(x => new { url = x }),
                start = start,
                size = size,
                total = files.Items.Count
            };
        }

        private async Task<string> UploadToQiniu(Stream stream, string fileType = "image", string ext = "")
        {
            var target = new IOClient();

            var key = GetFileName(fileType, ext);

            var result = await target.PutAsync(new PutPolicy(Config.Bucket).Token(), key, stream, null);

            return result.key;
        }

        private string GetFileName(string fileType, string ext = "")
        {
            var random = new Random(Guid.NewGuid().GetHashCode());

            return string.Format("{0}/{1}{2}{3}", fileType, DateTime.Now.ToString("yyyy/MM/dd/HHmmss"), random.Next(100000, 1000000), ext);
        }

        #endregion

        #endregion
    }
}