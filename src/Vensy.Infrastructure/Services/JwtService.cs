using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Vensy.Application.Common.Interfaces.Services;
using Vensy.Application.Interfaces.Persistence;
using Vensy.Infrastructure.Config;

namespace Vensy.Infrastructure.Services;

public class JwtService : IJwtService
{
    private readonly JwtSetting _jwtSetting;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly JwtSecurityTokenHandler _tokenHandler;

    public JwtService(IOptions<JwtSetting> jwtSettings, IDateTimeProvider dateTimeProvider)
    {
        _jwtSetting = jwtSettings.Value;
        _dateTimeProvider = dateTimeProvider;
        _tokenHandler = new JwtSecurityTokenHandler();
    }

    private SymmetricSecurityKey GetSigningKey(string secret)
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
    }

    public string GenerateAccessToken(IList<Claim> claims)
    {
        var signInKey = GetSigningKey(_jwtSetting.AccessTokenSecret);
        _ = int.TryParse(_jwtSetting.AccessTokenValidity, out var result);
        var token = new JwtSecurityToken(
            claims: claims,
            audience: _jwtSetting.Audience,
            issuer: _jwtSetting.Issuer,
            expires: _dateTimeProvider.UtcNow().AddMinutes(result),
            signingCredentials: new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken(IList<Claim> claims)
    {
        var signInKey = GetSigningKey(_jwtSetting.RefreshTokenSecret);
        _ = int.TryParse(_jwtSetting.RefreshTokenValidity, out var result);
        var token = new JwtSecurityToken(
            claims: claims,
            audience: _jwtSetting.Audience,
            issuer: _jwtSetting.Issuer,
            expires: _dateTimeProvider.UtcNow().AddDays(result),
            signingCredentials: new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public ClaimsPrincipal? GetClaimPrincipalFromRefreshToken(string refreshToken)
    {
        var jwtValidationParameters = new TokenValidationParameters()
        {
            ValidAudience = _jwtSetting.Audience,
            ValidIssuer = _jwtSetting.Issuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.RefreshTokenSecret)),
            ValidAlgorithms = new List<string>() { SecurityAlgorithms.HmacSha256 }
        };

        ClaimsPrincipal? principal;
        SecurityToken? securityToken;
        try
        {
            principal = _tokenHandler.ValidateToken(refreshToken, jwtValidationParameters, out securityToken);
        }
        catch
        {
            return null;
        }

        if (principal == null || securityToken == null) return null;

        return principal;
    }
}