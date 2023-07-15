namespace Vensy.Application.Authentication.Common;


public record ValidateRefreshTokenResult(string Username, string Firstname, string Lastname, string Email, string AccessToken, string RefreshToken);