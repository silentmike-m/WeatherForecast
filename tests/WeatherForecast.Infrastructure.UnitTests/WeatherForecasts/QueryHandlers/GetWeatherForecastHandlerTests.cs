namespace WeatherForecast.Infrastructure.UnitTests.WeatherForecasts.QueryHandlers;

using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using WeatherForecast.Application.Coordinates.Exceptions;
using WeatherForecast.Application.WeatherForecasts.Dto;
using WeatherForecast.Application.WeatherForecasts.Queries;
using WeatherForecast.Infrastructure.Coordinates.Interfaces;
using WeatherForecast.Infrastructure.Coordinates.Models;
using WeatherForecast.Infrastructure.WeatherForecasts.Interfaces;
using WeatherForecast.Infrastructure.WeatherForecasts.Mappers.Services;
using WeatherForecast.Infrastructure.WeatherForecasts.Models;
using WeatherForecast.Infrastructure.WeatherForecasts.QueryHandlers;

[TestClass]
public sealed class GetWeatherForecastHandlerTests
{
    private Mock<ICoordinatesReadService> coordinatesReadServiceMock;
    private GetWeatherForecastHandler handler;
    private NullLogger<GetWeatherForecastHandler> logger = new();
    private WeatherForecastMapper mapper = new();
    private Mock<IWeatherForecastReadService> weatherForecastReadServiceMock;

    [TestMethod]
    public async Task Handle_Should_ReturnNull_When_WeatherForecastNotFound()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;

        var coordinates = new CoordinatesReadModel
        {
            Id = Guid.NewGuid(),
            Latitude = 10,
            Longitude = 20,
        };

        var request = new GetWeatherForecast
        {
            CoordinatesId = coordinates.Id,
        };

        this.coordinatesReadServiceMock
            .Setup(service => service.GetCoordinatesAsync(request.CoordinatesId, cancellationToken))
            .ReturnsAsync(coordinates);

        this.weatherForecastReadServiceMock
            .Setup(service => service.GetWeatherForecastsAsync(It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((WeatherForecastReadModel?)null);

        // Act
        var result = await this.handler.Handle(request, cancellationToken);

        // Assert
        result.Should()
            .BeNull();
    }

    [TestMethod]
    public async Task Handle_Should_ReturnWeatherForecastDto_Whe_nWeatherForecastFound()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;

        var coordinates = new CoordinatesReadModel
        {
            Id = Guid.NewGuid(),
            Latitude = 10,
            Longitude = 20,
        };

        var request = new GetWeatherForecast
        {
            CoordinatesId = coordinates.Id,
        };

        var weatherForecast = new WeatherForecastReadModel
        {
            Days =
            [
                new DailyWeatherForecastReadModel()
                {
                    Date = new DateOnly(year: 2025, month: 3, day: 2),
                    RainSum = new WeatherForecastValueReadModel
                    {
                        Unit = "mm",
                        Value = 2.34m,
                    },
                    ShowersSum = new WeatherForecastValueReadModel
                    {
                        Unit = "mm",
                        Value = 2.33m,
                    },
                    SnowfallSum = new WeatherForecastValueReadModel
                    {
                        Unit = "mm",
                        Value = 22.34m,
                    },
                    Temperature2mMax = new WeatherForecastValueReadModel
                    {
                        Unit = "C",
                        Value = 5.5m,
                    },
                    Temperature2mMin = new WeatherForecastValueReadModel
                    {
                        Unit = "c",
                        Value = 2m,
                    },
                    WeatherCode = 2,
                    ApparentTemperatureMax = new WeatherForecastValueReadModel
                    {
                        Unit = "C",
                        Value = 5.5m,
                    },
                    ApparentTemperatureMin = new WeatherForecastValueReadModel
                    {
                        Unit = "c",
                        Value = 2m,
                    },
                },
            ],
            Latitude = coordinates.Latitude,
            Longitude = coordinates.Longitude,
            Timezone = "UTC",
        };

        this.coordinatesReadServiceMock
            .Setup(service => service.GetCoordinatesAsync(request.CoordinatesId, cancellationToken))
            .ReturnsAsync(coordinates);

        this.weatherForecastReadServiceMock
            .Setup(service => service.GetWeatherForecastsAsync(coordinates.Latitude, coordinates.Longitude, cancellationToken))
            .ReturnsAsync(weatherForecast);

        // Act
        var result = await this.handler.Handle(request, CancellationToken.None);

        // Assert
        var expectedResult = new WeatherForecastDto
        {
            Days =
            [
                new DailyWeatherForecastDto()
                {
                    Date = new DateOnly(year: 2025, month: 3, day: 2),
                    RainSum = new WeatherForecastValueDto
                    {
                        Unit = "mm",
                        Value = 2.34m,
                    },
                    ShowersSum = new WeatherForecastValueDto
                    {
                        Unit = "mm",
                        Value = 2.33m,
                    },
                    SnowfallSum = new WeatherForecastValueDto
                    {
                        Unit = "mm",
                        Value = 22.34m,
                    },
                    Temperature2mMax = new WeatherForecastValueDto
                    {
                        Unit = "C",
                        Value = 5.5m,
                    },
                    Temperature2mMin = new WeatherForecastValueDto
                    {
                        Unit = "c",
                        Value = 2m,
                    },
                    WeatherCode = 2,
                    ApparentTemperatureMax = new WeatherForecastValueDto
                    {
                        Unit = "C",
                        Value = 5.5m,
                    },
                    ApparentTemperatureMin = new WeatherForecastValueDto
                    {
                        Unit = "c",
                        Value = 2m,
                    },
                },
            ],
            Latitude = coordinates.Latitude,
            Longitude = coordinates.Longitude,
            Timezone = "UTC",
        };

        result.Should()
            .BeEquivalentTo(expectedResult);
    }

    [TestMethod]
    public async Task Handle_Should_ThrowCoordinatesNotFoundException_When_CoordinatesNotFound()
    {
        // Arrange
        var request = new GetWeatherForecast
        {
            CoordinatesId = Guid.NewGuid(),
        };

        this.coordinatesReadServiceMock
            .Setup(service => service.GetCoordinatesAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((CoordinatesReadModel?)null);

        // Act
        var action = async () => await this.handler.Handle(request, CancellationToken.None);

        // Assert
        await action.Should()
            .ThrowAsync<CoordinatesNotFoundException>();
    }

    [TestInitialize]
    public void TestInitialize()
    {
        this.coordinatesReadServiceMock = new Mock<ICoordinatesReadService>();
        this.weatherForecastReadServiceMock = new Mock<IWeatherForecastReadService>();

        this.handler = new GetWeatherForecastHandler(this.coordinatesReadServiceMock.Object, this.logger, this.mapper, this.weatherForecastReadServiceMock.Object
        );
    }
}
