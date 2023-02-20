using AutoMapper;
using EmergencyVehicleTracking.DataAccess.Requests;
using EmergencyVehicleTracking.Models;
using Microsoft.Extensions.Logging;

namespace EmergencyVehicleTracking.Services;

public class PatientRequestService
{
    private readonly IPatientRequestRepository _patientRequestRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<PatientRequestService> _logger;

    public PatientRequestService(IPatientRequestRepository patientRequestRepository, IMapper mapper, ILogger<PatientRequestService> logger)
    {
        _patientRequestRepository = patientRequestRepository ?? throw new ArgumentNullException(nameof(patientRequestRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public async Task<List<PatientRequestDto>> GetAllAsync()
    {
        var data = await _patientRequestRepository.GetAllAsync();
        return data.Select(i => _mapper.Map<PatientRequestDto>(i)).ToList();
    }

    public async Task<PatientRequestDto> InsertAsync(PatientRequestDto request)
    {
        var dbPatientRequest = _mapper.Map<DbPatientRequest>(request);
        var insertedRequest = await _patientRequestRepository.InsertAsync(dbPatientRequest);
        // TODO: add new request to route calculator
        return _mapper.Map<PatientRequestDto>(insertedRequest);
    }
}