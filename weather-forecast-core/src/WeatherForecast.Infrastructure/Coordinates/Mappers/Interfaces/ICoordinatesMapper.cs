namespace WeatherForecast.Infrastructure.Coordinates.Mappers.Interfaces;

using WeatherForecast.Application.Coordinates.Dto;
using WeatherForecast.Infrastructure.Coordinates.Models;

internal interface ICoordinatesMapper
{
    CoordinatesDto ToDto(CoordinatesReadModel coordinates);
}
