using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BolaSocial.Api.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
        services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();
        return services;
    }
}

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}

public sealed class SystemDateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}

public sealed class JwtOptions
{
    public string Key { get; init; } = "dev-secret";
    public int AccessTokenMinutes { get; init; } = 15;
    public int RefreshTokenDays { get; init; } = 7;
}
