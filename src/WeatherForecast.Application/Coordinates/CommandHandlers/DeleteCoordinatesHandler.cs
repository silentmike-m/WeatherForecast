namespace WeatherForecast.Application.Coordinates.CommandHandlers;

using WeatherForecast.Application.Common.Extensions;
using WeatherForecast.Application.Coordinates.Commands;
using WeatherForecast.Application.Coordinates.Interfaces;

internal sealed class DeleteCoordinatesHandler : IRequestHandler<DeleteCoordinates>
{
    private readonly ILogger<DeleteCoordinatesHandler> logger;
    private readonly ICoordinatesRepository repository;

    public DeleteCoordinatesHandler(ILogger<DeleteCoordinatesHandler> logger, ICoordinatesRepository repository)
    {
        this.logger = logger;
        this.repository = repository;
    }

    public async Task Handle(DeleteCoordinates request, CancellationToken cancellationToken)
    {
        using var loggerScope = this.logger.BeginPropertyScope(
            ("Id", request.Id)
        );

        this.logger.LogInformation("Try to delete coordinates");

        await this.repository.DeleteCoordinatesAsync(request.Id, cancellationToken);
    }
}
