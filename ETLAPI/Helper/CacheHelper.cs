using System;
using Microsoft.Extensions.Caching.Memory;
using ETLAPI.Interface;

namespace ETLAPI.Helper{
    public class CacheHelper:ICacheHelper{
        public MemoryCacheEntryOptions GetCacheEntryOptions(){
             return new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(30));
        }
    }
}