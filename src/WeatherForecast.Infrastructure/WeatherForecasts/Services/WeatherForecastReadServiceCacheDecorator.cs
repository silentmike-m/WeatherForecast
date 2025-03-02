namespace WeatherForecast.Infrastructure.WeatherForecasts.Services;

using Microsoft.Extensions.Options;
using WeatherForecast.Infrastructure.Cache;
using WeatherForecast.Infrastructure.Cache.Interfaces;
using WeatherForecast.Infrastructure.Cache.Models;
using WeatherForecast.Infrastructure.WeatherForecasts.Interfaces;
using WeatherForecast.Infrastructure.WeatherForecasts.Models;

internal sealed class WeatherForecastReadServiceCacheDecorator : IWeatherForecastReadService
{
    private readonly CacheOptions cacheOptions;
    private readonly ICacheService cacheService;
    private readonly IWeatherForecastReadService readService;

    public WeatherForecastReadServiceCacheDecorator(IOptions<CacheOptions> cacheOptions, ICacheService cacheService, IWeatherForecastReadService readService)
    {
        this.cacheOptions = cacheOptions.Value;
        this.cacheService = cacheService;
        this.readService = readService;
    }

    public async Task<WeatherForecastsReadModel?> GetWeatherForecastsAsync(decimal latitude, decimal longitude, CancellationToken cancellationToken)
    {
        var cacheKey = new WeatherForecastsKey
        {
            Latitude = latitude,
            Longitude = longitude,
        };

        var result = await this.cacheService.GetAsync(cacheKey, cancellationToken);

        if (result is null)
        {
            result = await this.readService.GetWeatherForecastsAsync(latitude, longitude, cancellationToken);

            if (result is not null)
            {
                await this.cacheService.SetAsync(cacheKey, result, this.cacheOptions.WeatherForecastsExpirationInMinutes, cancellationToken);
            }
        }

        return result;
    }
}
