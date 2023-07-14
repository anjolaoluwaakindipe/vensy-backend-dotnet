
using Microsoft.AspNetCore.Identity;

namespace Vensy.Domain.Models;

public class ApplicationUser : IdentityUser
{
    public string Firstname { get; set; } = string.Empty; public string Lastname { get; set; } = string.Empty;

    public List<Company> Companies { get; } = new();

    public List<RefreshToken> RefreshTokens { get; } = new();

}