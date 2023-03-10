using EmergencyVehicleTracking.DataAccess.Driver;
using Microsoft.Extensions.Caching.Memory;

namespace EmergencyVehicleTracking.DataAccess.User;

public class InMemoryUserRepository : BaseInMemoryRepository<DbUser>, IUserRepository
{
    public InMemoryUserRepository(IMemoryCache memoryCache) : base(memoryCache)
    {
    }

    protected override List<DbUser> GetMockData()
    {
        return new List<DbUser>()
        {
            new() { Id = 1, Username = "admin", FirstName = "Super", LastName = "User", Roles = new[] { ApplicationRole.ServerUser }, Password = "SecurePassword" },
            new() { Id = 2, Username = "pinko.palinko", FirstName = "Pinko", LastName = "Palinko", Roles = new[] { ApplicationRole.DriverUser }, Password = "Test1234" },
            new() { Id = 3, Username = "john.doe", FirstName = "John", LastName = "Doe", Roles = new[] { ApplicationRole.DriverUser }, Password = "Test1234" }
        };
    }

    public async Task<DbUser?> GetByUsernameAsync(string username)
    {
        var allUsers = await GetAllAsync();
        return allUsers.FirstOrDefault(i => i.Username == username);
    }

    public override async Task<DbUser> InsertAsync(DbUser user)
    {
        // Do not allow two users with same username
        var allUsers = await GetAllAsync();
        if (allUsers.Any(i => i.Username == user.Username))
        {
            throw new ArgumentOutOfRangeException(nameof(user.Username), "User with such username already exists.");
        }

        return await base.InsertAsync(user);
    }
}