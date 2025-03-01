namespace WeatherForecast.Infrastructure.MongoDb.Mappers.Services;

using Riok.Mapperly.Abstractions;
using WeatherForecast.Application.Coordinates.Models;
using WeatherForecast.Infrastructure.Coordinates.Models;
using WeatherForecast.Infrastructure.MongoDb.Mappers.Interfaces;
using WeatherForecast.Infrastructure.MongoDb.Models;

[Mapper]
internal sealed partial class CoordinatesDbMapper : ICoordinatesDbMapper
{
    public partial CoordinatesDbModel ToDbModel(CoordinatesEntity coordinates);
    public partial CoordinatesReadModel ToReadModel(CoordinatesDbModel coordinates);
}
