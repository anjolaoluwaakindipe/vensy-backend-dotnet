namespace Vensy.Application.Authentication.Common;

public record AuthenticationResult(
    string AccessToken,
    string Email,
    string Firstname,
    string Lastname
);