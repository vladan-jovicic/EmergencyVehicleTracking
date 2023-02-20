using EmergencyVehicleTracking.Models;

namespace EmergencyVehicleTracking.Services.Authorization;

public interface IAuthorizationService
{
    Task<AuthorizedUser?> Authorize(string username, string password);
}