namespace Vensy.Contracts.Authentication.Response;

public record LoginResponse(string Email, string Firstname, string Lastname, string Username, string AccessToken, string? RefreshToken);