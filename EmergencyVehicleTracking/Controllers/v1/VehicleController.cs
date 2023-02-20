using EmergencyVehicleTracking.DataAccess.User;
using EmergencyVehicleTracking.Models;
using EmergencyVehicleTracking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmergencyVehicleTracking.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[Authorize(Roles = ApplicationRole.ServerUser)]
public class VehicleController : ControllerBase
{
    private readonly VehicleService _vehicleService;
    private readonly ILogger<VehicleController> _logger;

    public VehicleController(VehicleService vehicleService, ILogger<VehicleController> logger)
    {
        _vehicleService = vehicleService ?? throw new ArgumentNullException(nameof(vehicleService));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    [HttpGet]
    public async Task<ActionResult<List<VehicleDto>>> GetAll()
    {
        return await _vehicleService.GetAllAsync();
    }

    [HttpPost]
    public async Task<ActionResult<VehicleDto>> Post(VehicleDto vehicle)
    {
        return await _vehicleService.InsertAsync(vehicle);
    }
}