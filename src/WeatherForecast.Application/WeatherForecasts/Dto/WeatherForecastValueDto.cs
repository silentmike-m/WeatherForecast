namespace WeatherForecast.Application.WeatherForecasts.Dto;

public sealed record WeatherForecastValueDto
{
    public required string Unit { get; init; }
    public required decimal? Value { get; init; }
}
