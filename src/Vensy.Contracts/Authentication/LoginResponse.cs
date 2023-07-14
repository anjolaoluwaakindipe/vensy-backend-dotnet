namespace Vensy.Contracts.Authentication;

public record LoginResponse(string Email, string Firstname, string Lastname, string Username, string AccessToken, string? RefreshToken);