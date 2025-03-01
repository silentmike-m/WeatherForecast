namespace WeatherForecast.Infrastructure.MongoDb.Services;

using WeatherForecast.Infrastructure.Coordinates.Interfaces;
using WeatherForecast.Infrastructure.Coordinates.Models;
using WeatherForecast.Infrastructure.MongoDb.Mappers.Interfaces;

internal sealed class CoordinatesReadService : ICoordinatesReadService
{
    private readonly ICoordinatesDbMapper mapper;

    public CoordinatesReadService(ICoordinatesDbMapper mapper)
        => this.mapper = mapper;

    public async Task<CoordinatesReadModel?> GetCoordinatesAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = MongoDb.Coordinates.TryGetValue(id, out var dbModel)
            ? this.mapper.ToReadModel(dbModel)
            : null;

        return await Task.FromResult(result);
    }

    public async Task<CoordinatesReadModel?> GetCoordinatesAsync(decimal latitude, decimal longitude, CancellationToken cancellationToken)
    {
        var dbModel = MongoDb.Coordinates.Values
            .Where(coordinates => coordinates.Latitude == latitude)
            .SingleOrDefault(coordinates => coordinates.Longitude == longitude);

        var result = dbModel is null
            ? null
            : this.mapper.ToReadModel(dbModel);

        return await Task.FromResult(result);
    }

    public async Task<IEnumerable<CoordinatesReadModel>> GetCoordinatesAsync(CancellationToken cancellationToken)
    {
        var result = MongoDb.Coordinates.Values
            .Select(dbModel => this.mapper.ToReadModel(dbModel));

        return await Task.FromResult(result);
    }
}
