using System;

namespace SimpleFramework.Core.Settings
{
    public class SettingEntry 
    {
        public long Id { get; set; }

        public string ObjectType { get; set; }

        public string GroupName { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public SettingValueType ValueType { get; set; }
        public string[] AllowedValues { get; set; }
        public string DefaultValue { get; set; }
        public bool IsArray { get; set; }
        public string[] ArrayValues { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public object Clone()
        {
            var retVal = new SettingEntry()
            {
                Id = Id,
                Name = Name,
                ObjectType = ObjectType,
                GroupName = GroupName,
                Value = Value,
                ValueType = ValueType,
                AllowedValues = AllowedValues,
                DefaultValue = DefaultValue,
                IsArray = IsArray,
                ArrayValues = ArrayValues,
                Title = Title,
                Description = Description
            };
            return retVal;
        }
    }
}
