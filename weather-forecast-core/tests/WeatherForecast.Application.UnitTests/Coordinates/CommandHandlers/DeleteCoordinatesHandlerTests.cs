namespace WeatherForecast.Application.UnitTests.Coordinates.CommandHandlers;

using Microsoft.Extensions.Logging.Abstractions;
using WeatherForecast.Application.Coordinates.CommandHandlers;
using WeatherForecast.Application.Coordinates.Commands;
using WeatherForecast.Application.Coordinates.Interfaces;

[TestClass]
public sealed class DeleteCoordinatesHandlerTests
{
    private readonly DeleteCoordinatesHandler handler;
    private readonly NullLogger<DeleteCoordinatesHandler> logger = new();
    private readonly Mock<ICoordinatesRepository> repositoryMock = new();

    public DeleteCoordinatesHandlerTests()
        => this.handler = new DeleteCoordinatesHandler(this.logger, this.repositoryMock.Object);

    [TestMethod]
    public async Task Handle_ShouldDeleteCoordinates()
    {
        //ARRANGE
        var cancellationToken = CancellationToken.None;

        var request = new DeleteCoordinates
        {
            Id = Guid.NewGuid(),
        };

        //ACT
        await this.handler.Handle(request, cancellationToken);

        //ASSERT
        this.repositoryMock.Verify(x => x.DeleteCoordinatesAsync(request.Id, cancellationToken), Times.Once);
    }
}
