using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Vensy.Domain.Models;
using Vensy.Infrastructure.Config;

namespace Vensy.Infrastructure.Persistence;


public class Context : IdentityDbContext<ApplicationUser>
{

    public DbSet<Company> Companies { get; set; } = null!;
    public DbSet<Venue> Venues { get; set; } = null!;
    public DbSet<Appointment> Appointments { get; set; } = null!;

    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;


    public Context(DbContextOptions options) : base(options)
    {
    }


}