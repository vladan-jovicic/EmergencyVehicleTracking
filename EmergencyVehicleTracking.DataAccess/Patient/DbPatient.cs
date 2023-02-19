﻿namespace EmergencyVehicleTracking.DataAccess.Patient;

public class DbPatient : DbEntity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
}