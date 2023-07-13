namespace Vensy.Domain.Models;

public class Venue
{
    public int VenueId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public string Address { get; set; } = string.Empty;

    public int CompanyId { get; set; }

    public Company Company { get; set; } = null!;

    public int Capacity { get; set; }

    public List<Appointment> Appointments { get; set; } = new List<Appointment>();
}