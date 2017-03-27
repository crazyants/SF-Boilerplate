using Microsoft.EntityFrameworkCore.ChangeTracking;
using SF.Core.Abstraction.Resolvers;
using SF.Entitys.Abstraction;
using System;



namespace SF.Core.Interceptors
{
    /// <summary>
    /// 对象编辑拦截器，拦截对象BaseEntity，处理创建日期、创建人、更新日期、更新人
    /// </summary>
    public class AuditableCreateInterceptor : ChangeInterceptor<IHaveCreatedMeta>
    {
        
        private readonly IUserNameResolver _userNameResolver;

        public AuditableCreateInterceptor(IUserNameResolver userNameResolver)
        {
            _userNameResolver = userNameResolver;
        }
        /// <summary>
        /// 新增前
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="item"></param>
        public override void OnBeforeInsert(EntityEntry entry, IHaveCreatedMeta item)
        {
            base.OnBeforeInsert(entry, item);

            var currentTime = DateTimeOffset.Now;
            var currentUser = _userNameResolver.GetCurrentUserName();
            item.CreatedOn = item.CreatedOn == default(DateTimeOffset) ? currentTime : item.CreatedOn;
            item.CreatedBy = item.CreatedBy ?? currentUser;
       
        }
        /// <summary>
        /// 更新前
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="item"></param>
        public override void OnBeforeUpdate(EntityEntry entry, IHaveCreatedMeta item)
        {
            base.OnBeforeUpdate(entry, item);

        }
        
    }
}
