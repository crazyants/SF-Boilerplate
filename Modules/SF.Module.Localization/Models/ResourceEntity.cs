using SF.Entitys.Abstraction;

namespace SF.Module.Localization.Models
{
    public class ResourceEntity : BaseEntity
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public CultureEntity Culture { get; set; }
    }
}
