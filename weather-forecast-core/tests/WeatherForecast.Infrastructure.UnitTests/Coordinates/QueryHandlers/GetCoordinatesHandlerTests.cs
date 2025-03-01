namespace WeatherForecast.Infrastructure.UnitTests.Coordinates.QueryHandlers;

using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using WeatherForecast.Application.Coordinates.Dto;
using WeatherForecast.Application.Coordinates.Queries;
using WeatherForecast.Infrastructure.Coordinates.Interfaces;
using WeatherForecast.Infrastructure.Coordinates.Mappers.Services;
using WeatherForecast.Infrastructure.Coordinates.Models;
using WeatherForecast.Infrastructure.Coordinates.QueryHandlers;

[TestClass]
public sealed class GetCoordinatesHandlerTests
{
    private CoordinatesMapper coordinatesMapper = null!;
    private GetCoordinatesHandler handler = null!;
    private NullLogger<GetCoordinatesHandler> logger = null!;
    private Mock<ICoordinatesReadService> readServiceMock = null!;

    [TestMethod]
    public async Task Handle_Should_GetAllCoordinates()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;

        var coordinate1 = new CoordinatesReadModel
        {
            Id = Guid.NewGuid(),
            Latitude = 12.34m,
            Longitude = 56.78m,
        };

        var coordinate2 = new CoordinatesReadModel
        {
            Id = Guid.NewGuid(),
            Latitude = 11.24m,
            Longitude = 54.28m,
        };

        var coordinates = new List<CoordinatesReadModel>
        {
            coordinate1,
            coordinate2,
        };

        var request = new GetCoordinates();

        this.readServiceMock
            .Setup(service => service.GetCoordinatesAsync(cancellationToken))
            .ReturnsAsync(coordinates);

        // Act
        var result = await this.handler.Handle(request, cancellationToken);

        // Assert
        var expectedResult = new List<CoordinatesDto>
        {
            new()
            {
                Id = coordinate1.Id,
                Latitude = coordinate1.Latitude,
                Longitude = coordinate1.Longitude,
            },
            new()
            {
                Id = coordinate2.Id,
                Latitude = coordinate2.Latitude,
                Longitude = coordinate2.Longitude,
            },
        };

        result.Should()
            .BeEquivalentTo(expectedResult);
    }

    [TestInitialize]
    public void Initialize()
    {
        this.coordinatesMapper = new CoordinatesMapper();
        this.logger = new NullLogger<GetCoordinatesHandler>();
        this.readServiceMock = new Mock<ICoordinatesReadService>();

        this.handler = new GetCoordinatesHandler(this.coordinatesMapper, this.logger, this.readServiceMock.Object);
    }
}
