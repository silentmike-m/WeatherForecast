namespace WeatherForecast.Application.Common.Services;

using WeatherForecast.Application.Common.Interfaces;

internal sealed class GuidService : IGuidService
{
    public Guid NewGuid()
        => Guid.NewGuid();
}
