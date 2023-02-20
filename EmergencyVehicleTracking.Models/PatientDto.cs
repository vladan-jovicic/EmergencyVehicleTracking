using System.ComponentModel.DataAnnotations;

namespace EmergencyVehicleTracking.Models;

public class PatientDto
{
    public long Id { get; set; }
    [Required]
    [MaxLength(32)]
    public string? FirstName { get; set; }
    [Required]
    [MaxLength(32)]
    public string? LastName { get; set; }
    [Required]
    [MaxLength(128)]
    public string? Address { get; set; }
    [Required]
    [MaxLength(64)]
    public string? City { get; set; }
    [Required]
    [MaxLength(32)]
    public string? Country { get; set; }
}