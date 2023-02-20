using System.ComponentModel.DataAnnotations;

namespace EmergencyVehicleTracking.Models;

public class DriverDto
{
    public long Id { get; set; }
    [Required]
    [MaxLength(32)]
    public string? FirstName { get; set; }
    [Required]
    [MaxLength(32)]
    public string? LastName { get; set; }
    [Required]
    public Coordinates Location { get; set; }
    [Required]
    public float? Perimeter { get; set; }
}