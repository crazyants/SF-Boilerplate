using SimpleFramework.Core.Abstraction.Entitys;

namespace SimpleFramework.Module.Localization.Models
{
    public class Resource : BaseEntity
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public Culture Culture { get; set; }
    }
}
