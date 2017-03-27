using Microsoft.EntityFrameworkCore.ChangeTracking;
using SF.Core.Abstraction.Resolvers;
using SF.Entitys.Abstraction;
using System;



namespace SF.Core.Interceptors
{
    /// <summary>
    /// 对象编辑拦截器，拦截对象BaseEntity，处理创建日期、创建人、更新日期、更新人
    /// </summary>
    public class AuditableUpdateInterceptor : ChangeInterceptor<IHaveUpdatedMeta>
    {
        private readonly IUserNameResolver _userNameResolver;
       
        public AuditableUpdateInterceptor(IUserNameResolver userNameResolver)
        {
            _userNameResolver = userNameResolver;
        }
        /// <summary>
        /// 新增前
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="item"></param>
        public override void OnBeforeInsert(EntityEntry entry, IHaveUpdatedMeta item)
        {
            base.OnBeforeInsert(entry, item);

            var currentTime = DateTimeOffset.Now;
            var currentUser = _userNameResolver.GetCurrentUserName();
            item.UpdatedOn = item.UpdatedOn == default(DateTimeOffset) ? currentTime : item.UpdatedOn;
            item.UpdatedBy = item.UpdatedBy ?? currentUser;
        }
        /// <summary>
        /// 更新前
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="item"></param>
        public override void OnBeforeUpdate(EntityEntry entry, IHaveUpdatedMeta item)
        {
            base.OnBeforeUpdate(entry, item);
            var currentTime = DateTimeOffset.Now;
            item.UpdatedOn = currentTime;
            item.UpdatedBy = _userNameResolver.GetCurrentUserName();
        }
       
    }
}
