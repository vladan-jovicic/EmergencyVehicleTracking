using AutoMapper;
using EmergencyVehicleTracking.DataAccess.Driver;
using EmergencyVehicleTracking.DataAccess.Patient;
using EmergencyVehicleTracking.DataAccess.Requests;
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
            .ForMember(dest => dest.Type, src => src.MapFrom(i => (int)i.Type))
            .ForMember(dest => dest.LocationX, src => src.MapFrom(i => i.Location.X))
            .ForMember(dest => dest.LocationY, src => src.MapFrom(i => i.Location.Y));
        CreateMap<DbVehicle, VehicleDto>()
            .ForMember(dest => dest.Type, src => src.MapFrom(i => (VehicleType)i.Type))
            .ForMember(dest => dest.Location, src => src.MapFrom(i => new Coordinates()
            {
                X = i.LocationX,
                Y = i.LocationY
            }));
        
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
        
        // Requests
        CreateMap<PatientRequestDto, DbPatientRequest>()
            .ForMember(dest => dest.PickUpLocationX, src => src.MapFrom(i => i.PickUpLocation.X))
            .ForMember(dest => dest.PickUpLocationY, src => src.MapFrom(i => i.PickUpLocation.Y))
            .ForMember(dest => dest.DropOffLocationX, src => src.MapFrom(i => i.DropOffLocation.X))
            .ForMember(dest => dest.DropOffLocationY, src => src.MapFrom(i => i.DropOffLocation.Y))
            .ForMember(dest => dest.Status, src => src.MapFrom(i => (int)i.Status));
        CreateMap<DbPatientRequest, PatientRequestDto>()
            .ForMember(dest => dest.PickUpLocation, src => src.MapFrom(i => new Coordinates()
            {
                X = i.PickUpLocationX,
                Y = i.PickUpLocationY
            }))
            .ForMember(dest => dest.DropOffLocation, src => src.MapFrom(i => new Coordinates()
            {
                X = i.DropOffLocationX,
                Y = i.DropOffLocationY
            }))
            .ForMember(dest => dest.Status, src => src.MapFrom(i => (PatientRequestStatus)i.Status));
    }
}