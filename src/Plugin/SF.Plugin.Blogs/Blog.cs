using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SF.Core.Plugins;
using SF.Core.Plugins.Abstraction;
using SF.Core.Plugins.Models;

namespace SF.Plugin.Blogs
{
    public class Blog : BasePlugin, IPlugin
    {

        public string Name
        {
            get
            {
                return "ModBlog";
            }
        }

        public string Version
        {
            get
            {
                return "1.0";
            }
        }

        public string Description
        {
            get
            {
                return "This plugin is used to post blogs and read them.";
            }
        }

        public ICollection<IPluginRoute> Routes
        {
            get
            {
                var routes = new List<IPluginRoute>();
                routes.MapPluginRoute(
                   name: "blogDefault",
                    template: "Blog/{controller=Home}/{action=Index}/{id?}",
                    plugin: new Blog());


                return routes;
            }
        }

        public ICollection<ServiceDescriptor> Services
        {
            get
            {
                var list = new List<ServiceDescriptor>();

                return list;
            }
        }

        public PluginInstallResult Install()
        {
            return new PluginInstallResult();
        }

        public PluginInstallResult UnInstall()
        {
            return new PluginInstallResult();
        }
    }
}
