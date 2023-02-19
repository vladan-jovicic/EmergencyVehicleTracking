﻿using AutoMapper;
using EmergencyVehicleTracking.DataAccess.Driver;
using EmergencyVehicleTracking.Models;
using Microsoft.Extensions.Logging;

namespace EmergencyVehicleTracking.Services;

public class DriverService
{
    private readonly IDriverRepository _driverRepository;
    private IMapper _mapper;
    private readonly ILogger<DriverService> _logger;

    public DriverService(IDriverRepository driverRepository, IMapper mapper, ILogger<DriverService> logger)
    {
        _driverRepository = driverRepository ?? throw new ArgumentNullException(nameof(driverRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    
    public async Task<List<DriverDto>> GetAllAsync()
    {
        var data = await _driverRepository.GetAllAsync();
        return data.Select(i => _mapper.Map<DriverDto>(i)).ToList();
    }

    public async Task<DriverDto> InsertAsync(DriverDto vehicleDto)
    {
        var dbVehicle = _mapper.Map<DbDriver>(vehicleDto);
        var insertedVehicle = await _driverRepository.InsertAsync(dbVehicle);
        return _mapper.Map<DriverDto>(insertedVehicle);
    }
}