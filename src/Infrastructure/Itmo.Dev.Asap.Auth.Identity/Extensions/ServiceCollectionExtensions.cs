using Itmo.Dev.Asap.Auth.Application.Abstractions.Services;
using Itmo.Dev.Asap.Auth.Identity.Entities;
using Itmo.Dev.Asap.Auth.Identity.Services;
using Itmo.Dev.Asap.Auth.Identity.Tools;
using Itmo.Dev.Platform.Postgres.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Itmo.Dev.Asap.Auth.Identity.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddIdentityConfiguration(
        this IServiceCollection collection,
        IConfiguration configuration)
    {
        collection
            .AddOptions<IdentityConfiguration>()
            .BindConfiguration("Infrastructure:Identity:Configuration");

        IConfigurationSection postgresSection = configuration
            .GetSection("Infrastructure:PostgresConfiguration");

        PostgresConnectionConfiguration? postgresConfiguration = postgresSection
            .Get<PostgresConnectionConfiguration>();

        if (postgresConfiguration is null)
            throw new InvalidOperationException("Postgres is not configured");

        collection.AddScoped<IAuthorizationService, AuthorizationService>();

        collection.AddDbContext<AsapIdentityContext>(builder => builder
            .UseNpgsql(postgresConfiguration.ToConnectionString())
            .UseLoggerFactory(LoggerFactory.Create(x => x.AddSerilog().SetMinimumLevel(LogLevel.Trace))));

        collection.AddIdentity<AsapIdentityUser, AsapIdentityRole>()
            .AddEntityFrameworkStores<AsapIdentityContext>()
            .AddDefaultTokenProviders();

        return collection;
    }
}