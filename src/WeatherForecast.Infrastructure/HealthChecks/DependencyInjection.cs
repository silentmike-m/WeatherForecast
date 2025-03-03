namespace WeatherForecast.Infrastructure.HealthChecks;

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WeatherForecast.Infrastructure.Cache;
using Mongo = WeatherForecast.Infrastructure.MongoDb.DependencyInjection;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddWeatherForecastHealthChecks(this IServiceCollection services)
    {
        services
            .AddHealthChecks()
            .AddMongoDb(Mongo.CreateMongoClient, failureStatus: HealthStatus.Unhealthy)
            .AddRedis(serviceProvider =>
            {
                var options = serviceProvider.GetRequiredService<RedisOptions>();

                return options.ConnectionString;
            }, failureStatus: HealthStatus.Degraded);

        return services;
    }
}
