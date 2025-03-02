namespace WeatherForecast.Infrastructure.WeatherForecasts.Interfaces;

using WeatherForecast.Infrastructure.WeatherForecasts.Models;

internal interface IWeatherForecastReadService
{
    Task<WeatherForecastReadModel?> GetWeatherForecastsAsync(decimal latitude, decimal longitude, CancellationToken cancellationToken);
}
