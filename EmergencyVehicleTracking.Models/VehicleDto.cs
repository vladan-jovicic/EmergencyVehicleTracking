using System.Text.Json.Serialization;

namespace EmergencyVehicleTracking.Models;

public class VehicleDto
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? RegistrationNumber { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public VehicleType Type { get; set; }
}