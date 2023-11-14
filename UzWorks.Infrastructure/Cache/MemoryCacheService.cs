using Microsoft.Extensions.Caching.Memory;

namespace UzWorks.Infrastructure.Cache;

public class MemoryCacheService<T> : IMemoryCacheService<T>
{
    private readonly IMemoryCache _cache;

    public MemoryCacheService(IMemoryCache cache)
    {
        _cache = cache;
    }

    public T Get(string key)
    {
        _cache.TryGetValue(key, out T entry);
        return entry;
    }

    public void Remove(string key) => _cache.Remove(key);

    public void Set(string key, T value, MemoryCacheEntryOptions? options = null) => 
        _cache.Set(key, value, options);
}
