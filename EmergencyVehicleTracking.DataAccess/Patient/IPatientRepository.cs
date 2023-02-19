namespace EmergencyVehicleTracking.DataAccess.Patient;

public interface IPatientRepository
{
    Task<List<DbPatient>> GetAllAsync();
    Task<DbPatient> GetByIdAsync(long id);
    Task<DbPatient> InsertAsync(DbPatient patient);
}