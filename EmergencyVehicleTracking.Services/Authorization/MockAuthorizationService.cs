using EmergencyVehicleTracking.DataAccess.User;
using EmergencyVehicleTracking.Models;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EmergencyVehicleTracking.Models.Config;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EmergencyVehicleTracking.Services.Authorization;

public class MockAuthorizationService : IAuthorizationService
{

    private readonly IUserRepository _userRepository;
    private readonly AuthenticationOptions _authenticationOptions;
    private readonly ILogger<MockAuthorizationService> _logger;
    
    public MockAuthorizationService(IOptions<AuthenticationOptions> authOptions, IUserRepository userRepository, ILogger<MockAuthorizationService> logger)
    {
        _authenticationOptions = authOptions.Value ?? throw new ArgumentNullException(nameof(authOptions));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    /// <summary>
    /// Authorize user.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns>Authorized user with token.</returns>
    public async Task<AuthorizedUser?> Authorize(string username, string password)
    {
        try
        {
            var authenticatedUser = await Authenticate(username, password);
            if (authenticatedUser is null)
            {
                _logger.LogInformation("Invalid login attempt for {user}.", username);
                return null;
            }

            var token = GetAuthroizationToken(username, authenticatedUser.DisplayName, authenticatedUser.Roles);

            return new AuthorizedUser()
            {
                Token = token,
                Username = username,
                DisplayName = authenticatedUser.DisplayName
            };

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occured while authorizing {user}.", username);
            return null;
        }
    }

    /// <summary>
    /// Method used for authentication.
    /// Instead of a simple username/password check, one can implement multiple authentication
    /// providers and delegate authentication to them.
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns><see cref="AuthenticatedUser"/> object or null if authentication fails</returns>
    public async Task<AuthenticatedUser?> Authenticate(string username, string password)
    {
        var dbUser = await _userRepository.GetByUsernameAsync(username);
        if (dbUser is null)
        {
            _logger.LogWarning("Invalid login attempt: {user} does not exist.", username);
            return null;
        }

        // check for password
        // in real production, password is usually stored as hash
        // so hashing of the input password would be required
        if (string.Compare(password, dbUser.Password, StringComparison.Ordinal) != 0)
        {
            return null;
        }

        return new AuthenticatedUser()
        {
            FirstName = dbUser.FirstName,
            LastName = dbUser.LastName,
            Roles = dbUser.Roles
        };
    }

    /// <summary>
    /// Method for creating JWT authorization token
    /// </summary>
    /// <param name="username"></param>
    /// <param name="displayName"></param>
    /// <param name="claims"></param>
    /// <returns></returns>
    private string GetAuthroizationToken(string username, string displayName, string[] claims)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var securityKey = Encoding.UTF8.GetBytes(_authenticationOptions.JwtSecurityKey);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.GivenName, displayName)
            }),
            Expires = DateTime.UtcNow.Add(TimeSpan.FromHours(12)),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256Signature)
        };

        // TODO: each claim should be equipped with type
        foreach (var claim in claims)
        {
            tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, claim));
        }

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}