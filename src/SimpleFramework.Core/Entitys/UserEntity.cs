using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SimpleFramework.Core.Abstraction.Entitys;
using SimpleFramework.Core.Common;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace SimpleFramework.Core.Entitys
{
    public class UserEntity : IdentityUser<long, IdentityUserClaim<long>, UserRoleEntity, IdentityUserLogin<long>>, IEntityWithTypedId<long>
    {
        public UserEntity()
        {
            CreatedOn = DateTimeOffset.Now;
            UpdatedOn = DateTimeOffset.Now;
        }

       public Guid UserGuid { get; set; }

        public string FullName { get; set; }

        public bool IsDeleted { get; set; }

        public DateTimeOffset CreatedOn { get; set; }

        public DateTimeOffset UpdatedOn { get; set; }

        public virtual IList<UserAddressEntity> UserAddresses { get; set; } = new List<UserAddressEntity>();

        public virtual UserAddressEntity CurrentShippingAddress { get; set; }

       // public long? CurrentShippingAddressId { get; set; }

        // public virtual IList<ApiAccountEntity> ApiAccounts { get; set; }
    }
}
