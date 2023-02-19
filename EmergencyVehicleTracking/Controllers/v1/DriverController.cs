using EmergencyVehicleTracking.Models;
using EmergencyVehicleTracking.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmergencyVehicleTracking.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
public class DriverController : ControllerBase
{
    private readonly DriverService _driverService;

    public DriverController(DriverService driverService)
    {
        _driverService = driverService ?? throw new ArgumentNullException(nameof(driverService));
    }
    
    [HttpGet]
    public async Task<ActionResult<List<DriverDto>>> GetAll()
    {
        return await _driverService.GetAllAsync();
    }

    [HttpPost]
    public async Task<ActionResult<DriverDto>> Post(DriverDto driver)
    {
        return await _driverService.InsertAsync(driver);
    }
}