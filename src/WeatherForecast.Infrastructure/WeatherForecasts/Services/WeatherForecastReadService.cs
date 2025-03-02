namespace WeatherForecast.Infrastructure.WeatherForecasts.Services;

using WeatherForecast.Infrastructure.OpenMeteo.Interfaces;
using WeatherForecast.Infrastructure.WeatherForecasts.Extensions;
using WeatherForecast.Infrastructure.WeatherForecasts.Interfaces;
using WeatherForecast.Infrastructure.WeatherForecasts.Models;

internal sealed class WeatherForecastReadService : IWeatherForecastReadService
{
    private readonly IOpenMeteoClient openMeteoClient;

    public WeatherForecastReadService(IOpenMeteoClient openMeteoClient)
        => this.openMeteoClient = openMeteoClient;

    public async Task<WeatherForecastsReadModel?> GetWeatherForecastsAsync(decimal latitude, decimal longitude, CancellationToken cancellationToken)
    {
        var response = await this.openMeteoClient.GetWeatherForecastAsync(latitude, longitude, cancellationToken);

        var result = response.ToReadModel();

        return result;
    }
}
