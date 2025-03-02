namespace WeatherForecast.Application.Coordinates.CommandHandlers;

using WeatherForecast.Application.Common.Extensions;
using WeatherForecast.Application.Coordinates.Commands;
using WeatherForecast.Application.Coordinates.Interfaces;
using WeatherForecast.Application.Coordinates.Notification;

internal sealed class DeleteCoordinatesHandler : IRequestHandler<DeleteCoordinates>
{
    private readonly ILogger<DeleteCoordinatesHandler> logger;
    private readonly IPublisher mediator;
    private readonly ICoordinatesRepository repository;

    public DeleteCoordinatesHandler(ILogger<DeleteCoordinatesHandler> logger, IPublisher mediator, ICoordinatesRepository repository)
    {
        this.logger = logger;
        this.mediator = mediator;
        this.repository = repository;
    }

    public async Task Handle(DeleteCoordinates request, CancellationToken cancellationToken)
    {
        using var loggerScope = this.logger.BeginPropertyScope(
            ("Id", request.Id)
        );

        this.logger.LogInformation("Try to delete coordinates");

        var coordinates = await this.repository.GetCoordinatesAsync(request.Id, cancellationToken);

        if (coordinates is null)
        {
            return;
        }

        await this.repository.DeleteCoordinatesAsync(coordinates, cancellationToken);

        var notification = new DeletedCoordinates
        {
            Latitude = coordinates.Latitude,
            Longitude = coordinates.Longitude,
        };

        await this.mediator.Publish(notification, cancellationToken);
    }
}
