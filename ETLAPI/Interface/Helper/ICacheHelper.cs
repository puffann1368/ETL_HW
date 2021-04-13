using Microsoft.Extensions.Caching.Memory;

namespace ETLAPI.Interface{
    public interface ICacheHelper{
        MemoryCacheEntryOptions GetCacheEntryOptions();
    }
}