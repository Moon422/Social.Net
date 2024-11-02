using Social.Net.Core.Caching;

namespace Social.Net.Services.Caching;

public interface ICacheService
{
    CacheKey PrepareCacheKey(CacheKey cacheKey, params object[] tokens);

    Task<T?> GetAsync<T>(CacheKey cacheKey, Func<Task<T>> dbCall);

    Task<IList<T>> GetAsync<T>(CacheKey cacheKey, Func<Task<IList<T>>> dbCall);

    void Remove(CacheKey cacheKey);

    void RemoveByPrefix(string prefix);

    void ClearCache();
}