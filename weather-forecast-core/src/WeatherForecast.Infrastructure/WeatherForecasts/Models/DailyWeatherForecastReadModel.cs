namespace WeatherForecast.Infrastructure.WeatherForecasts.Models;

internal sealed record DailyWeatherForecastReadModel
{
    public required WeatherForecastValueReadModel ApparentTemperatureMax { get; init; }
    public required WeatherForecastValueReadModel ApparentTemperatureMin { get; init; }
    public required DateOnly Date { get; init; }
    public required WeatherForecastValueReadModel RainSum { get; init; }
    public required WeatherForecastValueReadModel ShowersSum { get; init; }
    public required WeatherForecastValueReadModel SnowfallSum { get; init; }
    public required WeatherForecastValueReadModel Temperature2mMax { get; init; }
    public required WeatherForecastValueReadModel Temperature2mMin { get; init; }
    public required string WeatherCode { get; init; }
}
