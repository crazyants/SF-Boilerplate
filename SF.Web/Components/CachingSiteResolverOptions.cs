using System;

namespace SF.Web.Components
{
    public class CachingSiteResolverOptions
    {
        public TimeSpan FolderListCacheDuration { get; set; } = new TimeSpan(1, 0, 0); // 1 hour default
        public TimeSpan SiteCacheDuration { get; set; } = new TimeSpan(1, 0, 0); // 1 hour default
    }
}
