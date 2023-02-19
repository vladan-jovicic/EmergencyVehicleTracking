using Microsoft.AspNetCore.Mvc;

namespace EmergencyVehicleTracking.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
public class PatientController : ControllerBase
{
    public PatientController()
    {
    }

    [HttpGet]
    public async Task<ActionResult<bool>> GetAll()
    {
        throw new NotImplementedException();
    }
}