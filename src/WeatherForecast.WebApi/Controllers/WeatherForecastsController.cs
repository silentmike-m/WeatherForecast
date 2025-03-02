namespace WeatherForecast.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Application.WeatherForecasts.Queries;
using WeatherForecast.WebApi.Models;

[ApiController, Route("WeatherForecasts")]
public sealed class WeatherForecastsController : ApiController
{
    [HttpGet, Route("GetWeatherForecast")]
    public async Task<ActionResult<WeatherForecastGetModel>> GetWeatherForecast([FromQuery] Guid coordinatesId, CancellationToken cancellationToken)
    {
        var request = new GetWeatherForecast
        {
            CoordinatesId = coordinatesId,
        };

        var weatherForecast = await this.Mediator.Send(request, cancellationToken);

        if (weatherForecast is null)
        {
            return this.NotFound();
        }

        var result = new WeatherForecastGetModel
        {
            Days = weatherForecast.Days.Select(day => new DailyWeatherForecastGetModel
            {
                Date = day.Date,
                RainSum = new WeatherForecastValueGetModel
                {
                    Unit = day.RainSum.Unit,
                    Value = day.RainSum.Value,
                },
                ShowersSum = new WeatherForecastValueGetModel
                {
                    Unit = day.ShowersSum.Unit,
                    Value = day.ShowersSum.Value,
                },
                SnowfallSum = new WeatherForecastValueGetModel
                {
                    Unit = day.SnowfallSum.Unit,
                    Value = day.SnowfallSum.Value,
                },
                Temperature2mMax = new WeatherForecastValueGetModel
                {
                    Unit = day.Temperature2mMax.Unit,
                    Value = day.Temperature2mMax.Value,
                },
                Temperature2mMin = new WeatherForecastValueGetModel
                {
                    Unit = day.Temperature2mMin.Unit,
                    Value = day.Temperature2mMin.Value,
                },
                WeatherCode = day.WeatherCode,
                ApparentTemperatureMax = new WeatherForecastValueGetModel
                {
                    Unit = day.ApparentTemperatureMax.Unit,
                    Value = day.ApparentTemperatureMax.Value,
                },
                ApparentTemperatureMin = new WeatherForecastValueGetModel
                {
                    Unit = day.ApparentTemperatureMin.Unit,
                    Value = day.ApparentTemperatureMin.Value,
                },
            }).ToList(),
            Latitude = weatherForecast.Latitude,
            Longitude = weatherForecast.Longitude,
            Timezone = weatherForecast.Timezone,
        };

        return result;
    }
}
