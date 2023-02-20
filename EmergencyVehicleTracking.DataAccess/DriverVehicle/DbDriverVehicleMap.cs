namespace EmergencyVehicleTracking.DataAccess.DriverVehicle;

public class DbDriverVehicleMap : DbEntity
{
    public long DriverId { get; set; }
    public long VehicleId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}