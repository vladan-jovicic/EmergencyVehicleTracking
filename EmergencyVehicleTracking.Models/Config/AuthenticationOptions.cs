namespace EmergencyVehicleTracking.Models.Config;

public class AuthenticationOptions
{
    public string JwtSecurityKey { get; set; }
    public int Expiration { get; set; } = 12;
}