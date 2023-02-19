namespace EmergencyVehicleTracking.DataAccess.Patient;

public interface IPatientRepository
{
    Task<List<DbPatient>> GetAllPatientsAsync();
    Task<DbPatient> GetByIdAsync();
}