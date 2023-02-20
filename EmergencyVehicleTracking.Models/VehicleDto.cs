using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmergencyVehicleTracking.Models;

public class VehicleDto
{
    public long Id { get; set; }
    [Required]
    [MaxLength(32)]
    public string? Name { get; set; }
    [Required]
    [MaxLength(32)]
    public string? RegistrationNumber { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public VehicleType Type { get; set; }
    public Coordinates Location { get; set; }
}