using Microsoft.Extensions.Caching.Memory;

namespace EmergencyVehicleTracking.DataAccess.DriverVehicle;

public class InMemoryDriverVehicleMapRepository : BaseInMemoryRepository<DbDriverVehicleMap>, IDriverVehicleMapRepository
{
    public InMemoryDriverVehicleMapRepository(IMemoryCache memoryCache) : base(memoryCache)
    {
    }

    public async Task<long?> GetDriverAssignedToVehicleAsync(long vehicleId)
    {
        var allAssignments = await GetAllAsync();
        return allAssignments.FirstOrDefault(i => i.VehicleId == vehicleId
                                                  && i.StartDate < DateTime.Now && DateTime.Now < i.EndDate)?.DriverId;
    }

    public async Task<long?> GetVehicleAssignedToDriverAsync(long driverId)
    {
        var allAssignments = await GetAllAsync();
        return allAssignments.FirstOrDefault(i => i.DriverId == driverId
                                                  && i.StartDate < DateTime.Now && DateTime.Now < i.EndDate)?.VehicleId;
    }

    public override async Task<DbDriverVehicleMap> InsertAsync(DbDriverVehicleMap item)
    {
        var allAssignments = await GetAllAsync();
        // check if there already exists assignment and throw exception
        if (allAssignments.Any(i => i.DriverId == item.DriverId || i.VehicleId == item.VehicleId))
        {
            throw new ArgumentException("A driver/vehicle can be assigned to a single vehicle/driver");
        }

        return await base.InsertAsync(item);
    }
}