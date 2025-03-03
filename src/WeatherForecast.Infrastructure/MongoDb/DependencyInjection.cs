namespace WeatherForecast.Infrastructure.MongoDb;

using System.Diagnostics.CodeAnalysis;
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

[ExcludeFromCodeCoverage]
internal static class DependencyInjection
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbOptions>(configuration.GetSection(nameof(MongoDbOptions)));

        services.AddSingleton(CreateMongoClient);

        services.AddScoped<IMongoCollectionFactory, MongoCollectionFactory>();

        services.AddScoped<ICoordinatesRepository, CoordinatesRepository>();
        services.AddScoped<ICoordinatesReadService, CoordinatesReadService>();

        services.AddSingleton<ICoordinatesDbMapper, CoordinatesDbMapper>();

        return services;
    }

    public static IMongoClient CreateMongoClient(IServiceProvider serviceProvider)
    {
        var options = serviceProvider.GetRequiredService<IOptions<MongoDbOptions>>().Value;

        return CreateMongoClient(options.ConnectionString);
    }

    private static MongoClient CreateMongoClient(string connectionString)
    {
        var settings = MongoClientSettings.FromConnectionString(connectionString);
        settings.ApplicationName = ServiceConstants.ServiceName;

        return new MongoClient(settings);
    }
}
