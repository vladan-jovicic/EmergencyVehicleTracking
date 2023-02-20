namespace EmergencyVehicleTracking.Models;

public class AuthorizedUser
{
    public string Username { get; set; }
    public string DisplayName { get; set; }
    public string Token { get; set; }
    public string[] Roles { get; set; }
}