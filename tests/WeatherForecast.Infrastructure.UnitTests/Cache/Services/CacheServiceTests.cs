namespace WeatherForecast.Infrastructure.UnitTests.Cache.Services;

using FluentAssertions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging.Abstractions;
using WeatherForecast.Infrastructure.Cache.Models;
using WeatherForecast.Infrastructure.Cache.Services;
using WeatherForecast.Infrastructure.WeatherForecasts.Models;

[TestClass]
public sealed class CacheServiceTests
{
    private Mock<IDistributedCache> cacheMock = null!;
    private CacheService cacheService = null!;
    private NullLogger<CacheService> logger = null!;

    [TestMethod]
    public async Task ClearAsync_DoesNotThrow_When_CacheErrorOccurs()
    {
        // Arrange
        this.cacheMock
            .Setup(c => c.RemoveAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Cache clear error"));

        var cacheKey = new WeatherForecastsKey
        {
            Latitude = 12.34m,
            Longitude = 56.78m,
        };

        // Act
        var action = async () => await this.cacheService.ClearAsync(cacheKey);

        // Assert
        await action.Should()
            .NotThrowAsync();
    }

    [TestMethod]
    public async Task GetAsync_ReturnsDefault_WhenCacheErrorOccurs()
    {
        // Arrange
        var cacheKey = new WeatherForecastsKey
        {
            Latitude = 12.34m,
            Longitude = 56.78m,
        };

        this.cacheMock
            .Setup(c => c.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Cache get error"));

        // Act
        var result = await this.cacheService.GetAsync(cacheKey);

        // Assert
        result.Should()
            .BeNull();
    }

    [TestMethod]
    public async Task SetAsync_DoesNotThrow_WhenCacheErrorOccurs()
    {
        // Arrange
        this.cacheMock
            .Setup(c => c.SetAsync(It.IsAny<string>(), It.IsAny<byte[]>(), It.IsAny<DistributedCacheEntryOptions>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Cache set error"));

        var cacheKey = new WeatherForecastsKey
        {
            Latitude = 12.34m,
            Longitude = 56.78m,
        };

        var readModel = new WeatherForecastsReadModel
        {
            Days = [],
            Latitude = 12.34m,
            Longitude = 56.78m,
            Timezone = "UTC",
        };

        // Act
        var action = async () => await this.cacheService.SetAsync(cacheKey, readModel, keyTimeoutInMinutes: 5);

        // Assert
        await action.Should()
            .NotThrowAsync();
    }

    [TestInitialize]
    public void Setup()
    {
        this.cacheMock = new Mock<IDistributedCache>();
        this.logger = new NullLogger<CacheService>();
        this.cacheService = new CacheService(this.cacheMock.Object, this.logger);
    }
}
