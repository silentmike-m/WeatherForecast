namespace WeatherForecast.Infrastructure.MongoDb.Services;

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WeatherForecast.Infrastructure.MongoDb.Interfaces;

[ExcludeFromCodeCoverage]
internal sealed class MongoCollectionFactory : IMongoCollectionFactory
{
    private readonly IMongoClient client;
    private readonly MongoDbOptions options;

    public MongoCollectionFactory(IMongoClient client, IOptions<MongoDbOptions> options)
    {
        this.client = client;
        this.options = options.Value;
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName) where T : class
    {
        if (string.IsNullOrWhiteSpace(this.options.DatabaseName))
        {
            throw new ArgumentException("Empty DatabaseName in configuration");
        }

        var db = this.client.GetDatabase(this.options.DatabaseName);
        var collection = db.GetCollection<T>(collectionName);

        return collection;
    }
}
