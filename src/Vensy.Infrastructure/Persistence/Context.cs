using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Vensy.Domain.Models;

namespace Vensy.Infrastructure.Persistence;


public  class Context : IdentityDbContext<User>
{
    public DbSet<Company> Companies { get; set; }
    public DbSet<Venue> Venues { get; set; }
    public DbSet<Appointment> Appointments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=testvensy;Username=anjolaoluwaakindipe;Password=daniel23082000")
                      .UseSnakeCaseNamingConvention();
    }

}