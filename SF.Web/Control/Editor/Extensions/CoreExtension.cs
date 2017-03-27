using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using SF.Core;
using SF.Web.Modules;
using SF.Core.Infrastructure.Modules;

namespace SF.Web.Control.Editor
{
    public class CoreExtension : ModuleInitializerBase
    {
        public override IEnumerable<KeyValuePair<int, Action<IServiceCollection>>> ConfigureServicesActionsByPriorities
        {
            get
            {
                return new Dictionary<int, Action<IServiceCollection>>()
                {
                    [3] = this.AddEditorServices
                };
            }
        }

        public override IEnumerable<KeyValuePair<int, Action<IApplicationBuilder>>> ConfigureActionsByPriorities
        {
            get
            {
                return new Dictionary<int, Action<IApplicationBuilder>>()
                {
                    [10000] = this.UseEditorBuilder,
                };
            }
        }

        #region MyRegion IServiceCollection
        /// <summary>
        /// 添加全局服务注册
        /// </summary>
        /// <param name="services"></param>
        public void AddEditorServices(IServiceCollection services)
        {
            services.AddUEditorService("config/config.json");
            //.Add("test", context =>
            //{
            //    context.Response.WriteAsync("from test action");
            //})
            //.Add("test2", context =>
            //{
            //    context.Response.WriteAsync("from test2 action");
            //});
            //以上代码扩展了名字为test和test2两个action，作为示例仅仅返回了字符串。在扩展时可以读取Config配置，并使用已有的Handler。
        }
        #endregion

        #region MyRegion IApplicationBuilder
        public void UseEditorBuilder(IApplicationBuilder applicationBuilder)
        {

        }
        #endregion


    }
}