namespace WeatherForecast.Infrastructure.WeatherForecasts.Mappers.Services;

using Riok.Mapperly.Abstractions;
using WeatherForecast.Application.WeatherForecasts.Dto;
using WeatherForecast.Infrastructure.WeatherForecasts.Mappers.Interfaces;
using WeatherForecast.Infrastructure.WeatherForecasts.Models;

[Mapper]
internal sealed partial class WeatherForecastMapper : IWeatherForecastMapper
{
    public partial WeatherForecastDto ToDto(WeatherForecastReadModel weatherForecast);
}
