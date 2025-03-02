namespace WeatherForecast.Infrastructure.Cache;

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherForecast.Infrastructure.Cache.Interfaces;
using WeatherForecast.Infrastructure.Cache.Services;

[ExcludeFromCodeCoverage]
internal static class DependencyInjection
{
    public static IServiceCollection AddCache(this IServiceCollection services, IConfiguration configuration)
    {
        var redisOptions = configuration.GetSection(nameof(RedisOptions)).Get<RedisOptions>();
        redisOptions ??= new RedisOptions();

        services.Configure<CacheOptions>(configuration.GetSection(nameof(CacheOptions)));

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = $"{redisOptions.Server},password={redisOptions.Password}";
            options.InstanceName = redisOptions.InstanceName;
        });

        services.AddScoped<ICacheService, CacheService>();

        return services;
    }
}
