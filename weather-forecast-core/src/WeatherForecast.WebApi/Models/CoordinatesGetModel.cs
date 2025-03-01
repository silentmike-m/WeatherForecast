namespace WeatherForecast.WebApi.Models;

using System.Text.Json.Serialization;

public sealed record CoordinatesGetModel
{
    [JsonPropertyName("id")]
    public required Guid Id { get; init; }

    [JsonPropertyName("Latitude")]
    public required decimal Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public required decimal Longitude { get; init; }
}
