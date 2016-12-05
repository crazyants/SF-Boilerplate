using Microsoft.EntityFrameworkCore.ChangeTracking;
using SF.Core.Abstraction.Entitys;
using SF.Core.Services;
using System;



namespace SF.Core.Interceptors
{
    /// <summary>
    /// 对象编辑拦截器，拦截对象BaseEntity，处理创建日期、创建人、更新日期、更新人
    /// </summary>
    public class AuditableInterceptor : ChangeInterceptor<BaseEntity>
    {
        private readonly IUserNameResolver _userNameResolver;

        public AuditableInterceptor(IUserNameResolver userNameResolver)
        {
            _userNameResolver = userNameResolver;
        }
        /// <summary>
        /// 新增前
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="item"></param>
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
        /// <summary>
        /// 更新前
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="item"></param>
        public override void OnBeforeUpdate(EntityEntry entry, BaseEntity item)
        {
            base.OnBeforeUpdate(entry, item);
            var currentTime = DateTimeOffset.Now;
            item.UpdatedOn = currentTime;
            item.UpdatedBy = GetCurrentUserName();
        }
        /// <summary>
        /// 获取当前用户名
        /// </summary>
        /// <returns></returns>
        private string GetCurrentUserName()
        {
            var result = _userNameResolver != null ? _userNameResolver.GetCurrentUserName() : "unknown";
            return result;
        }
    }
}
