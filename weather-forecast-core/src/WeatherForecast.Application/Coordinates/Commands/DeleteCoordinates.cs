namespace WeatherForecast.Application.Coordinates.Commands;

public sealed record DeleteCoordinates : IRequest
{
    public required Guid Id { get; init; }
}
