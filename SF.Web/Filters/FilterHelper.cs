/*******************************************************************************
* 命名空间: SF.Web.Filters
*
* 功 能： N/A
* 类 名： FilterHelper
*
* Ver 变更日期 负责人 变更内容
* ───────────────────────────────────
* V0.01 2016/11/24 10:54:56 疯狂蚂蚁 初版
*
* Copyright (c) 2016 SF 版权所有
* Description: SF快速开发平台
* Website：http://www.mayisite.com
*********************************************************************************/
using Newtonsoft.Json;
using SF.Core.Common;
using SF.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace SF.Web.Filters
{
    public static class FilterHelper
    {

        private const string _defaultType = "String";


        public static IQueryable ToFilter<TEntity>(this IQueryable query, List<FilterCriteria> criteria,string entityName)
        {
            var sql = new FilterSql();
            
            if (ToWhereClause(sql, criteria, c => !c.IsInactive && c.Entity.IsCaseInsensitiveEqual(entityName)))
            {
                //query 保证存在 entity 实体关联，否则 entity.name 不成立
                //WhereClause= " or entity.name =@{0}" EF 生成的SQL语句格式，非多表查询，可省略entity
                //Values="123"
                query = query.Where(sql.WhereClause.ToString(), sql.Values.ToArray());
            }

            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool ToWhereClause(FilterSql context)
        {
            if (context.Values == null)
                context.Values = new List<object>();
            else
                context.Values.Clear();

            if (context.WhereClause == null)
                context.WhereClause = new StringBuilder();
            else
                context.WhereClause.Clear();

            int index = 0;

            FilterParentheses(context.Criteria);

            foreach (var itm in context.Criteria)
            {
                if (context.WhereClause.Length > 0)
                    context.WhereClause.AppendFormat(" {0} ", itm.Or ? "Or" : "And");

                if (itm.Open.HasValue && itm.Open.Value)
                    context.WhereClause.Append("(");


                if (!itm.Value.HasValue())
                {
                    context.WhereClause.AppendFormat("ASCII({0}) Is Null", itm.SqlName);        // true if null or empty (string)
                }
                else
                {
                    context.WhereClause.Append(itm.SqlName);

                    if (itm.Operator == FilterOperator.Contains)
                        context.WhereClause.Append(".Contains(");
                    else if (itm.Operator == FilterOperator.StartsWith)
                        context.WhereClause.Append(".StartsWith(");
                    else if (itm.Operator == FilterOperator.EndsWith)
                        context.WhereClause.Append(".EndsWith(");
                    else
                        context.WhereClause.AppendFormat(" {0} ", itm.Operator == null ? "=" : itm.Operator.ToString());

                    context.WhereClause.Append(FormatParameterIndex(ref index));

                    if (itm.Operator == FilterOperator.Contains || itm.Operator == FilterOperator.StartsWith || itm.Operator == FilterOperator.EndsWith)
                        context.WhereClause.Append(")");

                    context.Values.Add(FilterValueToObject(itm.Value, itm.Type));
                }

                if (itm.Open.HasValue && !itm.Open.Value)
                    context.WhereClause.Append(")");
            }
            return (context.WhereClause.Length > 0);
        }

        public static bool ToWhereClause(FilterSql context, List<FilterCriteria> findIn, Predicate<FilterCriteria> match)
        {
            if (context.Criteria != null)
                context.Criteria.Clear();   // !

            context.Criteria = findIn.FindAll(match);

            return ToWhereClause(context);
        }

        /// <summary>
        /// 序列化过滤条件
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        public static List<FilterCriteria> Deserialize(string jsonData)
        {
            if (jsonData.HasValue())
            {
                if (jsonData.StartsWith("["))
                {
                    return JsonConvert.DeserializeObject<List<FilterCriteria>>(jsonData);
                }

                return new List<FilterCriteria> { JsonConvert.DeserializeObject<FilterCriteria>(jsonData) };
            }
            return new List<FilterCriteria>();
        }

        #region Utilities



        private static object FilterValueToObject(string value, string type)
        {
            if (value == null)
                value = "";

            //if (value == "__UtcNow__")
            //	return DateTime.UtcNow;

            //if (value == "__Now__")
            //	return DateTime.Now;

            //if (curlyBracketFormatting)
            //	return value.FormatWith("\"{0}\"", value.Replace("\"", "\"\""));

            Type t = Type.GetType("System.{0}".FormatInvariant(ValidateValue(type, _defaultType)));

            var result = TypeConverterFactory.GetConverter(t).ConvertFrom(CultureInfo.InvariantCulture, value);

            return result;
        }
        // helper
        private static string ValidateValue(string value, string alternativeValue)
        {
            if (value.HasValue() && !value.IsCaseInsensitiveEqual("null"))
                return value;

            return alternativeValue;
        }

        private static string FormatParameterIndex(ref int index)
        {
            //if (curlyBracketFormatting)
            //	return "{0}{1}{2}".FormatWith('{', index++, '}');

            return $"@{index++}";
        }

        private static void FilterParentheses(List<FilterCriteria> criteria)
        {
            // Logical or combine all criteria with same name.
            //
            // "The order of precedence for the logical operators is NOT (highest), followed by AND, followed by OR.
            // The order of evaluation at the same precedence level is from left to right."
            // http://www.databasedev.co.uk/sql-multiple-conditions.html

            if (criteria.Count > 0)
            {
                criteria.Sort();
                criteria.ForEach(c => { c.Open = null; c.Or = false; });

                var data = (
                    from c in criteria
                    group c by c.Entity).Where(g => g.Count() > 1);
                //group c by c.Name).Where(g => g.Count() > 1);

                foreach (var grp in data)
                {
                    grp.ToList().ForEach(f => f.Or = true);
                    grp.First().Or = false;
                    grp.First().Open = true;
                    grp.Last().Open = false;
                }
            }
        }
        #endregion

    }
}
