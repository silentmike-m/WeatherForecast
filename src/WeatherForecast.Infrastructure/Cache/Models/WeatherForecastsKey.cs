namespace WeatherForecast.Infrastructure.Cache.Models;

using WeatherForecast.Infrastructure.WeatherForecasts.Models;

internal sealed record WeatherForecastsKey : CacheKey<WeatherForecastReadModel>
{
    public required decimal Latitude { get; init; }
    public required decimal Longitude { get; init; }
}
