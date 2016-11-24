/*******************************************************************************
* 命名空间: SF.Core.Common.RouteConventions
*
* 功 能： N/A
* 类 名： MvcOptionsExtensions
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/10 11:08:32 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using SF.Core.Common.RouteHelper.TypedRouting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Core.Common.RouteConventions
{
    public static class MvcOptionsExtensions
    {
        /// <summary>
        ///  配置全局路由前缀 
        /// </summary>
        /// <param name="opts"></param>
        /// <param name="routeAttribute"></param>
        public static void UseCentralRoutePrefix(this MvcOptions opts, IRouteTemplateProvider routeAttribute)
        {
            // 添加我们自定义 实现IApplicationModelConvention的RouteConvention
            opts.Conventions.Insert(0, new RouteConvention(routeAttribute));
        }


        public static TypedRoute Get(this MvcOptions opts, string template, Action<TypedRoute> configSetup)
        {
            return AddRoute(template, configSetup).ForHttpMethods("GET");
        }

        public static TypedRoute Post(this MvcOptions opts, string template, Action<TypedRoute> configSetup)
        {
            return AddRoute(template, configSetup).ForHttpMethods("POST");
        }

        public static TypedRoute Put(this MvcOptions opts, string template, Action<TypedRoute> configSetup)
        {
            return AddRoute(template, configSetup).ForHttpMethods("PUT");
        }

        public static TypedRoute Delete(this MvcOptions opts, string template, Action<TypedRoute> configSetup)
        {
            return AddRoute(template, configSetup).ForHttpMethods("DELETE");
        }

        public static TypedRoute Route(this MvcOptions opts, string template, Action<TypedRoute> configSetup)
        {
            return AddRoute(template, configSetup);
        }

        private static TypedRoute AddRoute(string template, Action<TypedRoute> configSetup)
        {
            var route = new TypedRoute(template);
            configSetup(route);

            if (TypedRoutingApplicationModelConvention.Routes.ContainsKey(route.ControllerType))
            {
                var controllerActions = TypedRoutingApplicationModelConvention.Routes[route.ControllerType];
                controllerActions.Add(route);
            }
            else
            {
                var controllerActions = new List<TypedRoute> { route };
                TypedRoutingApplicationModelConvention.Routes.Add(route.ControllerType, controllerActions);
            }

            return route;
        }

        public static void EnableTypedRouting(this MvcOptions opts)
        {
            opts.Conventions.Add(new TypedRoutingApplicationModelConvention());
        }
    }
}
