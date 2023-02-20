namespace EmergencyVehicleTracking.DataAccess.User;

public interface IUserRepository
{
    Task<List<DbUser>> GetAllAsync();
    Task<DbUser> InsertAsync(DbUser user);
    Task<DbUser?> GetByUsernameAsync(string username);
}