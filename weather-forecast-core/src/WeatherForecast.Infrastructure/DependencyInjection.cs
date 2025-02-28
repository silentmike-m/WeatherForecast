namespace WeatherForecast.Infrastructure;

using Microsoft.Extensions.DependencyInjection;
using WeatherForecast.Application.Coordinates.Interfaces;
using WeatherForecast.Application.Coordinates.Models;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ICoordinatesValidationService, CoordinatesValidationService>();
        services.AddScoped<ICoordinatesRepository, CoordinatesRepository>();

        return services;
    }
}

internal sealed class CoordinatesRepository : ICoordinatesRepository
{
    public Task AddCoordinatesAsync(CoordinatesEntity entity, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    public Task DeleteCoordinatesAsync(Guid id, CancellationToken cancellationToken)
        => throw new NotImplementedException();
}

internal sealed class CoordinatesValidationService : ICoordinatesValidationService
{
    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
        => throw new NotImplementedException();

    public Task<bool> IsLatitudeAndLongitudeUniqueAsync(decimal latitude, decimal longitude, CancellationToken cancellationToken)
        => throw new NotImplementedException();
}
