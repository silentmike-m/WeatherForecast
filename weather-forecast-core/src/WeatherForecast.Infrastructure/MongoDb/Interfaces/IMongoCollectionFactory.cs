namespace WeatherForecast.Infrastructure.MongoDb.Interfaces;

using MongoDB.Driver;

internal interface IMongoCollectionFactory
{
    IMongoCollection<T> GetCollection<T>(string collectionName)
        where T : class;
}
