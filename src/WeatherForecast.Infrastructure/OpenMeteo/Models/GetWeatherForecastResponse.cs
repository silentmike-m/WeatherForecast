namespace WeatherForecast.Infrastructure.OpenMeteo.Models;

using System.Text.Json.Serialization;

internal sealed record GetWeatherForecastResponse
{
    [JsonPropertyName("daily")]
    public DailyResponse Daily { get; init; } = new();

    [JsonPropertyName("daily_units")]

    public DailyUnitsResponse DailyUnits { get; init; } = new();

    [JsonPropertyName("latitude")]
    public decimal Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public decimal Longitude { get; init; }

    [JsonPropertyName("timezone")]
    public string Timezone { get; init; } = string.Empty;
}
