namespace WeatherForecast.Application.Coordinates.Notification;

public sealed record DeletedCoordinates : INotification
{
    public required decimal Latitude { get; init; }
    public required decimal Longitude { get; init; }
}
