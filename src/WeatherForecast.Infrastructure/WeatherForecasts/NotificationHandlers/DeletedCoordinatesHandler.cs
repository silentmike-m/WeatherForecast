namespace WeatherForecast.Infrastructure.WeatherForecasts.NotificationHandlers;

using WeatherForecast.Application.Coordinates.Notification;
using WeatherForecast.Infrastructure.Cache.Interfaces;
using WeatherForecast.Infrastructure.Cache.Models;

internal sealed class DeletedCoordinatesHandler : INotificationHandler<DeletedCoordinates>
{
    private readonly ICacheService cacheService;

    public DeletedCoordinatesHandler(ICacheService cacheService)
        => this.cacheService = cacheService;

    public async Task Handle(DeletedCoordinates notification, CancellationToken cancellationToken)
    {
        var cacheKey = new WeatherForecastsKey
        {
            Latitude = notification.Latitude,
            Longitude = notification.Longitude,
        };

        await this.cacheService.ClearAsync(cacheKey, cancellationToken);
    }
}
