﻿namespace WeatherForecast.Application.Common.Extensions;

public static class LoggerExtensions
{
    public static IDisposable BeginPropertyScope(this ILogger logger, params ValueTuple<string, object?>[] properties)
    {
        var dictionary = properties.ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);

        return logger.BeginScope(dictionary)!;
    }
}
