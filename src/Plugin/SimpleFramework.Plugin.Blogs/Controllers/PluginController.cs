using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SimpleFramework.Core.Plugins.Mvc;
using SimpleFramework.Core.Plugins.Abstraction;

namespace SimpleFramework.Plugin.Blogs.Controllers
{
    public class PluginController : BasePluginController
    {

        public override IPlugin Plugin
        {
            get
            {
                return new Blog();
            }
        }

        public PluginController( IPluginSettingsManager pluginSettingsManager) :
            base( pluginSettingsManager)
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Test()
        {
            var obj = JsonConverter.Equals(new { }, new { });

            return Content("hi");
        }

    }
}
