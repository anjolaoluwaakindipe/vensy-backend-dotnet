namespace Vensy.Infrastructure.Config;

public class JwtSetting
{
    public const string Name = "JwtSettings";

    public string AccessTokenSecret { get; set; } = string.Empty;
    public string AccessTokenValidity { get; set; } = string.Empty;
    public string RefreshTokenSecret { get; set; } = string.Empty;

    public string RefreshTokenValidity { get; set; } = string.Empty;

    public string Issuer { get; set; } = string.Empty;

    public string Audience { get; set; } = string.Empty;
}