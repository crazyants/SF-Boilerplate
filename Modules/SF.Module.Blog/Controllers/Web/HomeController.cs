using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace SF.Module.Blog.Controllers
{

    //[Authorize(Roles = "Administrators")]
   [Route("Blog")]
    public class HomeController : Controller
    {
        /// <summary>
        /// Index默认可以不加路由特性
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return new ContentResult() {  Content="hello sf"};
        }

        #region 试图

        /// <summary>
        /// 注意必须添加路由特性，因为Controller易加入路由  [Route("Blog")]
        /// </summary>
        /// <returns></returns>
        [Route("Form")]
        public ActionResult Form()
        {
            return View();
        }
        #endregion
    }
}
