namespace EmergencyVehicleTracking.DataAccess.Driver;

public interface IDriverRepository
{
    Task<List<DbDriver>> GetAllAsync();
    Task<DbDriver> InsertAsync(DbDriver driver);
}