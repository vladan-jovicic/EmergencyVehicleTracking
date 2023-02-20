namespace EmergencyVehicleTracking.DataAccess.DriverVehicle;

public interface IDriverVehicleMapRepository
{
    Task<long?> GetDriverAssignedToVehicleAsync(long vehicleId);
    Task<long?> GetVehicleAssignedToDriverAsync(long driverId);
    Task<DbDriverVehicleMap> InsertAsync(DbDriverVehicleMap item);
}