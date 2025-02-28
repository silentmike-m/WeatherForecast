namespace WeatherForecast.Application.Coordinates.CommandHandlers;

using WeatherForecast.Application.Common.Extensions;
using WeatherForecast.Application.Common.Interfaces;
using WeatherForecast.Application.Coordinates.Commands;
using WeatherForecast.Application.Coordinates.Interfaces;
using WeatherForecast.Application.Coordinates.Models;

internal sealed class AddCoordinatesHandler : IRequestHandler<AddCoordinates>
{
    private readonly IGuidService guidService;
    private readonly ILogger<AddCoordinatesHandler> logger;
    private readonly ICoordinatesRepository repository;

    public AddCoordinatesHandler(IGuidService guidService, ILogger<AddCoordinatesHandler> logger, ICoordinatesRepository repository)
    {
        this.guidService = guidService;
        this.logger = logger;
        this.repository = repository;
    }

    public async Task Handle(AddCoordinates request, CancellationToken cancellationToken)
    {
        using var loggerScope = this.logger.BeginPropertyScope(
            ("Latitude", request.Latitude),
            ("Longitude", request.Longitude)
        );

        this.logger.LogInformation("Try to add coordinates");

        var id = this.guidService.NewGuid();

        var entity = new CoordinatesEntity(id, request.Latitude, request.Longitude);

        await this.repository.AddCoordinatesAsync(entity, cancellationToken);
    }
}
