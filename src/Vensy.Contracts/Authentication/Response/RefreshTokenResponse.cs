namespace Vensy.Contracts.Authentication.Response;


public record RefreshTokenRespone(string Firstname, string Lastname, string Username, string Email, string AccessToken, string RefreshToken);