namespace WeatherForecast.Infrastructure.Coordinates.Mappers.Services;

using Riok.Mapperly.Abstractions;
using WeatherForecast.Application.Coordinates.Dto;
using WeatherForecast.Infrastructure.Coordinates.Mappers.Interfaces;
using WeatherForecast.Infrastructure.Coordinates.Models;

[Mapper]
internal sealed partial class CoordinatesMapper : ICoordinatesMapper
{
    public partial CoordinatesDto ToDto(CoordinatesReadModel coordinates);
}
