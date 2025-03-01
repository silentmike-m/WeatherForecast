namespace WeatherForecast.Infrastructure.WeatherForecasts.Interfaces;

using global::WeatherForecast.Infrastructure.WeatherForecasts.Models;

internal interface IWeatherForecastReadService
{
    Task<WeatherForecastsReadModel?> GetWeatherForecastsAsync(decimal latitude, decimal longitude, CancellationToken cancellationToken);
}
