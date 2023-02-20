using EmergencyVehicleTracking.DataAccess.User;
using EmergencyVehicleTracking.Models;
using EmergencyVehicleTracking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EmergencyVehicleTracking.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[Authorize(Roles = ApplicationRole.ServerUser)]
public class PatientController : ControllerBase
{
    private readonly PatientService _patientService;
    public PatientController(PatientService patientService)
    {
        _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PatientDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<PatientDto>>> GetAll()
    {
        return await _patientService.GetAllPatientsAsync();
    }
}