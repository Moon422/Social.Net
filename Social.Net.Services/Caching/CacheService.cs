using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using Social.Net.Core.Attributes.DependencyRegistrars;
using Social.Net.Core.Caching;

namespace Social.Net.Services.Caching;

[ScopedDependency(typeof(ICacheService))]
public class CacheService(IMemoryCache memoryCache) : ICacheService
{
    private readonly Dictionary<string, CancellationTokenSource> _cachePrefixCancellationTokens = new();

    private void SetCacheData<T>(CacheKey cacheKey, T value)
    {
        if (!_cachePrefixCancellationTokens.TryGetValue(cacheKey.Prefix, out var cts))
        {
            cts = new CancellationTokenSource();
            _cachePrefixCancellationTokens[cacheKey.Prefix] = cts;
        }

        var cacheEntryOption = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(10))
            .SetAbsoluteExpiration(TimeSpan.FromHours(6))
            .AddExpirationToken(new CancellationChangeToken(cts.Token));

        memoryCache.Set(cacheKey.Key, value, cacheEntryOption);
    }
    
    public CacheKey PrepareCacheKey(CacheKey cacheKey, params object[] tokens)
    {
        var key = string.Format(cacheKey.Key, tokens);
        return new CacheKey(key, cacheKey.Prefix);
    }

    public async Task<T?> GetAsync<T>(CacheKey cacheKey, Func<Task<T>> dbCall)
    {
        if (memoryCache.TryGetValue(cacheKey.Key, out T? data))
        {
            return data;
        }

        data = await dbCall();
        SetCacheData(cacheKey, data);

        return data;
    }

    public async Task<IList<T>> GetAsync<T>(CacheKey cacheKey, Func<Task<IList<T>>> dbCall)
    {
        if (memoryCache.TryGetValue(cacheKey.Key, out IList<T>? value))
        {
            return value ?? new List<T>();
        }

        var data = await dbCall();
        SetCacheData(cacheKey, data);

        return data;
    }

    public void Remove(CacheKey cacheKey)
    {
        memoryCache.Remove(cacheKey.Key);
    }

    public void RemoveByPrefix(string prefix)
    {
        if (!_cachePrefixCancellationTokens.TryGetValue(prefix, out var cts))
        {
            return;
        }
        
        cts.Cancel();
        _cachePrefixCancellationTokens.Remove(prefix);
    }

    public void ClearCache()
    {
        foreach (var cts in _cachePrefixCancellationTokens.Values)
        {
            cts.Cancel();
        }

        _cachePrefixCancellationTokens.Clear();
    }
}