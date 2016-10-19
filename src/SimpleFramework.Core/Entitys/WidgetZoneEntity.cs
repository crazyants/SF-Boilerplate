using SimpleFramework.Infrastructure.Entitys;

namespace SimpleFramework.Core.Entitys
{
    public class WidgetZoneEntity :AuditableEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
