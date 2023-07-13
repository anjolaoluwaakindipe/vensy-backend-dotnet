using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vensy.Application.Common.Interfaces.Services;
using Vensy.Application.Interfaces.Persistence;
using Vensy.Domain.Models;
using Vensy.Infrastructure.Config;
using Vensy.Infrastructure.Persistence;
using Vensy.Infrastructure.Services;

namespace Vensy.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.Configure<JwtSetting>(configuration.GetSection(JwtSetting.Name));
        services.AddDbContext<Context>();
        services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<Context>().AddDefaultTokenProviders();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        return services;
    }
}