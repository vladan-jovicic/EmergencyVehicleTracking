using EmergencyVehicleTracking.Models;
using EmergencyVehicleTracking.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmergencyVehicleTracking.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
public class PatientController : ControllerBase
{
    private readonly PatientService _patientService;
    public PatientController(PatientService patientService)
    {
        _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
    }

    [HttpGet]
    public async Task<ActionResult<List<PatientDto>>> GetAll()
    {
        return await _patientService.GetAllPatientsAsync();
    }
}