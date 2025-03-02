namespace WeatherForecast.Infrastructure.MongoDb.Exceptions;

using WeatherForecast.Application.Common;
using WeatherForecast.Infrastructure.MongoDb.Constants;

public sealed class MongoDbConnectionException : ApplicationException
{
    public override string Code => ErrorCodes.MONGO_DB_CONNECTION;

    public MongoDbConnectionException(Exception? innerException = null) :
        base("There was problem with mongo db connection", innerException)
    {
    }
}
