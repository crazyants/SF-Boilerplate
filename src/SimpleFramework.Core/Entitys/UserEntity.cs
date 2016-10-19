using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SimpleFramework.Infrastructure.Entitys;
using SimpleFramework.Infrastructure.Common;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace SimpleFramework.Core.Entitys
{
    public class UserEntity : IdentityUser<long, IdentityUserClaim<long>, UserRoleEntity, IdentityUserLogin<long>>, IEntityWithTypedId<long>, IAuditable
    {
        public UserEntity()
        {
            ApiAccounts = new NullCollection<ApiAccountEntity>();
        }

        public Guid UserGuid { get; set; }

        public string FullName { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsAdministrator { get; set; }

        [StringLength(128)]
        public string AccountState { get; set; }

        [StringLength(128)]
        public string UserType { get; set; }
        
        public long? CurrentShippingAddressId { get; set; }

        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset? ModifiedDate { get; set; }

        public virtual IList<UserAddressEntity> UserAddresses { get; set; } = new List<UserAddressEntity>();

        public virtual UserAddressEntity CurrentShippingAddress { get; set; }

        public virtual ObservableCollection<ApiAccountEntity> ApiAccounts { get; set; }
    }
}
