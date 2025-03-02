namespace WeatherForecast.Infrastructure.Coordinates;

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using WeatherForecast.Application.Coordinates.Interfaces;
using WeatherForecast.Infrastructure.Coordinates.Mappers.Interfaces;
using WeatherForecast.Infrastructure.Coordinates.Mappers.Services;
using WeatherForecast.Infrastructure.Coordinates.Services;

[ExcludeFromCodeCoverage]
internal static class DependencyInjection
{
    public static IServiceCollection AddCoordinates(this IServiceCollection services)
    {
        services.AddScoped<ICoordinatesValidationService, CoordinatesValidationService>();

        services.AddSingleton<ICoordinatesMapper, CoordinatesMapper>();

        return services;
    }
}
