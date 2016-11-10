using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SimpleFramework.Core.Abstraction.Entitys;
using SimpleFramework.Core.Common;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace SimpleFramework.Core.Entitys
{
    public class UserEntity : IdentityUser<long, IdentityUserClaim<long>, UserRoleEntity, IdentityUserLogin<long>>, IEntityWithTypedId<long>, IHaveCreatedMeta<string>, IHaveUpdatedMeta<string>
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

        public virtual IList<UserAddressEntity> UserAddresses { get; set; } = new List<UserAddressEntity>();

        public virtual UserAddressEntity CurrentShippingAddress { get; set; }

        public virtual IList<ApiAccountEntity> ApiAccounts { get; set; }

        private DateTimeOffset _createdOn;
        private DateTimeOffset _updatedOn;

        #region Implementation of IHaveCreatedMeta<TCreatedBy>

        /// <summary>
        /// The <see cref="DateTimeOffset"/> when it was created
        /// </summary>
        public virtual DateTimeOffset CreatedOn
        {
            get { return _createdOn; }
            set { _createdOn = value; }
        }

        /// <summary>
        /// The identifier (or entity) which first created this entity
        /// </summary>
        public virtual string CreatedBy { get; set; }

        #endregion

        #region Implementation of IHaveUpdatedMeta<TUpdatedBy>

        /// <summary>
        /// The <see cref="DateTimeOffset"/> when it was last updated
        /// </summary>
        public virtual DateTimeOffset UpdatedOn
        {
            get { return _updatedOn; }
            set { _updatedOn = value; }
        }

        /// <summary>
        /// The identifier (or entity) which last updated this entity
        /// </summary>
        public virtual string UpdatedBy { get; set; }

        #endregion
    }
}
