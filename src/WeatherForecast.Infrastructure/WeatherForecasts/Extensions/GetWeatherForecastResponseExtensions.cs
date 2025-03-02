namespace WeatherForecast.Infrastructure.WeatherForecasts.Extensions;

using WeatherForecast.Infrastructure.OpenMeteo.Models;
using WeatherForecast.Infrastructure.WeatherForecasts.Models;

internal static class GetWeatherForecastResponseExtensions
{
    public static WeatherForecastReadModel? ToReadModel(this GetWeatherForecastResponse? response)
    {
        if (response is null)
        {
            return null;
        }

        var days = new List<DailyWeatherForecastReadModel>();

        for (var index = 0; index < response.Daily.Days.Count; index++)
        {
            var date = response.Daily.Days[index];

            var day = new DailyWeatherForecastReadModel
            {
                Date = date,
                RainSum = MapWeatherForecastValueReadModel(index, response.DailyUnits.RainSum, response.Daily.RainSum),
                ShowersSum = MapWeatherForecastValueReadModel(index, response.DailyUnits.ShowersSum, response.Daily.ShowersSum),
                SnowfallSum = MapWeatherForecastValueReadModel(index, response.DailyUnits.SnowfallSum, response.Daily.SnowfallSum),
                Temperature2mMax = MapWeatherForecastValueReadModel(index, response.DailyUnits.Temperature2mMax, response.Daily.Temperature2mMax),
                Temperature2mMin = MapWeatherForecastValueReadModel(index, response.DailyUnits.Temperature2mMin, response.Daily.Temperature2mMin),
                WeatherCode = index < response.Daily.WeatherCodes.Count
                    ? response.Daily.WeatherCodes[index]
                    : null,
                ApparentTemperatureMax = MapWeatherForecastValueReadModel(index, response.DailyUnits.ApparentTemperatureMax, response.Daily.ApparentTemperatureMax),
                ApparentTemperatureMin = MapWeatherForecastValueReadModel(index, response.DailyUnits.ApparentTemperatureMin, response.Daily.ApparentTemperatureMin),
            };

            days.Add(day);
        }

        var result = new WeatherForecastReadModel
        {
            Days = days,
            Latitude = response.Latitude,
            Longitude = response.Longitude,
            Timezone = response.Timezone,
        };

        return result;
    }

    private static WeatherForecastValueReadModel MapWeatherForecastValueReadModel(int index, string unit, IReadOnlyList<decimal> values)
        => new()
        {
            Unit = unit,
            Value = index < values.Count
                ? values[index]
                : null,
        };
}
