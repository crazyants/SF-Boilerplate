/*******************************************************************************
* 命名空间: SF.Core.Common.RouteHelper.TypedRouting
*
* 功 能： N/A
* 类 名： TypedRoute
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/10 11:35:29 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Internal;

namespace SF.Core.Common.RouteHelper.TypedRouting
{
    /// <summary>
    /// 定义一个强类型的路由模型
    /// </summary>
    public class TypedRoute : AttributeRouteModel
    {
        public TypedRoute(string template)
        {
            Template = template;
            Constraints = new List<IActionConstraintMetadata>();
        }

        public TypeInfo ControllerType { get; private set; }

        public MethodInfo ActionMember { get; private set; }

        public List<IActionConstraintMetadata> Constraints { get; private set; }

        public TypedRoute Controller<TController>()
        {
            ControllerType = typeof(TController).GetTypeInfo();
            return this;
        }

        public TypedRoute Action<T, U>(Expression<Func<T, U>> expression)
        {
            ActionMember = GetMethodInfoInternal(expression);
            ControllerType = ActionMember.DeclaringType.GetTypeInfo();
            return this;
        }

        public TypedRoute Action<T>(Expression<Action<T>> expression)
        {
            ActionMember = GetMethodInfoInternal(expression);
            ControllerType = ActionMember.DeclaringType.GetTypeInfo();
            return this;
        }

        private static MethodInfo GetMethodInfoInternal(dynamic expression)
        {
            var method = expression.Body as MethodCallExpression;
            if (method != null)
                return method.Method;

            throw new ArgumentException("Expression is incorrect!");
        }

        public TypedRoute WithName(string name)
        {
            Name = name;
            return this;
        }

        public TypedRoute ForHttpMethods(params string[] methods)
        {
            Constraints.Add(new HttpMethodActionConstraint(methods));
            return this;
        }

        public TypedRoute WithConstraints(params IActionConstraintMetadata[] constraints)
        {
            Constraints.AddRange(constraints);
            return this;
        }
    }
}
