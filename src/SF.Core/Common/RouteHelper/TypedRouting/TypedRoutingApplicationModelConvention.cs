/*******************************************************************************
* 命名空间: SF.Core.Common.RouteHelper.TypedRouting
*
* 功 能： N/A
* 类 名： TypedRoutingApplicationModelConvention
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/10 11:34:44 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Internal;


namespace SF.Core.Common.RouteHelper.TypedRouting
{
    /// <summary>
    /// Lamda表达式的强类型Routing实现
    /// </summary>
    public class TypedRoutingApplicationModelConvention : IApplicationModelConvention
    {
        internal static readonly Dictionary<TypeInfo, List<TypedRoute>> Routes = new Dictionary<TypeInfo, List<TypedRoute>>();

        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                if (Routes.ContainsKey(controller.ControllerType))
                {
                    var typedRoutes = Routes[controller.ControllerType];
                    foreach (var route in typedRoutes)
                    {
                        var action = controller.Actions.FirstOrDefault(x => x.ActionMethod == route.ActionMember);

                        var selectorModel = new SelectorModel
                        {
                            AttributeRouteModel = route,
                        };

                        foreach (var constraint in route.Constraints)
                        {
                            selectorModel.ActionConstraints.Add(constraint);
                        }

                        action?.Selectors.Clear();
                        action?.Selectors.Insert(0, selectorModel);
                    }
                }
            }
        }
    }
}
