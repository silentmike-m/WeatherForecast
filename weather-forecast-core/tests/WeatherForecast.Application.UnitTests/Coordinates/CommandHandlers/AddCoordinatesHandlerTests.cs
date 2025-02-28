namespace WeatherForecast.Application.UnitTests.Coordinates.CommandHandlers;

using FluentAssertions;
using Microsoft.Extensions.Logging.Abstractions;
using WeatherForecast.Application.Common.Interfaces;
using WeatherForecast.Application.Coordinates.CommandHandlers;
using WeatherForecast.Application.Coordinates.Commands;
using WeatherForecast.Application.Coordinates.Interfaces;
using WeatherForecast.Application.Coordinates.Models;

[TestClass]
public sealed class AddCoordinatesHandlerTests
{
    private static readonly Guid ID = Guid.NewGuid();

    private readonly Mock<IGuidService> guidServiceMock = new();
    private readonly AddCoordinatesHandler handler;
    private readonly NullLogger<AddCoordinatesHandler> logger = new();
    private readonly Mock<ICoordinatesRepository> repositoryMock = new();

    public AddCoordinatesHandlerTests()
    {
        this.guidServiceMock
            .Setup(service => service.NewGuid())
            .Returns(ID);

        this.handler = new AddCoordinatesHandler(this.guidServiceMock.Object, this.logger, this.repositoryMock.Object);
    }

    [TestMethod]
    public async Task Handle_ShouldAddCoordinates()
    {
        //ARRANGE
        CoordinatesEntity? addedCoordinates = null;

        var cancellationToken = CancellationToken.None;

        var request = new AddCoordinates
        {
            Latitude = 24.56m,
            Longitude = 34.57m,
        };

        this.repositoryMock
            .Setup(service => service.AddCoordinatesAsync(It.IsAny<CoordinatesEntity>(), cancellationToken))
            .Callback<CoordinatesEntity, CancellationToken>((entity, _) => addedCoordinates = entity);

        //ACT
        await this.handler.Handle(request, cancellationToken);

        //ASSERT
        this.repositoryMock.Verify(service => service.AddCoordinatesAsync(It.IsAny<CoordinatesEntity>(), cancellationToken), Times.Once);

        var expectedCoordinates = new CoordinatesEntity(ID, request.Latitude, request.Longitude);

        addedCoordinates.Should()
            .BeEquivalentTo(expectedCoordinates);
    }
}
