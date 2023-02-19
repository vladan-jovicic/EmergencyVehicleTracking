namespace EmergencyVehicleTracking.DataAccess.Driver;

public class DbDriver : DbEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public float LocationX { get; set; }
    public float LocationY { get; set; }
    public float Perimeter { get; set; }
}