namespace WeatherForecast.Infrastructure.UnitTests.WeatherForecasts.NotificationHandlers;

using WeatherForecast.Application.Coordinates.Notification;
using WeatherForecast.Infrastructure.Cache.Interfaces;
using WeatherForecast.Infrastructure.Cache.Models;
using WeatherForecast.Infrastructure.WeatherForecasts.NotificationHandlers;

[TestClass]
public sealed class DeletedCoordinatesHandlerTests
{
    private Mock<ICacheService> cacheServiceMock = new();
    private DeletedCoordinatesHandler handler;

    public DeletedCoordinatesHandlerTests()
        => this.handler = new DeletedCoordinatesHandler(this.cacheServiceMock.Object);

    [TestMethod]
    public async Task Handle_ShouldClearCache_WithCorrectCacheKey()
    {
        // Arrange
        var notification = new DeletedCoordinates
        {
            Latitude = 10.4m,
            Longitude = 20.1m,
        };

        // Act
        await this.handler.Handle(notification, CancellationToken.None);

        // Assert
        var expectedCacheKey = new WeatherForecastsKey
        {
            Latitude = notification.Latitude,
            Longitude = notification.Longitude,
        };

        this.cacheServiceMock.Verify(x => x.ClearAsync(It.Is<WeatherForecastsKey>(key => key.Equals(expectedCacheKey)), CancellationToken.None), Times.Once);
    }
}
