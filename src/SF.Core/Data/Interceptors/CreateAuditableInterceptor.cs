using Microsoft.EntityFrameworkCore.ChangeTracking;
using SF.Core.Abstraction.Entitys;
using SF.Core.Services;
using System;


namespace SF.Core.Interceptors
{
    public class CreateAuditableInterceptor : ChangeInterceptor<IHaveCreatedMeta>
    {
        private readonly IUserNameResolver _userNameResolver;

        public CreateAuditableInterceptor(IUserNameResolver userNameResolver)
        {
            _userNameResolver = userNameResolver;
        }

        public override void OnBeforeInsert(EntityEntry entry, IHaveCreatedMeta item)
        {
            base.OnBeforeInsert(entry, item);

            var currentTime = DateTime.UtcNow;
            var currentUser = GetCurrentUserName();
            item.CreatedOn = item.CreatedOn == default(DateTime) ? currentTime : item.CreatedOn;
            item.CreatedBy = item.CreatedBy ?? currentUser;
        }

        public override void OnBeforeUpdate(EntityEntry entry, IHaveCreatedMeta item)
        {
            base.OnBeforeUpdate(entry, item);
        }

        private string GetCurrentUserName()
        {
            var result = _userNameResolver != null ? _userNameResolver.GetCurrentUserName() : "unknown";
            return result;
        }
    }
}
