namespace WeatherForecast.Application.Coordinates.Interfaces;

using WeatherForecast.Application.Coordinates.Models;

public interface ICoordinatesRepository
{
    Task AddCoordinatesAsync(CoordinatesEntity entity, CancellationToken cancellationToken);
    Task DeleteCoordinatesAsync(Guid id, CancellationToken cancellationToken);
}
