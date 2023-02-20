using System.Text.Json.Serialization;

namespace EmergencyVehicleTracking.Models;

public class PatientRequestDto
{
    public long Id { get; set; }
    public long PatientId { get; set; }
    public Coordinates PickUpLocation { get; set; }
    public Coordinates DropOffLocation { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PatientRequestStatus Status { get; set; }
}