using Microsoft.EntityFrameworkCore.ChangeTracking;
using SimpleFramework.Core.Abstraction.Entitys;
using System;


namespace SimpleFramework.Core.Interceptors
{
    public class AuditableInterceptor : ChangeInterceptor<EntityWithCreatedAndUpdatedMeta<long>>
    {
        private readonly IUserNameResolver _userNameResolver;

        public AuditableInterceptor(IUserNameResolver userNameResolver)
        {
            _userNameResolver = userNameResolver;
        }

        public override void OnBeforeInsert(EntityEntry entry, EntityWithCreatedAndUpdatedMeta<long> item)
        {
            base.OnBeforeInsert(entry, item);

            var currentTime = DateTime.UtcNow;
            var currentUser = GetCurrentUserName();
            item.CreatedOn = item.CreatedOn == default(DateTime) ? currentTime : item.CreatedOn;
            item.UpdatedOn = item.UpdatedOn == default(DateTime) ? currentTime : item.CreatedOn;
            item.CreatedBy = item.CreatedBy ?? currentUser;
            item.UpdatedBy = item.UpdatedBy ?? currentUser;
        }

        public override void OnBeforeUpdate(EntityEntry entry, EntityWithCreatedAndUpdatedMeta<long> item)
        {
            base.OnBeforeUpdate(entry, item);
            var currentTime = DateTime.UtcNow;
            item.UpdatedOn = currentTime;
            item.UpdatedBy = GetCurrentUserName();
        }

        private string GetCurrentUserName()
        {
            var result = _userNameResolver != null ? _userNameResolver.GetCurrentUserName() : "unknown";
            return result;
        }
    }
}
