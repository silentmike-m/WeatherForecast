namespace WeatherForecast.Application.Coordinates.Commands;

public sealed record AddCoordinates : IRequest
{
    public required decimal Latitude { get; init; }
    public required decimal Longitude { get; init; }
}
