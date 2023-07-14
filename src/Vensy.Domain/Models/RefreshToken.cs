namespace Vensy.Domain.Models;


public class RefreshToken
{
    public int Id { get; set; }

    public string Token { get; set; } = string.Empty;

    public string ApplicationUserId { get; set; } = string.Empty;

    public ApplicationUser ApplicationUser { get; set; } = null!;

}