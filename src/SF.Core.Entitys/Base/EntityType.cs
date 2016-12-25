using SF.Core.Entitys.Abstraction;

namespace SF.Core.Entitys
{
    public class EntityType : BaseEntity
    {
        public string Name { get; set; }

        public string RoutingController { get; set; }

        public string RoutingAction { get; set; }
    }
}
