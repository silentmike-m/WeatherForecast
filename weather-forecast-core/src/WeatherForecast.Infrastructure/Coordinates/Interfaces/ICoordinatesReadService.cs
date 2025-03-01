namespace WeatherForecast.Infrastructure.Coordinates.Interfaces;

using WeatherForecast.Infrastructure.Coordinates.Models;

internal interface ICoordinatesReadService
{
    Task<CoordinatesReadModel?> GetCoordinatesAsync(Guid id, CancellationToken cancellationToken);
    Task<CoordinatesReadModel?> GetCoordinatesAsync(decimal latitude, decimal longitude, CancellationToken cancellationToken);
    Task<IEnumerable<CoordinatesReadModel>> GetCoordinatesAsync(CancellationToken cancellationToken);
}
