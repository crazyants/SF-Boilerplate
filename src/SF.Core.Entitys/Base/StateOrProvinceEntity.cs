using SF.Core.Entitys.Abstraction;

namespace SF.Core.Entitys
{
    public class StateOrProvinceEntity : BaseEntity
    {
        public long CountryId { get; set; }

        public virtual CountryEntity Country { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }
    }
}
