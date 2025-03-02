namespace WeatherForecast.Infrastructure.MongoDb.Mappers.Interfaces;

using WeatherForecast.Application.Coordinates.Models;
using WeatherForecast.Infrastructure.Coordinates.Models;
using WeatherForecast.Infrastructure.MongoDb.Models;

internal interface ICoordinatesDbMapper
{
    CoordinatesDbModel ToDbModel(CoordinatesEntity coordinates);
    CoordinatesReadModel ToReadModel(CoordinatesDbModel coordinates);
}
