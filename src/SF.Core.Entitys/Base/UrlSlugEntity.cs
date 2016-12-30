using SF.Core.Entitys.Abstraction;

namespace SF.Core.Entitys
{
    public class UrlSlugEntity : BaseEntity
    {
        public string Slug { get; set; }

        public long EntityId { get; set; }

        public long EntityTypeId { get; set; }

        public EntityType EntityType { get; set; }
    }
}
