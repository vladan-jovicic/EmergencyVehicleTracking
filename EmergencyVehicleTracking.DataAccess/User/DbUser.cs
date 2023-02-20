namespace EmergencyVehicleTracking.DataAccess.User;

public class DbUser : DbEntity
{
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string[] Roles { get; set; }
}