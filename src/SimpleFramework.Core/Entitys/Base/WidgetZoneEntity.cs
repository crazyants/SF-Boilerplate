using SimpleFramework.Core.Abstraction.Entitys;

namespace SimpleFramework.Core.Entitys
{
    public class WidgetZoneEntity : EntityWithCreatedAndUpdatedMeta<long>
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
