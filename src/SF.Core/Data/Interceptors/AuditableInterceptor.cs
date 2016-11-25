using Microsoft.EntityFrameworkCore.ChangeTracking;
using SF.Core.Abstraction.Entitys;
using SF.Core.Services;
using System;



namespace SF.Core.Interceptors
{
    public class AuditableInterceptor : ChangeInterceptor<BaseEntity>
    {
        private readonly IUserNameResolver _userNameResolver;

        public AuditableInterceptor(IUserNameResolver userNameResolver)
        {
            _userNameResolver = userNameResolver;
        }

        public override void OnBeforeInsert(EntityEntry entry, BaseEntity item)
        {
            base.OnBeforeInsert(entry, item);

            var currentTime = DateTimeOffset.Now;
            var currentUser = GetCurrentUserName();
            item.CreatedOn = item.CreatedOn == default(DateTimeOffset) ? currentTime : item.CreatedOn;
            item.CreatedBy = item.CreatedBy ?? currentUser;
            item.UpdatedOn = item.UpdatedOn == default(DateTimeOffset) ? currentTime : item.CreatedOn;
            item.UpdatedBy = item.UpdatedBy ?? currentUser;
        }

        public override void OnBeforeUpdate(EntityEntry entry, BaseEntity item)
        {
            base.OnBeforeUpdate(entry, item);
            var currentTime = DateTimeOffset.Now;
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
