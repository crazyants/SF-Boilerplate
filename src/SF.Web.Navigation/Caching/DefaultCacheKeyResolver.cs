using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Web.Navigation.Caching
{
    public class DefaultCacheKeyResolver : ITreeCacheKeyResolver
    {
        public string GetCacheKey(INavigationTreeBuilder builder)
        {
            return builder.Name;
        }
    }
}
