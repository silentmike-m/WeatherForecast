namespace WeatherForecast.Infrastructure.MongoDb.Services;

using WeatherForecast.Application.Coordinates.Interfaces;
using WeatherForecast.Application.Coordinates.Models;
using WeatherForecast.Infrastructure.MongoDb.Mappers.Interfaces;

internal sealed class CoordinatesRepository : ICoordinatesRepository
{
    private readonly ICoordinatesDbMapper mapper;

    public CoordinatesRepository(ICoordinatesDbMapper mapper)
        => this.mapper = mapper;

    public async Task AddCoordinatesAsync(CoordinatesEntity entity, CancellationToken cancellationToken)
    {
        var dbModel = this.mapper.ToDbModel(entity);

        MongoDb.Coordinates.Add(entity.Id, dbModel);

        await Task.CompletedTask;
    }

    public async Task DeleteCoordinatesAsync(Guid id, CancellationToken cancellationToken)
    {
        MongoDb.Coordinates.Remove(id);

        await Task.CompletedTask;
    }
}
