namespace EmergencyVehicleTracking.DataAccess.Patient;

public class InMemoryPatientRepository : IPatientRepository
{
    public Task<List<DbPatient>> GetAllPatientsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<DbPatient> GetByIdAsync()
    {
        throw new NotImplementedException();
    }
}