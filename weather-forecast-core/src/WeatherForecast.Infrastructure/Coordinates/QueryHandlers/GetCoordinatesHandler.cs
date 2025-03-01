namespace WeatherForecast.Infrastructure.Coordinates.QueryHandlers;

using WeatherForecast.Application.Coordinates.Dto;
using WeatherForecast.Application.Coordinates.Queries;
using WeatherForecast.Infrastructure.Coordinates.Interfaces;
using WeatherForecast.Infrastructure.Coordinates.Mappers.Interfaces;

internal sealed class GetCoordinatesHandler : IRequestHandler<GetCoordinates, IReadOnlyList<CoordinatesDto>>
{
    private readonly ICoordinatesMapper coordinatesMapper;
    private readonly ILogger<GetCoordinatesHandler> logger;
    private readonly ICoordinatesReadService readService;

    public GetCoordinatesHandler(ICoordinatesMapper coordinatesMapper, ILogger<GetCoordinatesHandler> logger, ICoordinatesReadService readService)
    {
        this.coordinatesMapper = coordinatesMapper;
        this.logger = logger;
        this.readService = readService;
    }

    public async Task<IReadOnlyList<CoordinatesDto>> Handle(GetCoordinates request, CancellationToken cancellationToken)
    {
        this.logger.LogInformation("Try to get all coordinates");

        var coordinates = await this.readService.GetCoordinatesAsync(cancellationToken);

        var result = coordinates
            .Select(this.coordinatesMapper.ToDto)
            .ToList();

        return result;
    }
}
