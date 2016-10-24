using SimpleFramework.Core.Abstraction.Entitys;

namespace SimpleFramework.Core.Entitys
{
    public class WidgetZoneEntity :AuditableEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
