namespace WeatherForecast.Infrastructure.UnitTests.WeatherForecasts.Services;

using FluentAssertions;
using Microsoft.Extensions.Options;
using WeatherForecast.Infrastructure.Cache;
using WeatherForecast.Infrastructure.Cache.Interfaces;
using WeatherForecast.Infrastructure.Cache.Models;
using WeatherForecast.Infrastructure.WeatherForecasts.Interfaces;
using WeatherForecast.Infrastructure.WeatherForecasts.Models;
using WeatherForecast.Infrastructure.WeatherForecasts.Services;

[TestClass]
public sealed class WeatherForecastReadServiceCacheDecoratorTests
{
    private CacheOptions cacheOptions = null!;
    private Mock<ICacheService> cacheServiceMock = null!;
    private WeatherForecastReadServiceCacheDecorator decorator = null!;
    private Mock<IWeatherForecastReadService> readServiceMock = null!;

    [TestMethod]
    public async Task GetWeatherForecastsAsync_Should_CallReadService_When_CacheIsEmpty()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;

        const decimal latitude = 10.45m;
        const decimal longitude = 20.23m;

        var key = new WeatherForecastsKey
        {
            Latitude = latitude,
            Longitude = longitude,
        };

        var readModel = new WeatherForecastReadModel
        {
            Days = [],
            Latitude = latitude,
            Longitude = longitude,
            Timezone = "UTC",
        };

        this.cacheServiceMock
            .Setup(service => service.GetAsync(It.Is<WeatherForecastsKey>(k => k.Equals(key)), cancellationToken))
            .ReturnsAsync((WeatherForecastReadModel?)null);

        this.readServiceMock
            .Setup(service => service.GetWeatherForecastsAsync(latitude, longitude, cancellationToken))
            .ReturnsAsync(readModel);

        // Act
        var result = await this.decorator.GetWeatherForecastsAsync(latitude, longitude, cancellationToken);

        // Assert
        result.Should()
            .Be(readModel);

        this.cacheServiceMock
            .Verify(service => service.SetAsync(It.Is<WeatherForecastsKey>(k => k.Equals(key)), readModel, this.cacheOptions.WeatherForecastsExpirationInMinutes, It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestMethod]
    public async Task GetWeatherForecastsAsync_Should_ReturnDataFromCache_When_CacheIsAvailable()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;

        const decimal latitude = 10.45m;
        const decimal longitude = 20.23m;

        var key = new WeatherForecastsKey
        {
            Latitude = latitude,
            Longitude = longitude,
        };

        var readModel = new WeatherForecastReadModel
        {
            Days = [],
            Latitude = latitude,
            Longitude = longitude,
            Timezone = "UTC",
        };

        this.cacheServiceMock
            .Setup(c => c.GetAsync(It.Is<WeatherForecastsKey>(k => k.Equals(key)), cancellationToken))
            .ReturnsAsync(readModel);

        // Act
        var result = await this.decorator.GetWeatherForecastsAsync(latitude, longitude, cancellationToken);

        // Assert
        result.Should()
            .Be(readModel);

        this.readServiceMock
            .Verify(service => service.GetWeatherForecastsAsync(It.IsAny<decimal>(), It.IsAny<decimal>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [TestInitialize]
    public void Initialize()
    {
        this.readServiceMock = new Mock<IWeatherForecastReadService>();
        this.cacheServiceMock = new Mock<ICacheService>();

        this.cacheOptions = new CacheOptions
        {
            WeatherForecastsExpirationInMinutes = 60,
        };

        this.decorator = new WeatherForecastReadServiceCacheDecorator(Options.Create(this.cacheOptions), this.cacheServiceMock.Object, this.readServiceMock.Object);
    }
}
