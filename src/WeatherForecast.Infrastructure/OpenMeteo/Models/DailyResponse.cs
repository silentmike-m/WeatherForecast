namespace WeatherForecast.Infrastructure.OpenMeteo.Models;

using System.Text.Json.Serialization;

internal sealed record DailyResponse
{
    [JsonPropertyName("apparent_temperature_max")]
    public IReadOnlyList<decimal> ApparentTemperatureMax { get; init; } = [];

    [JsonPropertyName("apparent_temperature_min")]
    public IReadOnlyList<decimal> ApparentTemperatureMin { get; init; } = [];

    [JsonPropertyName("time")]
    public IReadOnlyList<DateOnly> Days { get; init; } = [];

    [JsonPropertyName("rain_sum")]
    public IReadOnlyList<decimal> RainSum { get; init; } = [];

    [JsonPropertyName("showers_sum")]
    public IReadOnlyList<decimal> ShowersSum { get; init; } = [];

    [JsonPropertyName("snowfall_sum")]
    public IReadOnlyList<decimal> SnowfallSum { get; init; } = [];

    [JsonPropertyName("temperature_2m_max")]
    public IReadOnlyList<decimal> Temperature2mMax { get; init; } = [];

    [JsonPropertyName("temperature_2m_min")]
    public IReadOnlyList<decimal> Temperature2mMin { get; init; } = [];

    [JsonPropertyName("weather_code")]
    public IReadOnlyList<int> WeatherCodes { get; init; } = [];
}
