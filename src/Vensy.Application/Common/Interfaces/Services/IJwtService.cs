using System.Security.Claims;

namespace Vensy.Application.Common.Interfaces.Services;


public interface IJwtService
{
    /// <summary>
    /// Takes a List of claims and generates a
    /// short lasting accestoken token (expires in a set amount of minutes)
    /// </summary>
    /// <param name="claims">List of Claim types that contains user info and roles</param>
    /// <returns>Jwt string containing information of the claims that was passed and additional configuration from the project. Token validity will be short </returns>
    public string GenerateAccessToken(IList<Claim> claims);
    
    /// <summary>
    /// Takes a List of claims and generates a
    /// long lasting refresh token (expires in a set amount of days)
    /// </summary>
    /// <param name="claims">List of Claim types that contains user info and roles</param>
    /// <returns>Jwt string containing information of the claims that was passed and additional configuration from the project</returns>
    public string GenerateRefreshToken(IList<Claim> claims);

    /// <summary>
    /// Takes a refresh Jwt string and validates it based on the project Jwt settings
    /// </summary>
    /// <param name="refreshToken">a Refresh Token published by the server</param>
    /// <returns>Claims about the user that submitted a refresh token</returns>
    public ClaimsPrincipal? GetClaimPrincipalFromRefreshToken(string refreshToken);
}