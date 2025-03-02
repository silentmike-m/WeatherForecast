namespace WeatherForecast.Infrastructure.MongoDb.Services;

using MongoDB.Driver;
using WeatherForecast.Infrastructure.Coordinates.Interfaces;
using WeatherForecast.Infrastructure.Coordinates.Models;
using WeatherForecast.Infrastructure.MongoDb.Constants;
using WeatherForecast.Infrastructure.MongoDb.Exceptions;
using WeatherForecast.Infrastructure.MongoDb.Interfaces;
using WeatherForecast.Infrastructure.MongoDb.Mappers.Interfaces;
using WeatherForecast.Infrastructure.MongoDb.Models;

internal sealed class CoordinatesReadService : ICoordinatesReadService
{
    private readonly IMongoCollection<CoordinatesDbModel> collection;
    private readonly ICoordinatesDbMapper mapper;

    public CoordinatesReadService(IMongoCollectionFactory collectionFactory, ICoordinatesDbMapper mapper)
    {
        this.collection = collectionFactory.GetCollection<CoordinatesDbModel>(DatabaseCollections.COORDINATES_COLLECTION_NAME);
        this.mapper = mapper;
    }

    public async Task<CoordinatesReadModel?> GetCoordinatesAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var filter = Builders<CoordinatesDbModel>.Filter.Eq(coordinates => coordinates.Id, id);

            var dbModel = await this.collection.Find(filter).SingleOrDefaultAsync(cancellationToken);

            var result = dbModel is null
                ? null
                : this.mapper.ToReadModel(dbModel);

            return result;
        }
        catch (Exception exception)
        {
            throw new MongoDbConnectionException(exception);
        }
    }

    public async Task<CoordinatesReadModel?> GetCoordinatesAsync(decimal latitude, decimal longitude, CancellationToken cancellationToken)
    {
        try
        {
            var filter = Builders<CoordinatesDbModel>.Filter.And(
                Builders<CoordinatesDbModel>.Filter.Eq(coordinates => coordinates.Latitude, latitude),
                Builders<CoordinatesDbModel>.Filter.Eq(coordinates => coordinates.Longitude, longitude)
            );

            var dbModel = await this.collection.Find(filter).SingleOrDefaultAsync(cancellationToken);

            var result = dbModel is null
                ? null
                : this.mapper.ToReadModel(dbModel);

            return result;
        }
        catch (Exception exception)
        {
            throw new MongoDbConnectionException(exception);
        }
    }

    public async Task<IEnumerable<CoordinatesReadModel>> GetCoordinatesAsync(CancellationToken cancellationToken)
    {
        try
        {
            var filter = Builders<CoordinatesDbModel>.Filter.Empty;

            var dbModels = await this.collection.Find(filter).ToListAsync(cancellationToken);

            var result = dbModels.Select(this.mapper.ToReadModel);

            return result;
        }
        catch (Exception exception)
        {
            throw new MongoDbConnectionException(exception);
        }
    }
}
