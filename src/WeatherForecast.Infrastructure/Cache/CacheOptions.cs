namespace WeatherForecast.Infrastructure.Cache;

internal sealed record CacheOptions
{
    public int WeatherForecastsExpirationInMinutes { get; init; } = 5;
}
