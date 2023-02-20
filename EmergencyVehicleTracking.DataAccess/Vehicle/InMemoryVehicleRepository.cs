using Microsoft.Extensions.Caching.Memory;

namespace EmergencyVehicleTracking.DataAccess.Vehicle;

public class InMemoryVehicleRepository : BaseInMemoryRepository<DbVehicle>, IVehicleRepository
{
    public InMemoryVehicleRepository(IMemoryCache memoryCache) : base(memoryCache)
    {
    }

    protected override List<DbVehicle> GetMockData()
    {
        return new List<DbVehicle>()
        {
            new() { Id = 1, Name = "Vehicle 1", Type = 1, RegistrationNumber = "KP-VT-405", LocationX = 100, LocationY = 100},
            new() { Id = 2, Name = "Vehicle 2", Type = 2, RegistrationNumber = "KP-RG-069" }
        };
    }
}