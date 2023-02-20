using EmergencyVehicleTracking.Models;
using EmergencyVehicleTracking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmergencyVehicleTracking.Controllers.v1;

/// <summary>
/// Endpoint for managing drivers and driver actions
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[Authorize]
public class DriverController : ControllerBase
{
    private readonly DriverService _driverService;

    public DriverController(DriverService driverService)
    {
        _driverService = driverService ?? throw new ArgumentNullException(nameof(driverService));
    }
    
    /// <summary>
    /// Gets all drivers.
    /// </summary>
    /// <returns>A list of <see cref="DriverDto"/></returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<DriverDto>))]
    public async Task<ActionResult<List<DriverDto>>> GetAll()
    {
        return await _driverService.GetAllAsync();
    }

    /// <summary>
    /// Insert new driver.
    /// </summary>
    /// <param name="driver">A driver data to insert.</param>
    /// <returns>A newly inserted <see cref="DriverDto"/></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DriverDto))]
    public async Task<ActionResult<DriverDto>> Post(DriverDto driver)
    {
        return await _driverService.InsertAsync(driver);
    }

    /// <summary>
    /// An endpoint used to link driver with selected vehicle.
    /// </summary>
    /// <param name="driverId">ID of the driver.</param>
    /// <param name="vehicleId">ID of the vehicle</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpPost("{driverId:long}/vehicle/{vehicleId:long}")]
    public async Task<ActionResult<bool>> PairDriverAndVehicle(long driverId, long vehicleId)
    {
        throw new NotImplementedException();
    }
}