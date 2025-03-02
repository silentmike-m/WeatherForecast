namespace WeatherForecast.Application.Coordinates.Dto;

public sealed record CoordinatesDto
{
    public required Guid Id { get; init; }
    public required decimal Latitude { get; init; }
    public required decimal Longitude { get; init; }
}
