namespace EmergencyVehicleTracking.Services;

public class AuthenticatedUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string DisplayName => $"{FirstName} {LastName}";
    public string[] Roles { get; set; }
}