using Microsoft.EntityFrameworkCore.ChangeTracking;
using SF.Core.Abstraction.Resolvers;
using SF.Entitys;
using SF.Entitys.Abstraction;
using System;



namespace SF.Core.Interceptors
{
    /// <summary>
    /// 对象编辑拦截器，IMustHaveSite，复制多租户网站ID
    /// </summary>
    public class AuditableSiteInterceptor : ChangeInterceptor<IMustHaveSite>
    {
        private readonly ISiteContext _siteContext;

        public AuditableSiteInterceptor(SiteContext siteContext)
        {
            _siteContext = siteContext;
        }
        /// <summary>
        /// 新增前
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="item"></param>
        public override void OnBeforeInsert(EntityEntry entry, IMustHaveSite item)
        {
            base.OnBeforeInsert(entry, item);

            item.SiteId = _siteContext.Id;
        }
      
    }
}
