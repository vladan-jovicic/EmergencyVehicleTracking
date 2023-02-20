using EmergencyVehicleTracking.DataAccess.User;
using EmergencyVehicleTracking.Models;
using EmergencyVehicleTracking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmergencyVehicleTracking.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[Authorize(Roles = ApplicationRole.DriverUser)]
public class DriverStateController : ControllerBase
{

    private readonly DriverService _driverService;

    public DriverStateController(DriverService driverService)
    {
        _driverService = driverService ?? throw new ArgumentNullException(nameof(driverService));
    }

    /// <summary>
    /// Gets authenticated user and retrieves driver's state
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<DriverStateDto>> GetDriver()
    {
        var username = HttpContext.User.Identity?.Name;
        if (username is null)
        {
            return Unauthorized();
        }
        var driverState = await _driverService.GetDriverState(username);
        return Ok(driverState);
    }

    [HttpGet("Vehicles")]
    public async Task<ActionResult<List<VehicleDto>>> GetDriverVehicles()
    {
        var username = HttpContext.User.Identity?.Name;
        if (username is null)
        {
            return Unauthorized();
        }

        var vehicles = await _driverService.GetAvailableVehicles(username);
        return Ok(vehicles);
    }

}