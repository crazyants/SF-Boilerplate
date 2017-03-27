namespace SF.Web.Control.JqGrid.Infrastructure.Enums
{
    /// <summary>
    /// jqGrid data types
    /// </summary>
    public enum JqGridDataTypes
    {
        /// <summary>
        /// XML data
        /// </summary>
        Xml,
        /// <summary>
        /// XML data as string
        /// </summary>
        XmlString,
        /// <summary>
        /// JSON data
        /// </summary>
        Json,
        /// <summary>
        /// JSONP data
        /// </summary>
        Jsonp,
        /// <summary>
        /// JSON data as string
        /// </summary>
        JsonString
    }

    /// <summary>
    /// Extensions for jqGrid data types
    /// </summary>
    public static class JqGridDataTypesExtensions
    {
        /// <summary>
        /// Return value indicating if data type is one indicating string of data usage.
        /// </summary>
        /// <param name="dataType">The data type</param>
        /// <returns>The value indicating if data type is one indicating string of data usage.</returns>
        public static bool IsDataStringDataType(this JqGridDataTypes dataType)
        {
            return (dataType == JqGridDataTypes.JsonString || dataType == JqGridDataTypes.XmlString);
        }
    }
}
