using Microsoft.Extensions.Caching.Memory;

namespace EmergencyVehicleTracking.DataAccess;

public class BaseInMemoryRepository<T> where T : DbEntity
{
    private readonly IMemoryCache _memoryCache;
    private int _repositorySequence;

    protected BaseInMemoryRepository(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        
        // initialize cache with empty list
        _memoryCache.Set(GetCacheKey(), new List<T>());
    }

    private static string GetCacheKey()
    {
        return $"cache-{typeof(T).Name}";
    }


    protected List<T> GetAll()
    {
        var cacheKey = GetCacheKey();
        return !_memoryCache.TryGetValue(cacheKey, out List<T> data) ? new List<T>() : data;
    }

    protected bool Add(T item)
    {
        var cacheKey = GetCacheKey();
        if (!_memoryCache.TryGetValue(cacheKey, out List<T> data))
        {
            return false;
        }

        item.Id = GetRepositorySequence();
        data.Add(item);
        _memoryCache.Set(cacheKey, data);
        return true;
    }

    private int GetRepositorySequence()
    {
        _repositorySequence = _repositorySequence + 1;
        return _repositorySequence;
    }
}