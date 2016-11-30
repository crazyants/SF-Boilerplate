/*******************************************************************************
* 命名空间: SF.Core.QueryExtensions.Extensions
*
* 功 能： N/A
* 类 名： OrderBy_T_
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/1 15:39:09 疯狂蚂蚁 初版
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

namespace SF.Core.QueryExtensions.Extensions
{
    public class OrderBy<TEntity>
    {
        public OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> expression)
        {
            Expression = expression;
        }

        public OrderBy(string columName, bool reverse)
        {
            Expression = GetOrderBy(columName, reverse);
        }

        public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> Expression { get; private set; }


        private Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> GetOrderBy(string columnName, bool reverse)
        {
            Type typeQueryable = typeof(IQueryable<TEntity>);
            ParameterExpression argQueryable = System.Linq.Expressions.Expression.Parameter(typeQueryable, "p");
            var outerExpression = System.Linq.Expressions.Expression.Lambda(argQueryable, argQueryable);

            IQueryable<TEntity> query = new List<TEntity>().AsQueryable<TEntity>();
            var entityType = typeof(TEntity);
            ParameterExpression arg = System.Linq.Expressions.Expression.Parameter(entityType, "x");

            Expression expression = arg;
            string[] properties = columnName.Split('.');
            foreach (string propertyName in properties)
            {
                PropertyInfo propertyInfo = entityType.GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                expression = System.Linq.Expressions.Expression.Property(expression, propertyInfo);
                entityType = propertyInfo.PropertyType;
            }
            LambdaExpression lambda = System.Linq.Expressions.Expression.Lambda(expression, arg);
            string methodName = reverse ? "OrderByDescending" : "OrderBy";

            MethodCallExpression resultExp = System.Linq.Expressions.Expression.Call(typeof(Queryable),
                                                                                     methodName,
                                                                                     new Type[] { typeof(TEntity), entityType },
                                                                                     outerExpression.Body,
                                                                                     System.Linq.Expressions.Expression.Quote(lambda));

            var finalLambda = System.Linq.Expressions.Expression.Lambda(resultExp, argQueryable);

            return (Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>)finalLambda.Compile();
        }
    }
}
