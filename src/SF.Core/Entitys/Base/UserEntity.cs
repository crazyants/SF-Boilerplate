using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SF.Core.Abstraction.Entitys;
using SF.Core.Common;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace SF.Core.Entitys
{
    public class UserEntity : IdentityUser<long, IdentityUserClaim<long>, UserRoleEntity, IdentityUserLogin<long>>, IEntityWithTypedId<long>, IHaveCreatedMeta<string>, IHaveUpdatedMeta<string>, IHaveDeletedMeta<string>
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
        private DateTimeOffset? _deletedOn;
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

        #region Implementation of IHaveDeletedMeta<TCreatedBy>

        /// <summary>
        /// The <see cref="DateTimeOffset"/> when it was created
        /// </summary>
        public virtual DateTimeOffset? DeletedOn
        {
            get { return _deletedOn; }
            set { _deletedOn = value; }
        }

        /// <summary>
        /// The identifier (or entity) which first created this entity
        /// </summary>
        public virtual string DeletedBy { get; set; }

      
        #endregion
    }
}
