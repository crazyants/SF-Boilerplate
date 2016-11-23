using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using SimpleFramework.Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleFramework.Module.Backend.ViewModels.Setting
{
    public class SettingViewModel
    {
        public string GroupName { get; set; }
        /// <summary>
        /// System name (ID) of the setting
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Current value for non-array setting
        /// </summary>
        public string Value { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public SettingValueType ValueType { get; set; }
        /// <summary>
        /// Predefined set of allowed values for this setting
        /// </summary>
        public string[] AllowedValues { get; set; }
        public string DefaultValue { get; set; }
        /// <summary>
        /// Defines whether the setting can have multiple values
        /// </summary>
        public bool IsArray { get; set; }
        /// <summary>
        /// Current values for array setting
        /// </summary>
        public string[] ArrayValues { get; set; }
        /// <summary>
        /// User-friendly name of the setting
        /// </summary>
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
