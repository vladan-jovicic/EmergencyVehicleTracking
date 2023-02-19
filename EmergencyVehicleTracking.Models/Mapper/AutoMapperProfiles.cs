using AutoMapper;
using EmergencyVehicleTracking.DataAccess.Patient;

namespace EmergencyVehicleTracking.Models.Mapper;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        // Patient mapping
        CreateMap<PatientDto, DbPatient>();
        CreateMap<DbPatient, PatientDto>();
    }
}