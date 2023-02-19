using AutoMapper;
using EmergencyVehicleTracking.DataAccess.Driver;
using EmergencyVehicleTracking.DataAccess.Patient;
using EmergencyVehicleTracking.DataAccess.Vehicle;

namespace EmergencyVehicleTracking.Models.Mapper;

public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        // Patient mapping
        CreateMap<PatientDto, DbPatient>();
        CreateMap<DbPatient, PatientDto>();
        
        // Vehicle mapping
        CreateMap<VehicleDto, DbVehicle>()
            .ForMember(dest => dest.Type, src => src.MapFrom(i => (int)i.Type));
        CreateMap<DbVehicle, VehicleDto>()
            .ForMember(dest => dest.Type, src => src.MapFrom(i => (VehicleType)i.Type));
        
        // Driver mapping
        CreateMap<DriverDto, DbDriver>()
            .ForMember(dest => dest.LocationX, src => src.MapFrom(i => i.Location.X))
            .ForMember(dest => dest.LocationY, src => src.MapFrom(i => i.Location.Y));
        CreateMap<DbDriver, DriverDto>()
            .ForMember(dest => dest.Location, src => src.MapFrom(i => new Coordinates()
            {
                X = i.LocationX,
                Y = i.LocationY
            }));
    }
}