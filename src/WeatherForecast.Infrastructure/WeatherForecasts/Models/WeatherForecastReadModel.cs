namespace WeatherForecast.Infrastructure.WeatherForecasts.Models;

internal sealed record WeatherForecastReadModel
{
    public required IReadOnlyList<DailyWeatherForecastReadModel> Days { get; init; } = [];
    public required decimal Latitude { get; init; }
    public required decimal Longitude { get; init; }
    public required string Timezone { get; init; } = string.Empty;
}
