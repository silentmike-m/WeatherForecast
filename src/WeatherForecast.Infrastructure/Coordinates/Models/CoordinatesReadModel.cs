namespace WeatherForecast.Infrastructure.Coordinates.Models;

internal sealed record CoordinatesReadModel
{
    public Guid Id { get; init; }
    public decimal Latitude { get; init; }
    public decimal Longitude { get; init; }
}
