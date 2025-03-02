namespace WeatherForecast.Infrastructure.OpenMeteo.Services;

using System.Net.Http.Json;
using global::WeatherForecast.Infrastructure.OpenMeteo.Interfaces;
using global::WeatherForecast.Infrastructure.OpenMeteo.Models;

internal sealed class OpenMeteoClient : IOpenMeteoClient
{
    private const string GET_WEATHER_FORECAST_ENDPOINT_TEMPLATE = "forecast?latitude={0}&longitude={1}&daily=weather_code,temperature_2m_max,temperature_2m_min,apparent_temperature_max,apparent_temperature_min,rain_sum,showers_sum,snowfall_sum&forecast_days={2}";

    private readonly IHttpClientFactory httpClientFactory;
    private readonly OpenMeteoOptions options;

    public OpenMeteoClient(IHttpClientFactory httpClientFactory, OpenMeteoOptions options)
    {
        this.httpClientFactory = httpClientFactory;
        this.options = options;
    }

    public async Task<GetWeatherForecastResponse?> GetWeatherForecastAsync(decimal latitude, decimal longitude, CancellationToken cancellationToken)
    {
        var endpoint = string.Format(GET_WEATHER_FORECAST_ENDPOINT_TEMPLATE, latitude, longitude, this.options.ForecastDays);

        using var httpClient = this.httpClientFactory.CreateClient(OpenMeteoOptions.HTTP_CLIENT_NAME);

        //TODO: response validation
        var response = await httpClient.GetFromJsonAsync<GetWeatherForecastResponse>(endpoint, cancellationToken);

        return response;
    }
}
