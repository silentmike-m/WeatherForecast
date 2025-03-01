namespace WeatherForecast.Infrastructure.Coordinates.Services;

using WeatherForecast.Application.Coordinates.Interfaces;
using WeatherForecast.Infrastructure.Coordinates.Interfaces;

internal sealed class CoordinatesValidationService : ICoordinatesValidationService
{
    private readonly ICoordinatesReadService readService;

    public CoordinatesValidationService(ICoordinatesReadService readService)
        => this.readService = readService;

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        var coordinates = await this.readService.GetCoordinatesAsync(id, cancellationToken);

        return coordinates != null;
    }

    public async Task<bool> IsLatitudeAndLongitudeUniqueAsync(decimal latitude, decimal longitude, CancellationToken cancellationToken)
    {
        var coordinates = await this.readService.GetCoordinatesAsync(latitude, longitude, cancellationToken);

        return coordinates == null;
    }
}
