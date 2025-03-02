namespace WeatherForecast.Application.WeatherForecasts.Dto;

public sealed record WeatherForecastDto
{
    public required IReadOnlyList<DailyWeatherForecastDto> Days { get; init; } = [];
    public required decimal Latitude { get; init; }
    public required decimal Longitude { get; init; }
    public required string Timezone { get; init; } = string.Empty;
}
