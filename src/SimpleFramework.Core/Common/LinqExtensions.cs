using SimpleFramework.Core.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;


namespace SimpleFramework.Core.Common
{
    /// <summary>
    /// Linq, List, Dictionary, etc extensions
    /// </summary>
    public static partial class ExtensionMethods
    {
        #region GenericCollection Extensions

        /// <summary>
        /// Concatonate the items into a Delimited string
        /// </summary>
        /// <example>
        /// FirstNamesList.AsDelimited(",") would be "Ted,Suzy,Noah"
        /// FirstNamesList.AsDelimited(", ", " and ") would be "Ted, Suzy and Noah"
        /// </example>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <param name="finalDelimiter">The final delimiter. Set this if the finalDelimiter should be a different delimiter</param>
        /// <returns></returns>
        public static string AsDelimited<T>(this List<T> items, string delimiter, string finalDelimiter = null)
        {
            return AsDelimited<T>(items, delimiter, finalDelimiter, false);
        }

        /// <summary>
        /// Concatonate the items into a Delimited string an optionally htmlencode the strings
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items">The items.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <param name="finalDelimiter">The final delimiter.</param>
        /// <param name="HtmlEncode">if set to <c>true</c> [HTML encode].</param>
        /// <returns></returns>
        public static string AsDelimited<T>(this List<T> items, string delimiter, string finalDelimiter, bool HtmlEncode)
        {

            List<string> strings = new List<string>();
            foreach (T item in items)
            {
                if (item != null)
                {
                    string itemString = item.ToString();
                    if (HtmlEncode)
                    {

                        itemString = System.Net.WebUtility.HtmlEncode(itemString);
                    }
                    strings.Add(itemString);
                }
            }

            if (finalDelimiter != null && strings.Count > 1)
            {
                return String.Join(delimiter, strings.Take(strings.Count - 1).ToArray()) + string.Format("{0}{1}", finalDelimiter, strings.Last());
            }
            else
            {
                return String.Join(delimiter, strings.ToArray());
            }
        }

        /// <summary>
        /// Converts a List&lt;string&gt; to List&lt;guid&gt; only returning items that could be converted to a guid.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static List<Guid> AsGuidList(this IEnumerable<string> items)
        {
            return items.Select(a => a.AsGuidOrNull()).Where(a => a.HasValue).Select(a => a.Value).ToList();
        }

        /// <summary>
        /// Converts a List&lt;string&gt; to List&lt;guid&gt; return a null for items that could not be converted to a guid
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static List<Guid?> AsGuidOrNullList(this IEnumerable<string> items)
        {
            return items.Select(a => a.AsGuidOrNull()).ToList();
        }

        /// <summary>
        /// Converts a List&lt;string&gt; to List&lt;int&gt; only returning items that could be converted to a int.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <returns></returns>
        public static List<int> AsIntegerList(this IEnumerable<string> items)
        {
            return items.Select(a => a.AsIntegerOrNull()).Where(a => a.HasValue).Select(a => a.Value).ToList();
        }

        /// <summary>
        /// Joins a dictionary of items.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="delimter">The delimter.</param>
        /// <returns></returns>
        public static string Join(this Dictionary<string, string> items, string delimter)
        {
            List<string> parms = new List<string>();
            foreach (var item in items)
                parms.Add(string.Join("=", new string[] { item.Key, item.Value }));
            return string.Join(delimter, parms.ToArray());
        }

        /// <summary>
        /// Recursively flattens the specified source.
        /// http://stackoverflow.com/questions/5422735/how-do-i-select-recursive-nested-entities-using-linq-to-entity/5423024#5423024
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="childSelector">The child selector.</param>
        /// <returns></returns>
        public static IEnumerable<T> Flatten<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> childSelector)
        {
            // Flatten all the items without recursion or potential memory overflow
            var itemStack = new Stack<T>(source);
            while (itemStack.Count > 0)
            {
                T item = itemStack.Pop();
                yield return item;

                foreach (T child in childSelector(item))
                {
                    itemStack.Push(child);
                }
            }
        }

        /// <summary>
        /// Takes the last n items from a List.
        /// http://stackoverflow.com/questions/3453274/using-linq-to-get-the-last-n-elements-of-a-collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="N">The n.</param>
        /// <returns></returns>
        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> source, int N)
        {
            return source.Skip(Math.Max(0, source.Count() - N));
        }

        /// <summary>
        /// 组合And
        /// </summary>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.AndAlso);
        }
        /// <summary>
        /// 组合Or
        /// </summary>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.OrElse);
        }

        /// <summary>
        /// Combines the first expression with the second using the specified merge function.
        /// </summary>
        static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            var map = first.Parameters
                .Select((f, i) => new { f, s = second.Parameters[i] })
                .ToDictionary(p => p.s, p => p.f);
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        /// <summary>
        /// ParameterRebinder
        /// </summary>
        private class ParameterRebinder : ExpressionVisitor
        {
            /// <summary>
            /// The ParameterExpression map
            /// </summary>
            readonly Dictionary<ParameterExpression, ParameterExpression> map;
            /// <summary>
            /// Initializes a new instance of the <see cref="ParameterRebinder"/> class.
            /// </summary>
            /// <param name="map">The map.</param>
            ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
            {
                this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
            }
            /// <summary>
            /// Replaces the parameters.
            /// </summary>
            /// <param name="map">The map.</param>
            /// <param name="exp">The exp.</param>
            /// <returns>Expression</returns>
            public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
            {
                return new ParameterRebinder(map).Visit(exp);
            }
            /// <summary>
            /// Visits the parameter.
            /// </summary>
            /// <param name="p">The p.</param>
            /// <returns>Expression</returns>
            protected override Expression VisitParameter(ParameterExpression p)
            {
                ParameterExpression replacement;

                if (map.TryGetValue(p, out replacement))
                {
                    p = replacement;
                }
                return base.VisitParameter(p);
            }
        }

        #endregion GenericCollection Extensions

        #region Tree
        /// <summary>
        /// 树形查询条件
        /// </summary>
        /// <param name="entityList">数据源</param>
        /// <param name="condition">查询条件</param>
        /// <param name="primaryKey">主键</param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public static List<T> TreeWhere<T>(this List<T> entityList, Predicate<T> condition, string primaryKey="Id", string parentId = "ParentId") where T : class
        {
            List<T> locateList = entityList.FindAll(condition);
            var parameter = Expression.Parameter(typeof(T), "t");
            //模糊查询表达式
            List<T> treeList = new List<T>();
            foreach (T entity in locateList)
            {
                //先把自己加入进来
                treeList.Add(entity);
                //向上查询
                string pId = entity.GetType().GetProperty(parentId).GetValue(entity, null).ToString();
                while (true)
                {
                    if (string.IsNullOrEmpty(pId) && pId == "0")
                    {
                        break;
                    }
                  
                   // Predicate<T> upLambda = (Expression.Equal(parameter.Property(primaryKey), Expression.Constant(pId))).ToLambda<Predicate<T>>(parameter).Compile();
                    T upRecord = entityList.AsQueryable().Where(primaryKey, DynamicCompare.Equal, pId).FirstOrDefault();
                    if (upRecord != null)
                    {
                        treeList.Add(upRecord);
                        pId = upRecord.GetType().GetProperty(parentId).GetValue(upRecord, null).ToString();
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return treeList.Distinct().ToList();
        }
       
        #endregion

    }
}
