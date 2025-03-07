namespace WeatherForecast.Infrastructure.WeatherForecasts;

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using WeatherForecast.Infrastructure.WeatherForecasts.Interfaces;
using WeatherForecast.Infrastructure.WeatherForecasts.Mappers.Interfaces;
using WeatherForecast.Infrastructure.WeatherForecasts.Mappers.Services;
using WeatherForecast.Infrastructure.WeatherForecasts.Services;

[ExcludeFromCodeCoverage]
internal static class DependencyInjection
{
    public static IServiceCollection AddWeatherForecast(this IServiceCollection services)
    {
        services.AddScoped<IWeatherForecastReadService, WeatherForecastReadService>();
        services.Decorate<IWeatherForecastReadService, WeatherForecastReadServiceCacheDecorator>();

        services.AddSingleton<IWeatherForecastMapper, WeatherForecastMapper>();

        return services;
    }
}
