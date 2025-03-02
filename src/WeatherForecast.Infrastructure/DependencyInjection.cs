namespace WeatherForecast.Infrastructure;

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeatherForecast.Infrastructure.Cache;
using WeatherForecast.Infrastructure.Coordinates;
using WeatherForecast.Infrastructure.MongoDb;
using WeatherForecast.Infrastructure.OpenMeteo;
using WeatherForecast.Infrastructure.WeatherForecasts;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        services.AddCache(configuration);

        services.AddCoordinates();

        services.AddMongoDb(configuration);

        services.AddOpenMeteo(configuration);

        services.AddWeatherForecast();

        return services;
    }
}
