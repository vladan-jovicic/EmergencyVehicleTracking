namespace EmergencyVehicleTracking.Models;

public class DriverDto
{
    public long Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public Coordinates Location { get; set; }
    public float? Perimeter { get; set; }
}