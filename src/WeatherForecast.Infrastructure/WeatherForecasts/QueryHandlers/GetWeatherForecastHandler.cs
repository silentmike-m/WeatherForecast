namespace WeatherForecast.Infrastructure.WeatherForecasts.QueryHandlers;

using WeatherForecast.Application.Common.Extensions;
using WeatherForecast.Application.Coordinates.Exceptions;
using WeatherForecast.Application.WeatherForecasts.Dto;
using WeatherForecast.Application.WeatherForecasts.Queries;
using WeatherForecast.Infrastructure.Coordinates.Interfaces;
using WeatherForecast.Infrastructure.WeatherForecasts.Interfaces;
using WeatherForecast.Infrastructure.WeatherForecasts.Mappers.Interfaces;

internal sealed class GetWeatherForecastHandler : IRequestHandler<GetWeatherForecast, WeatherForecastDto?>
{
    private readonly ICoordinatesReadService coordinatesReadService;
    private readonly ILogger<GetWeatherForecastHandler> logger;
    private readonly IWeatherForecastMapper mapper;
    private readonly IWeatherForecastReadService weatherForecastReadService;

    public GetWeatherForecastHandler(ICoordinatesReadService coordinatesReadService, ILogger<GetWeatherForecastHandler> logger, IWeatherForecastMapper mapper, IWeatherForecastReadService weatherForecastReadService)
    {
        this.coordinatesReadService = coordinatesReadService;
        this.logger = logger;
        this.mapper = mapper;
        this.weatherForecastReadService = weatherForecastReadService;
    }

    public async Task<WeatherForecastDto?> Handle(GetWeatherForecast request, CancellationToken cancellationToken)
    {
        using var loggerScope = this.logger.BeginPropertyScope(
            ("CoordinatesId", request.CoordinatesId)
        );

        this.logger.LogInformation("Try to get weather forecast");

        var coordinates = await this.coordinatesReadService.GetCoordinatesAsync(request.CoordinatesId, cancellationToken);

        if (coordinates is null)
        {
            throw new CoordinatesNotFoundException(request.CoordinatesId);
        }

        var weatherForecast = await this.weatherForecastReadService.GetWeatherForecastsAsync(coordinates.Latitude, coordinates.Longitude, cancellationToken);

        var result = weatherForecast is null
            ? null
            : this.mapper.ToDto(weatherForecast);

        return result;
    }
}
