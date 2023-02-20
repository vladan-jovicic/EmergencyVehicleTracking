using AutoMapper;
using EmergencyVehicleTracking.DataAccess.Driver;
using EmergencyVehicleTracking.DataAccess.User;
using EmergencyVehicleTracking.Models;
using Microsoft.Extensions.Logging;

namespace EmergencyVehicleTracking.Services;

public class DriverService
{
    private readonly IDriverRepository _driverRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<DriverService> _logger;

    public DriverService(IDriverRepository driverRepository, IUserRepository userRepository, IMapper mapper, ILogger<DriverService> logger)
    {
        _driverRepository = driverRepository ?? throw new ArgumentNullException(nameof(driverRepository));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    
    public async Task<List<DriverDto>> GetAllAsync()
    {
        var data = await _driverRepository.GetAllAsync();
        return data.Select(i => _mapper.Map<DriverDto>(i)).ToList();
    }

    /// <summary>
    /// Insert new driver and application user.
    /// </summary>
    /// <param name="driver">Driver to insert</param>
    /// <returns>Newly inserted driver</returns>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task<DriverDto> InsertAsync(DriverDto driver)
    {
        // try to insert application user first
        // TODO: for the sake of simplicity, password is hardcoded for every driver user
        // TODO: in real production scenario, an email with activation link is sent to driver
        // TODO: where a password is set
        var dbUser = new DbUser()
        {
            FirstName = driver.FirstName ?? throw new ArgumentNullException(nameof(driver.FirstName)),
            LastName = driver.LastName ?? throw new ArgumentNullException(nameof(driver.LastName)),
            Password = "Test1234",
            Username = $"{driver.FirstName}.{driver.LastName}",
            Roles = new[] { ApplicationRole.DriverUser }
        };

        dbUser = await _userRepository.InsertAsync(dbUser);

        // try to insert driver
        try
        {
            var dbDriver = _mapper.Map<DbDriver>(driver);
            dbDriver.UserId = dbUser.Id;
            var insertedDriver = await _driverRepository.InsertAsync(dbDriver);
            return _mapper.Map<DriverDto>(insertedDriver);
        }
        catch (Exception ex)
        {
            // try to rollback inserted user
            _logger.LogError(ex, "Failed to insert driver after inserting user.");
            throw;
        }
    }
}