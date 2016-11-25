using Microsoft.EntityFrameworkCore.ChangeTracking;
using SF.Core.Abstraction.Entitys;
using SF.Core.Services;
using System;


namespace SF.Core.Interceptors
{
    public class UpdateAuditableInterceptor : ChangeInterceptor<IHaveUpdatedMeta>
    {
        private readonly IUserNameResolver _userNameResolver;

        public UpdateAuditableInterceptor(IUserNameResolver userNameResolver)
        {
            _userNameResolver = userNameResolver;
        }

        public override void OnBeforeInsert(EntityEntry entry, IHaveUpdatedMeta item)
        {
            base.OnBeforeInsert(entry, item);

            var currentTime = DateTime.UtcNow;
            var currentUser = GetCurrentUserName();    
            item.UpdatedOn = item.UpdatedOn;
            item.UpdatedBy = item.UpdatedBy ?? currentUser;
        }

        public override void OnBeforeUpdate(EntityEntry entry, IHaveUpdatedMeta item)
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
