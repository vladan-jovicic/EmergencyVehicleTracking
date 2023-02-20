using System.Text.Json.Serialization;

namespace EmergencyVehicleTracking.Models;

public class DriverStateDto
{
    public long DriverId { get; set; }
    public DriverDto Driver { get; set; }
    public long? SelectedVehicleId { get; set; }
    public long? RouteId { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DriverState State { get; set; }
}