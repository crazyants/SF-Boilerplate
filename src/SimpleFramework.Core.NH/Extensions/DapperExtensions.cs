using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using Dapper;
using SimpleFramework.Core.Dapper.Attributes;

namespace SimpleFramework.Core.Dapper.Extensions
{
    public static class DapperExtensions
    {
        /// <summary>
        /// This method is used to construct the insert statement based on the entity type.
        /// </summary>
        /// <typeparam name="T">Represents entity Type</typeparam>
        /// <param name="cnn">Represents SQL connection object</param>
        /// <param name="tableName">Name of the table which insert statement performs</param>
        /// <param name="param">Represents Parameters collection</param>
        /// <returns>Primary key</returns>
        public static T Insert<T>(this DbConnection cnn, string tableName, dynamic param)
        {
            IEnumerable<T> result = SqlMapper.Query<T>(cnn, DynamicQuery.GetInsertQuery(tableName, param), param);
            return result.First();
        }

        /// <summary>
        /// This method is used to construct the update statement based on the entity type.
        /// </summary>
        /// <typeparam name="T">Represents entity Type</typeparam>
        /// <param name="cnn">Represents SQL connection object</param>
        /// <param name="tableName">Name of the table which update statement performs</param>
        /// <param name="param">Represents Parameters collection</param>
        public static void Update(this DbConnection cnn, string tableName, dynamic param)
        {
            SqlMapper.Execute(cnn, DynamicQuery.GetUpdateQuery(tableName, param), param);
        }

        /// <summary>
        /// This method returns the resultset from the Stored Pocedure.
        /// </summary>
        /// <typeparam name="T">Represents entity Type</typeparam>
        /// <param name="cnn">Represents SQL connection object</param>
        /// <param name="procedureName">Name of the stored procedure</param>
        /// <param name="param">Represents Parameters collection</param>
        public static T ExecStoredProcedure<T>(this DbConnection cnn, string procedureName, dynamic param)
        {
            IEnumerable<T> result = SqlMapper.Query<T>(cnn, procedureName, param, commandType: CommandType.StoredProcedure);
            return result.First();
        }

        /// <summary>
        /// This method will returns the valid columns names exclude of DBIgnore attribute decoreted columns.
        /// </summary>
        /// <param name="props">Represents Property collection of object</param>
        /// <returns>Valid colums property infos</returns>
        public static PropertyInfo[] GetValidTableColumns(this PropertyInfo[] props)
        {
            var validProps = props.Where(s => !s.Name.Equals("Id")
                            && s.CustomAttributes.Count(ca => ca.AttributeType.Equals(typeof(DBIgnoreAttribute))) == 0).ToArray();
            return validProps;
        }

        /// <summary>
        /// ReBuilding the entity object to ignore DBIgnore properties.
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="item">object which contains all the properties</param>
        /// <returns>Returns valid item by ignoring DBIgnore properties</returns>
        public static T ReBuildEntity<T>(this T item)
        {
            var result = Activator.CreateInstance<T>();
            if (item != null)
            {
                var validProperties = result.GetType().GetProperties().Where(s => s.CustomAttributes.Count(ca => ca.AttributeType.Equals(typeof(DBIgnoreAttribute))) == 0);
                foreach (var property in validProperties)
                {
                    var propertySource = item.GetType().GetProperty(property.Name);
                    if (propertySource != null && propertySource.CanRead)
                    {
                        property.SetValue(result, propertySource.GetValue(item));
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// This method will returns valid columns used for select statement.
        /// </summary>
        /// <typeparam name="T">Represents entity Type</typeparam>
        /// <param name="tableName">Name of the table which select statement performs</param>
        /// <returns>returns select statement string.</returns>
        public static string GetValidSelectColumns<T>(this string tableName)
        {
            var objectType = tableName.GetEntityType<T>();
            if (objectType != null)
            {
                var validProps = objectType.GetProperties().Where(s => s != null
                                && s.CustomAttributes.Count(ca => ca.AttributeType.Equals(typeof(DBIgnoreAttribute))) == 0).ToArray();

                if (validProps != null && validProps.Count() > 0)
                {
                    return string.Concat("[", string.Join("], [", validProps.Select(prop => prop.Name)), "]");
                }
            }

            // when it has no entity present in the DBO object.
            return " * ";
        }

        /// <summary>
        /// Constructs the slect stement query based on the conditions supplied.
        /// </summary>
        /// <typeparam name="T">Represents entity Type</typeparam>
        /// <param name="tableName">Name of the table which update statement performs</param>
        /// <param name="conditions">Represents where condition statement</param>
        /// <returns>returns select statement</returns>
        public static string ConstructInlineQuery<T>(this string tableName, string conditions = "")
        {
            var query = string.Concat("SELECT ", tableName.GetValidSelectColumns<T>(), " FROM dbo.[", tableName, "] (NOLOCK)");
            query = string.Concat(query, string.IsNullOrEmpty(conditions) ? string.Empty : " WHERE " + conditions);

            return query;
        }

        /// <summary>
        /// This method will get the property value from the object.
        /// </summary>
        /// <typeparam name="T">Represents entity Type</typeparam>
        /// <param name="target">Represents Object target</param>
        /// <param name="propertyName">Represents Property name which looking for.</param>
        /// <returns>returns the property value as object</returns>
        public static object GetPropertyValue<T>(this T target, string propertyName)
        {
            PropertyInfo propertyInfo = target.GetType().GetProperty(propertyName);
            if (propertyInfo != null)
            {
                return propertyInfo.GetValue(target);
            }

            return default(object);
        }

       
    }
}
