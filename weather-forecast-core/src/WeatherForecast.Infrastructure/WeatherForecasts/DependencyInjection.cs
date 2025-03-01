namespace WeatherForecast.Infrastructure.WeatherForecasts;

using Microsoft.Extensions.DependencyInjection;
using WeatherForecast.Infrastructure.WeatherForecasts.Interfaces;
using WeatherForecast.Infrastructure.WeatherForecasts.Services;

internal static class DependencyInjection
{
    public static IServiceCollection AddWeatherForecast(this IServiceCollection services)
    {
        services.AddScoped<IWeatherForecastReadService, WeatherForecastReadService>();
        services.Decorate<IWeatherForecastReadService, WeatherForecastReadServiceCacheDecorator>();

        return services;
    }
}
