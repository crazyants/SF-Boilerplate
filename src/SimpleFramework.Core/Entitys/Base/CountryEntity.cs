using SimpleFramework.Core.Abstraction.Entitys;

namespace SimpleFramework.Core.Entitys
{
    public class CountryEntity : EntityWithCreatedAndUpdatedMeta<long>
    {
        public string Name { get; set; }
    }
}
