using Microsoft.Extensions.Caching.Memory;

namespace EmergencyVehicleTracking.DataAccess.Patient;

public class InMemoryPatientRepository : BaseInMemoryRepository<DbPatient>, IPatientRepository
{
    public InMemoryPatientRepository(IMemoryCache memoryCache) : base(memoryCache)
    {
    }

    protected override List<DbPatient> GetMockData()
    {
        return new List<DbPatient>()
        {
            new() { Id = 1, Address = "Kidriceva 34", City = "Koper", Country = "Slovenija", FirstName = "Peter", LastName = "Novak" },
            new() { Id = 2, Address = "Dekani 232", City = "Dekani", Country = "Slovenija", FirstName = "Vladan", LastName = "Jovicic" }
        };
    }
}