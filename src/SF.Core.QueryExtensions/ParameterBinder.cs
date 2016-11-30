/*******************************************************************************
* 命名空间: SF.Core.QueryExtensions.Extensions
*
* 功 能： N/A
* 类 名： ParameterBinder
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/19 14:45:30 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SF.Core.QueryExtensions.Extensions
{
    /// <summary>
    /// Rebinds a parameter or a parameterized lambda expression.
    /// </summary>
    public class ParameterBinder : ExpressionVisitor
    {
        readonly ParameterExpression parameter;
        readonly Expression replacement;

        /// <summary>
        /// Creates a new parameter binder.
        /// </summary>
        /// <param name="parameter">The parameter to search for.</param>
        /// <param name="replacement">The expression to replace with.</param>
        public ParameterBinder(ParameterExpression parameter, Expression replacement)
        {
            if (parameter == null)
                throw new ArgumentNullException(nameof(parameter));
            if (replacement == null)
                throw new ArgumentNullException(nameof(replacement));

            this.parameter = parameter;
            this.replacement = replacement;
        }

        /// <inheritdoc />
        protected override Expression VisitParameter(ParameterExpression node)
        {
            if (node == parameter)
            {
                return replacement;
            }

            return base.VisitParameter(node);
        }

        /// <inheritdoc />
        protected override Expression VisitInvocation(InvocationExpression node)
        {
            if (node?.Expression == parameter)
            {
                var lambda = replacement as LambdaExpression;
                if (lambda != null)
                {
                    var binders = lambda.Parameters.Zip(node.Arguments,
                        (p, a) => new ParameterBinder(p, a));

                    return binders.Aggregate(lambda.Body, (e, b) => b.Visit(e));
                }
            }

            return base.VisitInvocation(node);
        }
    }
}
