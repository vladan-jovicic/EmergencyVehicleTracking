namespace EmergencyVehicleTracking.DataAccess.Vehicle;

public class DbVehicle : DbEntity
{
    public string? Name { get; set; }
    public string? RegistrationNumber { get; set; }
    public int Type { get; set; }
}