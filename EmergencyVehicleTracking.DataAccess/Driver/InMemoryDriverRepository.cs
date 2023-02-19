using Microsoft.Extensions.Caching.Memory;

namespace EmergencyVehicleTracking.DataAccess.Driver;

public class InMemoryDriverRepository : BaseInMemoryRepository<DbDriver>, IDriverRepository
{
    public InMemoryDriverRepository(IMemoryCache memoryCache) : base(memoryCache)
    {
    }

    protected override List<DbDriver> GetMockData()
    {
        return new List<DbDriver>()
        {
            new() { Id = 1, FirstName = "Pinko", LastName = "Palinko", LocationX = 45, LocationY = 45, Perimeter = 45 },
            new() { Id = 2, FirstName = "John", LastName = "Doe", LocationX = 100, LocationY = 100, Perimeter = 50 }
        };
    }
}