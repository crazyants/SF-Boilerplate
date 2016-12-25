using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SF.Core.Entitys.Abstraction;

using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace SF.Core.Entitys
{
    public class UserEntity : IdentityUser<long, IdentityUserClaim<long>, UserRoleEntity, IdentityUserLogin<long>>, IEntityWithTypedId<long>, IUserInfo, IHaveCreatedMeta<string>, IHaveUpdatedMeta<string>, IHaveDeletedMeta<string>
    {
        public UserEntity()
        {
            ApiAccounts = new List<ApiAccountEntity>();
            var currentTime = DateTimeOffset.Now;
            var currentUser = "Admin";
            CreatedOn = this.CreatedOn == default(DateTimeOffset) ? currentTime : this.CreatedOn;
            CreatedBy = this.CreatedBy ?? currentUser;
            UpdatedOn = this.UpdatedOn == default(DateTimeOffset) ? currentTime : this.CreatedOn;
            UpdatedBy = this.UpdatedBy ?? currentUser;
        }
        public long SiteId { get; set; }

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

        public string DisplayName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public bool DisplayInMemberList { get; set; }

        public bool Trusted { get; set; }

        public string WebSiteUrl { get; set; }

        public bool IsLockedOut { get; set; }

        public DateTime? LastLoginUtc { get; set; }

        public string TimeZoneId { get; set; }

        public bool AccountApproved { get; set; }

        public string AvatarUrl { get; set; }

        public string Gender { get; set; }
        #endregion
    }
}
