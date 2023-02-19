using AutoMapper;
using EmergencyVehicleTracking.DataAccess.Vehicle;
using EmergencyVehicleTracking.Models;
using Microsoft.Extensions.Logging;

namespace EmergencyVehicleTracking.Services;

public class VehicleService
{
    private readonly IMapper _mapper;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly ILogger<VehicleService> _logger;

    public VehicleService(IVehicleRepository vehicleRepository, IMapper mapper, ILogger<VehicleService> logger)
    {
        _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public async Task<List<VehicleDto>> GetAllAsync()
    {
        var data = await _vehicleRepository.GetAllAsync();
        return data.Select(i => _mapper.Map<VehicleDto>(i)).ToList();
    }

    public async Task<VehicleDto> InsertAsync(VehicleDto vehicleDto)
    {
        var dbVehicle = _mapper.Map<DbVehicle>(vehicleDto);
        var insertedVehicle = await _vehicleRepository.InsertAsync(dbVehicle);
        return _mapper.Map<VehicleDto>(insertedVehicle);
    }
}