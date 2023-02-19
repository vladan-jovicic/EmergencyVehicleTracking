using Microsoft.Extensions.Caching.Memory;

namespace EmergencyVehicleTracking.DataAccess.Patient;

public class InMemoryPatientRepository : BaseInMemoryRepository<DbPatient>, IPatientRepository
{
    protected InMemoryPatientRepository(IMemoryCache memoryCache) : base(memoryCache)
    {
    }
    
    public Task<List<DbPatient>> GetAllPatientsAsync()
    {
        return Task.FromResult(GetAll());
    }

    public Task<DbPatient> GetByIdAsync(long patientId)
    {
        var cacheData = GetAll();
        return Task.FromResult(cacheData.Single(i => i.Id == patientId));
    }

    public Task<DbPatient> InsertAsync(DbPatient patient)
    {
        if (Add(patient))
        {
            return Task.FromResult(patient);
        }

        throw new Exception("Could not add new patient to database.");
    }
}