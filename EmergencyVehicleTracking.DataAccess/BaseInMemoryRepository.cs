using Microsoft.Extensions.Caching.Memory;

namespace EmergencyVehicleTracking.DataAccess;

public class BaseInMemoryRepository<T> where T : DbEntity
{
    private readonly IMemoryCache _memoryCache;
    private int _repositorySequence;

    protected BaseInMemoryRepository(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));

        // initialize cache with mock data
        var data = GetMockData();
        _repositorySequence = data.Count + 1;
        _memoryCache.Set(GetCacheKey(), data);
    }

    protected virtual List<T> GetMockData()
    {
        return new List<T>();
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

    public virtual Task<List<T>> GetAllAsync()
    {
        return Task.FromResult(GetAll());
    }

    public virtual Task<T> GetByIdAsync(long id)
    {
        var cacheData = GetAll();
        return Task.FromResult(cacheData.Single(i => i.Id == id));
    }

    public virtual Task<T> InsertAsync(T item)
    {
        if (Add(item))
        {
            return Task.FromResult(item);
        }

        throw new Exception("Could not add new entity to database.");
    }
}