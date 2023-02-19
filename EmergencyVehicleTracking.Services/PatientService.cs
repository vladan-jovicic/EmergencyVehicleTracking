using AutoMapper;
using EmergencyVehicleTracking.DataAccess.Patient;
using EmergencyVehicleTracking.Models;

namespace EmergencyVehicleTracking.Services;

public class PatientService
{
    private readonly IMapper _mapper;
    private readonly IPatientRepository _patientRepository;
    
    public PatientService(IMapper mapper, IPatientRepository patientRepository)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
    }

    public async Task<List<PatientDto>> GetAllPatientsAsync()
    {
        var data = await _patientRepository.GetAllAsync();
        return data.Select(i => _mapper.Map<PatientDto>(i)).ToList();
    }

    public async Task<PatientDto> InsertAsync(PatientDto patientDto)
    {
        var dbPatient = _mapper.Map<DbPatient>(patientDto);
        var insertedPatient = await _patientRepository.InsertAsync(dbPatient);
        return _mapper.Map<PatientDto>(insertedPatient);
    }
}