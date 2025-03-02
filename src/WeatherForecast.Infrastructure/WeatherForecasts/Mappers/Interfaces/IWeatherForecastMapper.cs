namespace WeatherForecast.Infrastructure.WeatherForecasts.Mappers.Interfaces;

using WeatherForecast.Application.WeatherForecasts.Dto;
using WeatherForecast.Infrastructure.WeatherForecasts.Models;

internal interface IWeatherForecastMapper
{
    WeatherForecastDto ToDto(WeatherForecastReadModel weatherForecast);
}
