namespace WeatherForecast.Infrastructure.MongoDb.Services;

using WeatherForecast.Infrastructure.MongoDb.Models;

internal static class MongoDb
{
    public static Dictionary<Guid, CoordinatesDbModel> Coordinates { get; } = [];
}
