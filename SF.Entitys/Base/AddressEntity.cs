using System.Collections.Generic;
using SF.Entitys.Abstraction;

namespace SF.Entitys
{
    public class AddressEntity : BaseEntity
    {
        public string ContactName { get; set; }

        public string Phone { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public long DistrictId { get; set; }

        public virtual DistrictEntity District { get; set; }

        public long StateOrProvinceId { get; set; }

        public virtual StateOrProvinceEntity StateOrProvince { get; set; }

        public long CountryId { get; set; }

        public virtual CountryEntity Country { get; set; }

        public virtual IList<UserAddressEntity> UserAddresses { get; set; } = new List<UserAddressEntity>();
    }
}
