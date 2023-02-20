using Microsoft.Extensions.Caching.Memory;

namespace EmergencyVehicleTracking.DataAccess.Requests;

public class InMemoryPatientRequestRepository : BaseInMemoryRepository<DbPatientRequest>, IPatientRequestRepository
{
    public InMemoryPatientRequestRepository(IMemoryCache memoryCache) : base(memoryCache)
    {
    }

    protected override List<DbPatientRequest> GetMockData()
    {
        return new List<DbPatientRequest>()
        {
            new() { Id = 1, Status = 0, PatientId = 1, PickUpLocationX = 50, PickUpLocationY = 50, DropOffLocationX = 100, DropOffLocationY = 100 }
        };
    }
}