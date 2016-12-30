using SF.Web.Control.JqGrid.Core.Request;
using SF.Web.Control.JqGrid.Infrastructure.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

namespace SF.Web.Control.JqGrid.Core.Json.Converters
{
    /// <summary>
    /// Converts JqGridRequestSearchingFilters from JSON. 
    /// </summary>
    internal sealed class JqGridRequestSearchingFiltersJsonConverter : JsonConverter
    {
        #region Fields
        private Type _jqGridRequestSearchingFiltersType = typeof(JqGridRequestSearchingFilters);
        #endregion

        #region Properties
        /// <summary>
        /// Gets a value indicating whether this JsonConverter can read JSON.
        /// </summary>
        public override bool CanRead
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether this JsonConverter can write JSON.
        /// </summary>
        public override bool CanWrite
        {
            get { return false; }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Determines whether this instance can convert the specified object type.
        /// </summary>
        /// <param name="objectType">Type of the object.</param>
        /// <returns>True if this instance can convert the specified object type, otherwise false.</returns>
        public override bool CanConvert(Type objectType)
        {
            return (objectType == _jqGridRequestSearchingFiltersType);
        }

        /// <summary>
        /// Reads the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The JsonReader to read from.</param>
        /// <param name="objectType">Type of the object.</param>
        /// <param name="existingValue">The existing value of object being read.</param>
        /// <param name="serializer">The calling serializer.</param>
        /// <returns>The object value.</returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);

            return ReadJqGridRequestSearchingFilters(jsonObject);
        }

        /// <summary>
        /// Writes the JSON representation of the object.
        /// </summary>
        /// <param name="writer">The JsonWriter to write to.</param>
        /// <param name="value">The value.</param>
        /// <param name="serializer">The calling serializer.</param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        private static JqGridRequestSearchingFilters ReadJqGridRequestSearchingFilters(JToken filtersToken)
        {
            JqGridRequestSearchingFilters jqGridRequestSearchingFilters = new JqGridRequestSearchingFilters();

            jqGridRequestSearchingFilters.GroupingOperator = ReadEnum<JqGridSearchGroupingOperators>(filtersToken, "groupOp", JqGridSearchGroupingOperators.And);
            if ((filtersToken["rules"] != null) && (filtersToken["rules"].Type == JTokenType.Array))
            {
                foreach(JToken filterToken in filtersToken["rules"].ToList())
                {
                    jqGridRequestSearchingFilters.Filters.Add(ReadJqGridRequestSearchingFilter(filterToken));
                }
            }

            if ((filtersToken["groups"] != null) && (filtersToken["groups"].Type == JTokenType.Array))
            {
                foreach (JToken groupToken in filtersToken["groups"].ToList())
                {
                    jqGridRequestSearchingFilters.Groups.Add(ReadJqGridRequestSearchingFilters(groupToken));
                }
            }

            return jqGridRequestSearchingFilters;
        }

        private static JqGridRequestSearchingFilter ReadJqGridRequestSearchingFilter(JToken jqGridRequestSearchingFilterToken)
        {
            JqGridRequestSearchingFilter jqGridRequestSearchingFilter = new JqGridRequestSearchingFilter();

            jqGridRequestSearchingFilter.SearchingName = ReadString(jqGridRequestSearchingFilterToken, "field");
            jqGridRequestSearchingFilter.SearchingOperator = ReadEnum(jqGridRequestSearchingFilterToken, "op", JqGridSearchOperators.Eq);
            jqGridRequestSearchingFilter.SearchingValue = ReadString(jqGridRequestSearchingFilterToken, "data");

            return jqGridRequestSearchingFilter;
        }

        private static TEnum ReadEnum<TEnum>(JToken valueToken, string key, TEnum defaultValue) where TEnum : struct
        {
            TEnum value = defaultValue;
            if ((valueToken[key] != null) && Enum.TryParse<TEnum>(valueToken[key].Value<String>(), true, out value))
            {
                return value;
            }
            else
            {
                return defaultValue;
            }
        }

        private static string ReadString(JToken valueToken, string key, string defaultValue = null)
        {
            if ((valueToken[key] != null) && (valueToken[key].Type == JTokenType.String))
            {
                return valueToken[key].Value<String>();
            }
            else
            {
                return defaultValue;
            }
        }
        #endregion
    }
}
