using EmergencyVehicleTracking.Models;
using EmergencyVehicleTracking.Services.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmergencyVehicleTracking.Controllers.v1;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
public class AuthorizeController : ControllerBase
{

    private readonly IAuthorizationService _authorizationService;

    public AuthorizeController(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
    }
    
    [HttpPost]
    public async Task<ActionResult<AuthorizedUser>> AuthenticateAndAuthorize([FromBody] AuthorizationRequest request)
    {
        var user = await _authorizationService.Authorize(request.Username, request.Password);
        if (user is null)
        {
            return Unauthorized();
        }

        return Ok(user);
    }
}