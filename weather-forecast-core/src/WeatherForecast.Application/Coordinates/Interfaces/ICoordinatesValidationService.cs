namespace WeatherForecast.Application.Coordinates.Interfaces;

public interface ICoordinatesValidationService
{
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> IsLatitudeAndLongitudeUniqueAsync(decimal latitude, decimal longitude, CancellationToken cancellationToken);
}
