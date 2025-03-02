namespace WeatherForecast.Application.Coordinates.Queries;

using WeatherForecast.Application.Coordinates.Dto;

public sealed record GetCoordinates : IRequest<IReadOnlyList<CoordinatesDto>>;
