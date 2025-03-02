namespace WeatherForecast.Infrastructure.UnitTests.WeatherForecasts.Extensions;

using FluentAssertions;
using WeatherForecast.Infrastructure.OpenMeteo.Models;
using WeatherForecast.Infrastructure.WeatherForecasts.Extensions;
using WeatherForecast.Infrastructure.WeatherForecasts.Models;

[TestClass]
public sealed class GetWeatherForecastResponseExtensionsTests
{
    [TestMethod]
    public void Should_MapResponse_To_ReadModel()
    {
        // Arrange
        var date = new DateOnly(year: 2025, month: 03, day: 02);
        var nullDate = new DateOnly(year: 2025, month: 03, day: 01);

        const decimal dateApparentTemperatureMax = 28.5m;
        const decimal dateApparentTemperatureMin = 17.5m;
        const decimal dateRainSum = 5.5m;
        const decimal dateShowersSum = 24.54m;
        const decimal dateSnowfallSum = 0.5m;
        const decimal dateTemperature2mMax = 25.5m;
        const decimal dateTemperature2mMin = 15.5m;
        const int dateWeatherCode = 2;

        var response = new GetWeatherForecastResponse
        {
            Latitude = 52.5200m,
            Longitude = 13.4050m,
            Timezone = "Europe/Berlin",
            Daily = new DailyResponse
            {
                Days = new List<DateOnly>
                {
                    date,
                    nullDate,
                },
                RainSum = new List<decimal>
                {
                    dateRainSum,
                },
                ShowersSum = new List<decimal>
                {
                    dateShowersSum,
                },
                SnowfallSum = new List<decimal>
                {
                    dateSnowfallSum,
                },
                Temperature2mMax = new List<decimal>
                {
                    dateTemperature2mMax,
                },
                Temperature2mMin = new List<decimal>
                {
                    dateTemperature2mMin,
                },
                WeatherCodes = new List<int>
                {
                    dateWeatherCode,
                },
                ApparentTemperatureMax = new List<decimal>
                {
                    dateApparentTemperatureMax,
                },
                ApparentTemperatureMin = new List<decimal>
                {
                    dateApparentTemperatureMin,
                },
            },
            DailyUnits = new DailyUnitsResponse
            {
                RainSum = "mm",
                ShowersSum = "mm",
                SnowfallSum = "cm",
                Temperature2mMax = "°C",
                Temperature2mMin = "°C",
                ApparentTemperatureMax = "°C",
                ApparentTemperatureMin = "°C",
            },
        };

        // Act
        var result = response.ToReadModel();

        // Assert
        var expectedResult = new WeatherForecastsReadModel
        {
            Days =
            [
                new DailyWeatherForecastReadModel
                {
                    Date = date,
                    RainSum = new WeatherForecastValueReadModel
                    {
                        Unit = response.DailyUnits.RainSum,
                        Value = dateRainSum,
                    },
                    ShowersSum = new WeatherForecastValueReadModel
                    {
                        Unit = response.DailyUnits.ShowersSum,
                        Value = dateShowersSum,
                    },
                    SnowfallSum = new WeatherForecastValueReadModel
                    {
                        Unit = response.DailyUnits.SnowfallSum,
                        Value = dateSnowfallSum,
                    },
                    Temperature2mMax = new WeatherForecastValueReadModel
                    {
                        Unit = response.DailyUnits.Temperature2mMax,
                        Value = dateTemperature2mMax,
                    },
                    Temperature2mMin = new WeatherForecastValueReadModel
                    {
                        Unit = response.DailyUnits.Temperature2mMin,
                        Value = dateTemperature2mMin,
                    },
                    WeatherCode = dateWeatherCode,
                    ApparentTemperatureMax = new WeatherForecastValueReadModel
                    {
                        Unit = response.DailyUnits.ApparentTemperatureMax,
                        Value = dateApparentTemperatureMax,
                    },
                    ApparentTemperatureMin = new WeatherForecastValueReadModel
                    {
                        Unit = response.DailyUnits.ApparentTemperatureMin,
                        Value = dateApparentTemperatureMin,
                    },
                },
                new DailyWeatherForecastReadModel
                {
                    Date = nullDate,
                    RainSum = new WeatherForecastValueReadModel
                    {
                        Unit = response.DailyUnits.RainSum,
                        Value = null,
                    },
                    ShowersSum = new WeatherForecastValueReadModel
                    {
                        Unit = response.DailyUnits.ShowersSum,
                        Value = null,
                    },
                    SnowfallSum = new WeatherForecastValueReadModel
                    {
                        Unit = response.DailyUnits.SnowfallSum,
                        Value = null,
                    },
                    Temperature2mMax = new WeatherForecastValueReadModel
                    {
                        Unit = response.DailyUnits.Temperature2mMax,
                        Value = null,
                    },
                    Temperature2mMin = new WeatherForecastValueReadModel
                    {
                        Unit = response.DailyUnits.Temperature2mMin,
                        Value = null,
                    },
                    WeatherCode = null,
                    ApparentTemperatureMax = new WeatherForecastValueReadModel
                    {
                        Unit = response.DailyUnits.ApparentTemperatureMax,
                        Value = null,
                    },
                    ApparentTemperatureMin = new WeatherForecastValueReadModel
                    {
                        Unit = response.DailyUnits.ApparentTemperatureMin,
                        Value = null,
                    },
                },
            ],
            Latitude = response.Latitude,
            Longitude = response.Longitude,
            Timezone = response.Timezone,
        };

        result.Should()
            .BeEquivalentTo(expectedResult);
    }

    [TestMethod]
    public void Should_ReturnNull_If_ResponseIsNull()
    {
        // Arrange
        GetWeatherForecastResponse? response = null;

        // Act
        var result = response.ToReadModel();

        // Assert
        result.Should()
            .BeNull();
    }
}
