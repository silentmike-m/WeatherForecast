namespace WeatherForecast.Infrastructure.OpenMeteo.Exceptions;

using WeatherForecast.Application.Common;
using WeatherForecast.Infrastructure.OpenMeteo.Constants;

public sealed class OpenMeteoConnectionException : ApplicationException
{
    public override string Code => ErrorCodes.OPEN_METEO_CONNECTION;

    public OpenMeteoConnectionException(Exception? innerException = null)
        : base("There was problem with open meteo connection", innerException)
    {
    }
}
