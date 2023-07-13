using System.Security.Claims;

namespace Vensy.Application.Common.Interfaces.Services;


public interface IJwtService
{
    public string GenerateAccessToken(IList<Claim> claims);
    public string GenerateRefreshToken(IList<Claim> claims);
}