namespace WeatherForecast.Infrastructure.UnitTests.Coordinates.Services;

using FluentAssertions;
using WeatherForecast.Infrastructure.Coordinates.Interfaces;
using WeatherForecast.Infrastructure.Coordinates.Models;
using WeatherForecast.Infrastructure.Coordinates.Services;

[TestClass]
public sealed class CoordinatesValidationServiceTests
{
    private Mock<ICoordinatesReadService> readServiceMock = null!;
    private CoordinatesValidationService validationService = null!;

    [TestMethod]
    public async Task ExistsAsync_Should_ReturnFalse_When_CoordinateDoesNotExist()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;

        var coordinateId = Guid.NewGuid();

        this.readServiceMock
            .Setup(service => service.GetCoordinatesAsync(coordinateId, cancellationToken))
            .ReturnsAsync((CoordinatesReadModel?)null);

        // Act
        var result = await this.validationService.ExistsAsync(coordinateId, cancellationToken);

        // Assert
        result.Should()
            .BeFalse();
    }

    [TestMethod]
    public async Task ExistsAsync_Should_ReturnTrue_When_CoordinateExists()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;

        var coordinates = new CoordinatesReadModel
        {
            Id = Guid.NewGuid(),
            Latitude = 12.34m,
            Longitude = 56.78m,
        };

        this.readServiceMock
            .Setup(service => service.GetCoordinatesAsync(coordinates.Id, cancellationToken))
            .ReturnsAsync(coordinates);

        // Act
        var result = await this.validationService.ExistsAsync(coordinates.Id, cancellationToken);

        // Assert
        result.Should().BeTrue();
    }

    [TestMethod]
    public async Task IsLatitudeAndLongitudeUniqueAsync_Should_ReturnFalse_When_CoordinatesAreNotUnique()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;

        var coordinates = new CoordinatesReadModel
        {
            Id = Guid.NewGuid(),
            Latitude = 12.34m,
            Longitude = 56.78m,
        };

        this.readServiceMock
            .Setup(service => service.GetCoordinatesAsync(coordinates.Latitude, coordinates.Longitude, cancellationToken))
            .ReturnsAsync(coordinates); // Simulates that the same latitude and longitude exist.

        // Act
        var result = await this.validationService.IsLatitudeAndLongitudeUniqueAsync(coordinates.Latitude, coordinates.Longitude, cancellationToken);

        // Assert
        result.Should()
            .BeFalse();
    }

    [TestMethod]
    public async Task IsLatitudeAndLongitudeUniqueAsync_Should_ReturnTrue_When_CoordinatesAreUnique()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;

        var latitude = 12.34m;
        var longitude = 56.78m;

        this.readServiceMock
            .Setup(service => service.GetCoordinatesAsync(latitude, longitude, cancellationToken))
            .ReturnsAsync((CoordinatesReadModel?)null);

        // Act
        var result = await this.validationService.IsLatitudeAndLongitudeUniqueAsync(latitude, longitude, CancellationToken.None);

        // Assert
        result.Should()
            .BeTrue();
    }

    [TestInitialize]
    public void Setup()
    {
        this.readServiceMock = new Mock<ICoordinatesReadService>();
        this.validationService = new CoordinatesValidationService(this.readServiceMock.Object);
    }
}
