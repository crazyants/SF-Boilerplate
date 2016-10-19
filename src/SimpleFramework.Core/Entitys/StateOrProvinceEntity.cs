using SimpleFramework.Infrastructure.Entitys;

namespace SimpleFramework.Core.Entitys
{
    public class StateOrProvinceEntity :AuditableEntity
    {
        public long CountryId { get; set; }

        public virtual CountryEntity Country { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }
    }
}
