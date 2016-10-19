using SimpleFramework.Infrastructure.Entitys;

namespace SimpleFramework.Core.Entitys
{
    public class EntityType :AuditableEntity
    {
        public string Name { get; set; }

        public string RoutingController { get; set; }

        public string RoutingAction { get; set; }
    }
}
