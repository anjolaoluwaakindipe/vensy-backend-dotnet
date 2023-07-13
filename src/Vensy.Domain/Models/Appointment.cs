using System;
namespace Vensy.Domain.Models;


public class Appointment
{
    public int Id { get; set; }

    public string CustomerEmail { get; set; } = string.Empty;
    public string CustomerPhone { get; set; } = string.Empty;

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public int VenueId {get; set;}

    public Venue Venue {get; set;} = null!;
}