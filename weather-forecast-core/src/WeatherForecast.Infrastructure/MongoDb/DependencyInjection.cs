namespace WeatherForecast.Infrastructure.MongoDb;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WeatherForecast.Application.Common.Shared;
using WeatherForecast.Application.Coordinates.Interfaces;
using WeatherForecast.Infrastructure.Coordinates.Interfaces;
using WeatherForecast.Infrastructure.MongoDb.Interfaces;
using WeatherForecast.Infrastructure.MongoDb.Mappers.Interfaces;
using WeatherForecast.Infrastructure.MongoDb.Mappers.Services;
using WeatherForecast.Infrastructure.MongoDb.Services;

internal static class DependencyInjection
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbOptions>(configuration.GetSection(nameof(MongoDbOptions)));

        services.AddSingleton<IMongoClient>(
            sp =>
            {
                var options = sp.GetRequiredService<IOptions<MongoDbOptions>>().Value;

                return CreateMongoClient(options.ConnectionString);
            }
        );

        services.AddScoped<IMongoCollectionFactory, MongoCollectionFactory>();

        services.AddScoped<ICoordinatesRepository, CoordinatesRepository>();
        services.AddScoped<ICoordinatesReadService, CoordinatesReadService>();

        services.AddSingleton<ICoordinatesDbMapper, CoordinatesDbMapper>();

        return services;
    }

    private static MongoClient CreateMongoClient(string connectionString)
    {
        var settings = MongoClientSettings.FromConnectionString(connectionString);
        settings.ApplicationName = ServiceConstants.ServiceName;

        return new MongoClient(settings);
    }
}
