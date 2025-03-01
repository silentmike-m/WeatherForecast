namespace WeatherForecast.Infrastructure.Coordinates;

using Microsoft.Extensions.DependencyInjection;
using WeatherForecast.Application.Coordinates.Interfaces;
using WeatherForecast.Infrastructure.Coordinates.Mappers.Interfaces;
using WeatherForecast.Infrastructure.Coordinates.Mappers.Services;
using WeatherForecast.Infrastructure.Coordinates.Services;

internal static class DependencyInjection
{
    public static IServiceCollection AddCoordinates(this IServiceCollection services)
    {
        services.AddScoped<ICoordinatesValidationService, CoordinatesValidationService>();

        services.AddSingleton<ICoordinatesMapper, CoordinatesMapper>();

        return services;
    }
}
