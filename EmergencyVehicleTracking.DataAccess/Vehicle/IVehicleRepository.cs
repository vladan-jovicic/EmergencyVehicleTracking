namespace EmergencyVehicleTracking.DataAccess.Vehicle;

public interface IVehicleRepository
{
    Task<List<DbVehicle>> GetAllAsync();
    Task<DbVehicle> GetByIdAsync(long id);
    Task<DbVehicle> InsertAsync(DbVehicle patient);
}