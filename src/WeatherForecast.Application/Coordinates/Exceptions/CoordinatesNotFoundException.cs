namespace WeatherForecast.Application.Coordinates.Exceptions;

using WeatherForecast.Application.Common;
using WeatherForecast.Application.Coordinates.Constants;

public sealed class CoordinatesNotFoundException : ApplicationException
{
    public override string Code => ErrorCodes.COORDINATES_NOT_FOUND;

    public CoordinatesNotFoundException(Guid id, Exception? innerException = null)
        : base($"Coordinates with id {id} has not been found", innerException)
    {
        this.Id = id;
    }
}
