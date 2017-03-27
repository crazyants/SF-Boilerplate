using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediatR;


namespace SF.Module.Backend.Controllers
{
    public class PluginController : Controller
    {
        //private IPluginManager _pluginManager;
        //private readonly IMediator _mediator;
        //public PluginController(IPluginManager pluginManager, IMediator mediator)
        //{
        //    _pluginManager = pluginManager;
        //    _mediator = mediator;
        //}

        //public IActionResult Activate()
        //{
        //    foreach(var plugin in _pluginManager.AvailablePluginAssemblies)
        //    {
        //        _pluginManager.ActivatePlugin(plugin.Item1);

        //    }
        //    //插件安装操作日志
        //    // _mediator.Publish(new ActivityHappened { ActivityTypeId = 1, EntityId = 1, EntityTypeId = 3, TimeHappened = DateTimeOffset.Now });

        //    return Content("Activated");
        //}


    }
}
