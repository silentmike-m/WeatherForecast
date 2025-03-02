namespace WeatherForecast.WebApi.Models;

using System.Text.Json.Serialization;

public sealed record WeatherForecastValueGetModel
{
    [JsonPropertyName("unit")]
    public required string Unit { get; init; }

    [JsonPropertyName("value")]
    public required decimal? Value { get; init; }
}
