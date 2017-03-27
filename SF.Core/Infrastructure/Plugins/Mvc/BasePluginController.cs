using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using SF.Core.Infrastructure.Plugins.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.Infrastructure.Plugins.Mvc
{
    public abstract class BasePluginController : Controller
    {
        private IPluginSettingsManager _pluginSettingsManager;

        public IPluginSettingsManager PluginSettingsManager
        {
            get
            {
                return _pluginSettingsManager;
            }
        }

        public abstract IPlugin Plugin { get; }


        public BasePluginController( IPluginSettingsManager pluginSettingsManager)
        {
            _pluginSettingsManager = pluginSettingsManager;

        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Items.Add("PluginName", this.Plugin.Name);

            base.OnActionExecuting(context);
        }

    }
}
