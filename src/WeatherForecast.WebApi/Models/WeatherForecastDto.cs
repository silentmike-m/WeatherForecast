namespace WeatherForecast.WebApi.Models;

using System.Text.Json.Serialization;

public sealed record WeatherForecastGetModel
{
    [JsonPropertyName("days")]
    public required IReadOnlyList<DailyWeatherForecastGetModel> Days { get; init; } = [];

    [JsonPropertyName("latitude")]
    public required decimal Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public required decimal Longitude { get; init; }

    [JsonPropertyName("timezone")]
    public required string Timezone { get; init; } = string.Empty;
}
