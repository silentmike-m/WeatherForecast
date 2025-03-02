namespace WeatherForecast.Infrastructure.MongoDb.Services;

using MongoDB.Driver;
using WeatherForecast.Application.Coordinates.Interfaces;
using WeatherForecast.Application.Coordinates.Models;
using WeatherForecast.Infrastructure.MongoDb.Constants;
using WeatherForecast.Infrastructure.MongoDb.Interfaces;
using WeatherForecast.Infrastructure.MongoDb.Mappers.Interfaces;
using WeatherForecast.Infrastructure.MongoDb.Models;

internal sealed class CoordinatesRepository : ICoordinatesRepository
{
    private readonly IMongoCollection<CoordinatesDbModel> collection;
    private readonly ICoordinatesDbMapper mapper;

    public CoordinatesRepository(IMongoCollectionFactory collectionFactory, ICoordinatesDbMapper mapper)
    {
        this.collection = collectionFactory.GetCollection<CoordinatesDbModel>(DatabaseCollections.COORDINATES_COLLECTION_NAME);
        this.mapper = mapper;
    }

    public async Task AddCoordinatesAsync(CoordinatesEntity entity, CancellationToken cancellationToken)
    {
        var dbModel = this.mapper.ToDbModel(entity);

        await this.collection.InsertOneAsync(dbModel, new InsertOneOptions(), cancellationToken);
    }

    public async Task DeleteCoordinatesAsync(CoordinatesEntity entity, CancellationToken cancellationToken)
        => await this.collection.DeleteOneAsync(coordinates => coordinates.Id == entity.Id, cancellationToken);

    public async Task<CoordinatesEntity?> GetCoordinatesAsync(Guid id, CancellationToken cancellationToken)
    {
        var filter = Builders<CoordinatesDbModel>.Filter.Eq(coordinates => coordinates.Id, id);

        var dbModel = await this.collection.Find(filter).SingleOrDefaultAsync(cancellationToken);

        var result = dbModel is null
            ? null
            : new CoordinatesEntity(dbModel.Id, dbModel.Longitude, dbModel.Latitude);

        return result;
    }
}
