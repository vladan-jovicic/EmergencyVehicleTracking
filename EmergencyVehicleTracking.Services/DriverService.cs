using AutoMapper;
using EmergencyVehicleTracking.DataAccess.Driver;
using EmergencyVehicleTracking.DataAccess.DriverVehicle;
using EmergencyVehicleTracking.DataAccess.User;
using EmergencyVehicleTracking.DataAccess.Vehicle;
using EmergencyVehicleTracking.Models;
using Microsoft.Extensions.Logging;

namespace EmergencyVehicleTracking.Services;

public class DriverService
{
    private readonly IDriverRepository _driverRepository;
    private readonly IUserRepository _userRepository;
    private readonly IDriverVehicleMapRepository _driverVehicleMapRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<DriverService> _logger;

    public DriverService(
        IDriverRepository driverRepository,
        IUserRepository userRepository,
        IDriverVehicleMapRepository driverVehicleMapRepository,
        IVehicleRepository vehicleRepository,
        IMapper mapper,
        ILogger<DriverService> logger)
    {
        _driverRepository = driverRepository ?? throw new ArgumentNullException(nameof(driverRepository));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _driverVehicleMapRepository = driverVehicleMapRepository ?? throw new ArgumentNullException(nameof(driverVehicleMapRepository));
        _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
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

    /// <summary>
    /// Gets general driver state
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    /// <exception cref="UserVisibleException"></exception>
    public async Task<DriverStateDto> GetDriverState(string username)
    {
        var dbUser = await _userRepository.GetByUsernameAsync(username);

        if (dbUser is null)
        {
            throw new UserVisibleException("E001", "Invalid driver.");
        }

        var drivers = await _driverRepository.GetAllAsync();
        var currentDriver = drivers.FirstOrDefault(i => i.UserId == dbUser.Id);

        if (currentDriver is null)
        {
            throw new UserVisibleException("E001", "Invalid driver.");
        }
        
        // we can infer driver state from assignments
        // if there is assigned route, then driver has "Driving" state
        // if there is assigned vehicle, then driver has "SelectRoute" state
        // otherwise, it is "SelectVehicle" state

        var state = DriverState.SelectVehicle;
        var assignedVehicle = await _driverVehicleMapRepository.GetVehicleAssignedToDriverAsync(currentDriver.Id);
        if (assignedVehicle is not null)
        {
            state = DriverState.SelectRoute;
        }

        return new DriverStateDto()
        {
            Driver = _mapper.Map<DriverDto>(currentDriver),
            DriverId = currentDriver.Id,
            SelectedVehicleId = assignedVehicle,
            State = state
        };
    }

    /// <summary>
    /// Get vehicles which can be assigned to driver
    /// </summary>
    /// <param name="username"></param>
    /// <returns></returns>
    public async Task<List<VehicleDto>> GetAvailableVehicles(string username)
    {
        var driverState = await GetDriverState(username);

        // if current state is not correct, return empty list
        // TODO: should we throw exception here
        if (driverState.State != DriverState.SelectVehicle)
        {
            return new List<VehicleDto>();
        }
        
        var allVehicles = await _vehicleRepository.GetAllAsync();
        
        // select all vehicles which are in drivers region
        var inRegionVehicles = allVehicles.Where(i => IsInRegion(driverState.Driver.Location, driverState.Driver.Perimeter, new Coordinates()
        {
            X = i.LocationX,
            Y = i.LocationY
        }));
        
        // filter out all vehicles already assigned to some driver
        var possibleVehicles = new List<VehicleDto>();
        foreach (var vehicle in inRegionVehicles)
        {
            var driverId = await _driverVehicleMapRepository.GetDriverAssignedToVehicleAsync(vehicle.Id);
            // if there is no assigned driver, we got a candidate
            if (driverId is null)
            {
                possibleVehicles.Add(_mapper.Map<VehicleDto>(vehicle));
            }
        }

        return possibleVehicles;
    }

    /// <summary>
    /// Assign vehicle to driver
    /// TODO: there might be concurrency problem here
    /// TODO: Use concurrent dictionary to store mapping instead of in memory repository
    /// </summary>
    /// <param name="vehicleId"></param>
    /// <param name="driverId"></param>
    /// <returns></returns>
    public async Task<bool> SelectVehicle(long vehicleId, long driverId)
    {
        // TODO: check if there is already assignment
        var driverVehicleMap = new DbDriverVehicleMap()
        {
            DriverId = driverId,
            VehicleId = vehicleId,
            StartDate = DateTime.Now
        };

        await _driverVehicleMapRepository.InsertAsync(driverVehicleMap);
        return true;
    }

    private static bool IsInRegion(Coordinates basePoint, float? perimeter, Coordinates point)
    {
        var d = Math.Sqrt((basePoint.X - point.X) * (basePoint.X - point.X)
                          + (basePoint.Y - point.Y) * (basePoint.Y - point.Y));

        return d <= perimeter;
    }
}