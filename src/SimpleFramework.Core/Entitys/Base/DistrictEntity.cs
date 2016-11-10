using SimpleFramework.Core.Abstraction.Entitys;

namespace SimpleFramework.Core.Entitys
{
    public class DistrictEntity : EntityWithCreatedAndUpdatedMeta<long>
    {
        public long StateOrProvinceId { get; set; }

        public virtual StateOrProvinceEntity StateOrProvince { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public string Location { get; set; }
    }
}
