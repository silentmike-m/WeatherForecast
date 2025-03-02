namespace WeatherForecast.Infrastructure.UnitTests.WeatherForecasts.Services;

using FluentAssertions;
using WeatherForecast.Infrastructure.OpenMeteo.Interfaces;
using WeatherForecast.Infrastructure.OpenMeteo.Models;
using WeatherForecast.Infrastructure.WeatherForecasts.Models;
using WeatherForecast.Infrastructure.WeatherForecasts.Services;

[TestClass]
public sealed class WeatherForecastReadServiceTests
{
    private readonly Mock<IOpenMeteoClient> openMeteoClientMock = new();
    private readonly WeatherForecastReadService readService;

    public WeatherForecastReadServiceTests()
        => this.readService = new WeatherForecastReadService(this.openMeteoClientMock.Object);

    [TestMethod]
    public async Task GetWeatherForecastsAsync_ShouldReturnNull_WhenNoDataIsRetrieved()
    {
        // Arrange
        const decimal latitude = 1.0m;
        const decimal longitude = 2.0m;
        var cancellationToken = CancellationToken.None;

        this.openMeteoClientMock
            .Setup(m => m.GetWeatherForecastAsync(latitude, longitude, cancellationToken))
            .ReturnsAsync((GetWeatherForecastResponse?)null);

        // Act
        var result = await this.readService.GetWeatherForecastsAsync(latitude, longitude, cancellationToken);

        // Assert
        result.Should()
            .BeNull();
    }

    [TestMethod]
    public async Task GetWeatherForecastsAsync_ShouldReturnReadModel_WhenDataIsRetrieved()
    {
        // Arrange
        const decimal latitude = 1.0m;
        const decimal longitude = 2.0m;
        var cancellationToken = CancellationToken.None;

        var weatherForecastResponse = new GetWeatherForecastResponse
        {
            Latitude = latitude,
            Longitude = longitude,
            Timezone = "UTC",
        };

        this.openMeteoClientMock
            .Setup(m => m.GetWeatherForecastAsync(latitude, longitude, cancellationToken))
            .ReturnsAsync(weatherForecastResponse);

        // Act
        var result = await this.readService.GetWeatherForecastsAsync(latitude, longitude, cancellationToken);

        // Assert
        var expectedResult = new WeatherForecastsReadModel
        {
            Days = [],
            Latitude = latitude,
            Longitude = longitude,
            Timezone = "UTC",
        };

        result.Should()
            .BeEquivalentTo(expectedResult);
    }
}
