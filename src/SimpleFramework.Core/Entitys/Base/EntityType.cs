using SimpleFramework.Core.Abstraction.Entitys;

namespace SimpleFramework.Core.Entitys
{
    public class EntityType : BaseEntity
    {
        public string Name { get; set; }

        public string RoutingController { get; set; }

        public string RoutingAction { get; set; }
    }
}
