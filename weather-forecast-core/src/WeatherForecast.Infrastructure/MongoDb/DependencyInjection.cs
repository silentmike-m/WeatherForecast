namespace WeatherForecast.Infrastructure.MongoDb;

using Microsoft.Extensions.DependencyInjection;
using WeatherForecast.Application.Coordinates.Interfaces;
using WeatherForecast.Infrastructure.Coordinates.Interfaces;
using WeatherForecast.Infrastructure.MongoDb.Mappers.Interfaces;
using WeatherForecast.Infrastructure.MongoDb.Mappers.Services;
using WeatherForecast.Infrastructure.MongoDb.Services;

internal static class DependencyInjection
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services)
    {
        services.AddScoped<ICoordinatesRepository, CoordinatesRepository>();
        services.AddScoped<ICoordinatesReadService, CoordinatesReadService>();

        services.AddSingleton<ICoordinatesDbMapper, CoordinatesDbMapper>();

        return services;
    }
}
