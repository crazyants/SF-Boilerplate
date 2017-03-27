using SF.Entitys.Abstraction;

namespace SF.Entitys
{
    public class EntityType : BaseEntity
    {
        public string Name { get; set; }

        public string RoutingController { get; set; }

        public string RoutingAction { get; set; }
    }
}
