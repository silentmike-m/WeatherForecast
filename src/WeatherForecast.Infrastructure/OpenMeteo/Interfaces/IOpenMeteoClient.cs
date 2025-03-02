namespace WeatherForecast.Infrastructure.OpenMeteo.Interfaces;

using global::WeatherForecast.Infrastructure.OpenMeteo.Models;

internal interface IOpenMeteoClient
{
    Task<GetWeatherForecastResponse?> GetWeatherForecastAsync(decimal latitude, decimal longitude, CancellationToken cancellationToken);
}
