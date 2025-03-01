namespace WeatherForecast.Infrastructure.WeatherForecasts.Models;

internal sealed record WeatherForecastValueReadModel
{
    public required string Unit { get; init; }
    public required decimal Value { get; init; }
}
