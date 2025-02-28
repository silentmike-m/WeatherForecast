namespace WeatherForecast.WebApi.Models;

using System.Text.Json.Serialization;

public sealed record CoordinatesToAdd
{
    [JsonPropertyName("Latitude")]
    public decimal Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public decimal Longitude { get; init; }
}
