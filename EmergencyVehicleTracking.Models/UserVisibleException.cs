namespace EmergencyVehicleTracking.Models;

public class UserVisibleException : Exception
{
    private readonly string _code;
    private readonly string _message;
    
    public UserVisibleException(string code, string message) : base(message)
    {
        _code = code;
        _message = message;
    }
    
}