namespace WeatherForecast.WebApi.Models;

using System.Text.Json.Serialization;

public sealed record DailyWeatherForecastGetModel
{
    [JsonPropertyName("apparent_temperature_max")]
    public required WeatherForecastValueGetModel ApparentTemperatureMax { get; init; }

    [JsonPropertyName("apparent_temperature_min")]
    public required WeatherForecastValueGetModel ApparentTemperatureMin { get; init; }

    [JsonPropertyName("date")] public required DateOnly Date { get; init; }
    public required WeatherForecastValueGetModel RainSum { get; init; }

    [JsonPropertyName("showers_sum")]
    public required WeatherForecastValueGetModel ShowersSum { get; init; }

    [JsonPropertyName("snowfall_sum")]
    public required WeatherForecastValueGetModel SnowfallSum { get; init; }

    [JsonPropertyName("temperature_2m_max")]

    public required WeatherForecastValueGetModel Temperature2mMax { get; init; }

    [JsonPropertyName("temperature_2m_min")]
    public required WeatherForecastValueGetModel Temperature2mMin { get; init; }

    [JsonPropertyName("weather_code")]
    public required int? WeatherCode { get; init; }
}
