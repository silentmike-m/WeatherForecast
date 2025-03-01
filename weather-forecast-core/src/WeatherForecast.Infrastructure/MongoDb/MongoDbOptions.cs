namespace WeatherForecast.Infrastructure.MongoDb;

internal sealed record MongoDbOptions
{
    public string ConnectionString { get; init; } = string.Empty;
    public string DatabaseName { get; init; } = string.Empty;
}
