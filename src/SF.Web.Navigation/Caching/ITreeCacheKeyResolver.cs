using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Web.Navigation.Caching
{
    public interface ITreeCacheKeyResolver
    {
        string GetCacheKey(INavigationTreeBuilder builder);
    }
}
