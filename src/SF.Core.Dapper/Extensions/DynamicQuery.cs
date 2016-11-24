using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace SimpleFramework.Core.Dapper.Extensions
{
    /// <summary>
    /// Dynamic query class.
    /// </summary>
    public static class DynamicQuery
    {
        /// <summary>
        /// Gets the insert query
        /// </summary>
        /// <param name="tableName">tableName parameter which needs to specify where to insert</param>
        /// <param name="item">item parameter which needs to be inserted</param>
        /// <returns>inserted item</returns>
        public static string GetInsertQuery(string tableName, dynamic item)
        {
            item = RemoveDefaultDateTimeValues(item);

            PropertyInfo[] props = item.GetType().GetProperties();

            string[] columns = props.GetValidTableColumns().Select(p => p.Name).ToArray();

            return string.Format("INSERT INTO [{0}] ({1}) OUTPUT inserted.ID VALUES (@{2})", tableName, string.Join(",", columns), string.Join(",@", columns));
        }

        /// <summary>
        /// Gets the update query
        /// </summary>
        /// <param name="tableName">tableName parameter which needs to specify where to update</param>
        /// <param name="item">item parameter which needs to be updated</param>
        /// <returns>updated items</returns>
        public static string GetUpdateQuery(string tableName, dynamic item)
        {
            item = RemoveDefaultDateTimeValues(item);
            PropertyInfo[] props = item.GetType().GetProperties();
            string[] columns = props.GetValidTableColumns().Select(p => p.Name).ToArray();
            ////string[] columns = props.Select(p => p.Name).ToArray();

            var parameters = columns.Select(name => name + "=@" + name).ToList();

            return string.Format("UPDATE [{0}] SET {1} WHERE ID=@ID", tableName, string.Join(",", parameters));
        }

        /// <summary>
        /// Removes the default DateTime value and update to null.
        /// </summary>
        /// <param name="item">item parameter which needs to change its default DateTime value</param>
        /// <returns>changed DateTime value</returns>
        public static dynamic RemoveDefaultDateTimeValues(dynamic item)
        {
            foreach (PropertyInfo propertyInfo in item.GetType().GetProperties())
            {
                if (propertyInfo.PropertyType == typeof(DateTime))
                {
                    DateTime date = (DateTime)propertyInfo.GetValue(item, null);
                    if (date.Equals(DateTime.MinValue))
                    {
                        propertyInfo.SetValue(item, DateTime.Now, null);
                    }
                }
            }

            return item;
        }

        /// <summary>
        /// Gets the dynamic query.
        /// </summary>
        /// <typeparam name="T">type of value to be process the query</typeparam>
        /// <param name="tableName">tableName parameter which needs to where to get the values</param>
        /// <param name="expression">expression parameter</param>
        /// <param name="countQuery">count query parameter</param>
        /// <returns>List of query objects</returns>
        public static QueryResult GetDynamicQuery<T>(string tableName, Expression<Func<T, bool>> expression, bool countQuery = false)
        {
            var queryProperties = new List<QueryParameter>();
            IDictionary<string, object> expando = new ExpandoObject();
            var builder = new StringBuilder();

            if (expression != null)
            {
                var body = (BinaryExpression)expression.Body;

                // walk the tree and build up a list of query parameter objects
                // from the left and right branches of the expression tree
                WalkTree(body, ExpressionType.Default, ref queryProperties);
            }

            // convert the query parms into a SQL string and dynamic property object
            builder.Append(string.Format("SELECT {0} FROM ", countQuery ? "COUNT(Id)" : tableName.GetValidSelectColumns<T>()));
            builder.Append(string.Concat("dbo.[", tableName, "] (NOLOCK) "));
            builder.Append(" WHERE ");

            if (queryProperties.Count == 0)
            {
                builder.Append("1 = 1");
            }

            for (int i = 0; i < queryProperties.Count(); i++)
            {
                QueryParameter item = queryProperties[i];

                if (!string.IsNullOrEmpty(item.LinkingOperator) && i > 0)
                {
                    builder.Append(string.Format("{0} [{1}] {2} @{1} ", item.LinkingOperator, item.PropertyName, item.QueryOperator));
                }
                else
                {
                    builder.Append(string.Format("[{0}] {1} @{0} ", item.PropertyName, item.QueryOperator));
                }

                expando[item.PropertyName] = item.PropertyValue;
            }

            return new QueryResult(builder.ToString().TrimEnd(), expando);
        }

        /// <summary>
        /// Gets the dynamic query.
        /// </summary>
        /// <typeparam name="T">type of value to be process the query</typeparam>
        /// <param name="tableName">tableName parameter which needs to where to get the values</param>
        /// <param name="expression">expression parameter</param>
        /// <param name="sortExpressions">sort expression parameter</param>
        /// <param name="pageIndex">represents page index</param>
        /// <param name="pageSize">represents page size</param>
        /// <returns>List of query objects</returns>
        public static QueryResult GetDynamicPagedQuery<T>(
                                                    string tableName,
                                                    Expression<Func<T, bool>> expression,
                                                    SortExpression<T>[] sortExpressions,
                                                    IEnumerable<string> searchColumns,
                                                    string valueToSearch,
                                                    int pageIndex = 1,
                                                    int pageSize = 10,
                                                    bool paging = true,
                                                    bool countQuery = false)
        {
            var query = GetDynamicQuery<T>(tableName, expression, countQuery);
            var builder = new StringBuilder();
            if (query != null)
            {
                var start = pageIndex + 1;
                var finish = pageIndex + pageSize;
                var orderByQuery = OrderByQuery<T>(sortExpressions);
                var searchQuery = string.IsNullOrEmpty(valueToSearch) ? string.Empty : "(" + string.Join(" LIKE '%" + valueToSearch + "%' OR ", searchColumns) + " LIKE '%" + valueToSearch + "%') ";
                builder.Append(query.Sql);
                builder.Append(string.Concat(searchQuery.Length > 0 ? " AND " : string.Empty, searchQuery));
                builder.Append(countQuery ? string.Empty : orderByQuery);

                // if paging is false, then it will return entire result.
                if (paging && !countQuery)
                {
                    builder.Append(" OFFSET " + pageIndex + " ROWS FETCH NEXT " + pageSize + " ROWS ONLY OPTION(RECOMPILE);");
                }
            }

            return new QueryResult(builder.ToString(), query.Param);
        }

        /// <summary>
        /// Gets the Order by query based on the sort expression parameter.
        /// </summary>
        /// <typeparam name="T">Represents entity Type</typeparam>
        /// <param name="sortExpressions">Represents Sort expression array</param>
        /// <returns>Order by query as string.</returns>
        private static string OrderByQuery<T>(SortExpression<T>[] sortExpressions)
        {
            string orderedQuery = " ORDER BY [Modified]";
            if (sortExpressions != null)
            {
                orderedQuery = string.Join(" ", sortExpressions.Select(item => string.Concat(item.SortBy.GetMemberName(), item.SortDirection.Equals(SortDirection.Ascending) ? " ASC " : " DESC ")));
                orderedQuery = string.Concat(!string.IsNullOrEmpty(orderedQuery) ? " ORDER BY " : string.Empty, orderedQuery);
            }

            return orderedQuery;
        }

        /// <summary>
        /// Gets Member name on the sort expression
        /// </summary>
        /// <typeparam name="TEntity">Represents entity Type</typeparam>
        /// <param name="sortBy">Represents Sort direction</param>
        /// <returns>Member name string.</returns>
        private static string GetMemberName<TEntity>(this Expression<Func<TEntity, object>> sortBy)
        {
            string memberName = string.Empty;
            if (sortBy != null)
            {
                var expression = sortBy.Body is UnaryExpression ? ((UnaryExpression)sortBy.Body).Operand as MemberExpression : (MemberExpression)sortBy.Body;
                memberName = expression.Member.Name;
            }

            return memberName;
        }

        /// <summary>
        /// Walks the tree.
        /// </summary>
        /// <param name="body">body parameter</param>
        /// <param name="linkingType">linkingType parameter to spec</param>
        /// <param name="queryProperties">queryProperties parameter which is the list of QueryParameter reference</param>
        private static void WalkTree(BinaryExpression body, ExpressionType linkingType, ref List<QueryParameter> queryProperties)
        {
            if (body.NodeType != ExpressionType.AndAlso && body.NodeType != ExpressionType.OrElse)
            {
                string propertyName = GetPropertyName(body);
                dynamic propertyValue = Expression.Lambda(body.Right).Compile().DynamicInvoke();
                string opr = GetOperator(body.NodeType);
                string link = GetOperator(linkingType);

                queryProperties.Add(new QueryParameter(link, propertyName, propertyValue, opr));
            }
            else if (body.Right.NodeType == ExpressionType.Call)
            {
                string propertyName = string.Empty;
                string opr = string.Empty;
                string link = string.Empty;
                object propertyValue = null;

                var expression = (MethodCallExpression)body.Right;
                if (expression != null && expression.Arguments.FirstOrDefault() != null && expression.Arguments.FirstOrDefault() is MemberExpression)
                {
                    propertyName = (expression.Arguments[1] as MemberExpression).Member.Name;
                    propertyValue = Expression.Lambda(expression.Arguments.FirstOrDefault()).Compile().DynamicInvoke();
                    opr = expression.Method.Name.Equals("Contains") ? "IN" : string.Empty;
                    if (propertyValue != null)
                    {
                        queryProperties.Add(new QueryParameter(link, propertyName, propertyValue, opr));
                    }
                }

                WalkTree((BinaryExpression)body.Left, body.NodeType, ref queryProperties);
            }
            else
            {
                WalkTree((BinaryExpression)body.Left, body.NodeType, ref queryProperties);
                WalkTree((BinaryExpression)body.Right, body.NodeType, ref queryProperties);
            }
        }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        /// <param name="body">body parameter</param>
        /// <returns>name of the property</returns>
        private static string GetPropertyName(BinaryExpression body)
        {
            string propertyName = body.Left.ToString().Split(new char[] { '.' })[1];

            if (body.Left.NodeType == ExpressionType.Convert)
            {
                // hack to remove the trailing ) when convering.
                propertyName = propertyName.Replace(")", string.Empty);
            }

            return propertyName;
        }

        /// <summary>
        /// Gets the operator.
        /// </summary>
        /// <param name="type">type parameter which needs to get operator</param>
        /// <returns>the operator based on type parameter</returns>
        private static string GetOperator(ExpressionType type)
        {
            switch (type)
            {
                case ExpressionType.Equal:
                    return "=";
                case ExpressionType.NotEqual:
                    return "!=";
                case ExpressionType.LessThan:
                    return "<";
                case ExpressionType.GreaterThan:
                    return ">";
                case ExpressionType.AndAlso:
                case ExpressionType.And:
                    return "AND";
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    return "OR";
                case ExpressionType.GreaterThanOrEqual:
                    return ">=";
                case ExpressionType.Default:
                    return string.Empty;
                default:
                    throw new NotImplementedException();
            }
        }

        private static string ExtractPropertyValueList(this object collectionInput)
        {
            if (collectionInput != null && collectionInput is IEnumerable<long>)
            {
                var inputValue = collectionInput as IEnumerable<long>;
                return string.Join(",", inputValue.Distinct().Select(item => Convert.ToString(item))).TrimEnd(',');
            }

            return default(string);
        }
    }
}
