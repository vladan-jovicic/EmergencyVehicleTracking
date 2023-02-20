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
public class PatientRequestController : ControllerBase
{

    private readonly PatientRequestService _patientRequestService;

    public PatientRequestController(PatientRequestService patientRequestService)
    {
        _patientRequestService = patientRequestService ?? throw new ArgumentNullException(nameof(patientRequestService));
    }

    [HttpGet]
    public async Task<ActionResult<List<PatientRequestDto>>> GetAll()
    {
        return await _patientRequestService.GetAllAsync();
    }

    [HttpPost]
    public async Task<ActionResult<PatientRequestDto>> Post(PatientRequestDto request)
    {
        return await _patientRequestService.InsertAsync(request);
    }
}