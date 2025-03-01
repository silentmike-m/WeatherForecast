namespace WeatherForecast.Infrastructure.OpenMeteo.Models;

using System.Text.Json.Serialization;

internal sealed record DailyUnitsResponse
{
    [JsonPropertyName("apparent_temperature_max")]
    public string ApparentTemperatureMax { get; init; } = string.Empty;

    [JsonPropertyName("apparent_temperature_min")]
    public string ApparentTemperatureMin { get; init; } = string.Empty;

    [JsonPropertyName("rain_sum")]
    public string RainSum { get; init; } = string.Empty;

    [JsonPropertyName("showers_sum")]
    public string ShowersSum { get; init; } = string.Empty;

    [JsonPropertyName("snowfall_sum")]
    public string SnowfallSum { get; init; } = string.Empty;

    [JsonPropertyName("temperature_2m_max")]
    public string Temperature2mMax { get; init; } = string.Empty;

    [JsonPropertyName("temperature_2m_min")]
    public string Temperature2mMin { get; init; } = string.Empty;

    [JsonPropertyName("weather_code")]
    public string WeatherCode { get; init; } = string.Empty;
}
