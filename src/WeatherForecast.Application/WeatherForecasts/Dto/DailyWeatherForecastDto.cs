namespace WeatherForecast.Application.WeatherForecasts.Dto;

public sealed record DailyWeatherForecastDto
{
    public required WeatherForecastValueDto ApparentTemperatureMax { get; init; }
    public required WeatherForecastValueDto ApparentTemperatureMin { get; init; }
    public required DateOnly Date { get; init; }
    public required WeatherForecastValueDto RainSum { get; init; }
    public required WeatherForecastValueDto ShowersSum { get; init; }
    public required WeatherForecastValueDto SnowfallSum { get; init; }
    public required WeatherForecastValueDto Temperature2mMax { get; init; }
    public required WeatherForecastValueDto Temperature2mMin { get; init; }
    public required int? WeatherCode { get; init; }
}
