using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        services.Configure<PostgresDatabaseSettings>(configuration.GetSection(PostgresDatabaseSettings.PropertyName));
        services.AddDbContext<Context>(options =>
        {
            var postgresSettings = configuration.GetSection(PostgresDatabaseSettings.PropertyName).Get<PostgresDatabaseSettings>();
            options.UseNpgsql(postgresSettings?.ConnectionString()).UseCamelCaseNamingConvention();
        });
        services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<Context>().AddDefaultTokenProviders();
        services.Configure<IdentityOptions>(IdentityOptionsConfiguration);
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IDateTimeProvider, DateTimeProvider>();
        return services;
    }

    public static void IdentityOptionsConfiguration(IdentityOptions options)
    {
        options.Password.RequireDigit = false;
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Lockout.AllowedForNewUsers = false;
        options.Lockout.MaxFailedAccessAttempts = 20;
        options.User.RequireUniqueEmail = true;
    }
}