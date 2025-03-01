namespace WeatherForecast.Infrastructure.MongoDb.Models;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

internal sealed record CoordinatesDbModel
{
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; init; }
    public decimal Latitude { get; init; }
    public decimal Longitude { get; init; }
}
