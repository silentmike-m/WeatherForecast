namespace WeatherForecast.Application.WeatherForecasts.Queries;

using WeatherForecast.Application.WeatherForecasts.Dto;

public sealed record GetWeatherForecast : IRequest<WeatherForecastDto?>
{
    public required Guid CoordinatesId { get; init; }
}
