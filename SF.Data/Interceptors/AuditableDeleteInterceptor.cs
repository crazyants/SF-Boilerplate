using Microsoft.EntityFrameworkCore.ChangeTracking;
using SF.Core.Abstraction.Resolvers;
using SF.Entitys.Abstraction;
using System;



namespace SF.Core.Interceptors
{
    /// <summary>
    /// 对象编辑拦截器，拦截对象BaseEntity，处理创建日期、创建人、更新日期、更新人
    /// </summary>
    public class AuditableDeleteInterceptor : ChangeInterceptor<IHaveDeletedMeta>
    {
        private readonly IUserNameResolver _userNameResolver;
      
        public AuditableDeleteInterceptor(IUserNameResolver userNameResolver)
        {
            _userNameResolver = userNameResolver;
        }
        /// <summary>
        /// 新增前
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="item"></param>
        public override void OnBeforeInsert(EntityEntry entry, IHaveDeletedMeta item)
        {
            base.OnBeforeInsert(entry, item);

        
            
        }
        /// <summary>
        /// 更新前
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="item"></param>
        public override void OnBeforeUpdate(EntityEntry entry, IHaveDeletedMeta item)
        {
            base.OnBeforeUpdate(entry, item);
            var currentTime = DateTimeOffset.Now;
            var currentUser = _userNameResolver.GetCurrentUserName();
            item.DeletedOn = item.DeletedOn == default(DateTimeOffset) ? currentTime : item.DeletedOn;
            item.DeletedBy = item.DeletedBy ?? currentUser;
        }
       
    }
}
