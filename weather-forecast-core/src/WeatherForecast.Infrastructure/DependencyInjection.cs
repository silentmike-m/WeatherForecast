namespace WeatherForecast.Infrastructure;

using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using WeatherForecast.Infrastructure.Coordinates;
using WeatherForecast.Infrastructure.MongoDb;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        services.AddCoordinates();

        services.AddMongoDb();

        return services;
    }
}
