namespace Vensy.Application.Authentication.Common;


public record LoginResult(string Email, string Lastname, string Firstname, string Username, string AccessToken, string RefreshToken);