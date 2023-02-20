namespace EmergencyVehicleTracking.DataAccess.Requests;

public interface IPatientRequestRepository
{
    Task<List<DbPatientRequest>> GetAllAsync();
    Task<DbPatientRequest> InsertAsync(DbPatientRequest request);
}