using Microsoft.Extensions.Caching.Memory;

namespace UzWorks.Infrastructure.Cache;

public interface IMemoryCacheService<T>
{
    T Get(string key);

    void Set(string key, T value, MemoryCacheEntryOptions? options = null);

    void Remove(string key);
}
