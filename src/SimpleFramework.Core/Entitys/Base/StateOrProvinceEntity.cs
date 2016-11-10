using SimpleFramework.Core.Abstraction.Entitys;

namespace SimpleFramework.Core.Entitys
{
    public class StateOrProvinceEntity : EntityWithCreatedAndUpdatedMeta<long>
    {
        public long CountryId { get; set; }

        public virtual CountryEntity Country { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }
    }
}
