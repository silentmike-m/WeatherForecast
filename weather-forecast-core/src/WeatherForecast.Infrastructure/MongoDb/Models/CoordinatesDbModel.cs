namespace WeatherForecast.Infrastructure.MongoDb.Models;

internal sealed record CoordinatesDbModel
{
    public Guid Id { get; init; }
    public decimal Latitude { get; init; }
    public decimal Longitude { get; init; }
}
