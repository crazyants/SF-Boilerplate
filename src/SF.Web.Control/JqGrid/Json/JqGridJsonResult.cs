using SF.Web.Control.JqGrid.Core.Json.Converters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SF.Web.Control.JqGrid.Core.Json
{
    /// <summary>
    /// Represents a class that is used to send object from SF.Web.Control.JqGrid.Core namespace as JSON-formatted content to the response, converted the way jqGrid expects it.
    /// </summary>
    public sealed class JqGridJsonResult : JsonResult
    {
        #region Constructor
        /// <summary>
        /// Initializes new instance of JqGridJsonResult class.
        /// </summary>
        /// <param name="value">The value to be send.</param>
        public JqGridJsonResult(object value)
            : base(value, GetJqGridJsonSerializerSettings())
        { }
        #endregion

        #region Methods
        private static JsonSerializerSettings GetJqGridJsonSerializerSettings()
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.Converters.Add(new JqGridResponseJsonConverter());

            return jsonSerializerSettings;
        }
        #endregion
    }
}
