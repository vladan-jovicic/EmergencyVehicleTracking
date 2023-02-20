
namespace EmergencyVehicleTracking.DataAccess.Requests;

public class DbPatientRequest : DbEntity
{
    public long PatientId { get; set; }
    public float PickUpLocationX { get; set; }
    public float PickUpLocationY { get; set; }
    public float DropOffLocationX { get; set; }
    public float DropOffLocationY { get; set; }
    public int Status { get; set; }
}