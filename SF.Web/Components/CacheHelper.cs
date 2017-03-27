 

using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Web.Components
{
    public class CacheHelper
    {
        public CacheHelper(
            IMemoryCache cache
            )
        {
            this.cache = cache;
        }

        private IMemoryCache cache;

        public void ClearCache(string cacheKey)
        {
            cache.Remove(cacheKey);
        }
    }
}
