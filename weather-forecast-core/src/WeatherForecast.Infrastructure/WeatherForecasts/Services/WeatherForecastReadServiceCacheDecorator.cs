namespace WeatherForecast.Infrastructure.WeatherForecasts.Services;

using WeatherForecast.Infrastructure.WeatherForecasts.Interfaces;
using WeatherForecast.Infrastructure.WeatherForecasts.Models;

internal sealed class WeatherForecastReadServiceCacheDecorator : IWeatherForecastReadService
{
    private readonly IWeatherForecastReadService readService;

    public WeatherForecastReadServiceCacheDecorator(IWeatherForecastReadService readService)
        => this.readService = readService;

    public async Task<WeatherForecastsReadModel?> GetWeatherForecastsAsync(decimal latitude, decimal longitude, CancellationToken cancellationToken)
        => await this.readService.GetWeatherForecastsAsync(latitude, longitude, cancellationToken);
}
