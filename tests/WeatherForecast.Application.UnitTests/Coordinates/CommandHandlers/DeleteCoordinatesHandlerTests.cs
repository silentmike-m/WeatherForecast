namespace WeatherForecast.Application.UnitTests.Coordinates.CommandHandlers;

using FluentAssertions;
using MediatR;
using Microsoft.Extensions.Logging.Abstractions;
using WeatherForecast.Application.Coordinates.CommandHandlers;
using WeatherForecast.Application.Coordinates.Commands;
using WeatherForecast.Application.Coordinates.Interfaces;
using WeatherForecast.Application.Coordinates.Models;
using WeatherForecast.Application.Coordinates.Notification;

[TestClass]
public sealed class DeleteCoordinatesHandlerTests
{
    private readonly DeleteCoordinatesHandler handler;
    private readonly NullLogger<DeleteCoordinatesHandler> logger = new();
    private readonly Mock<IPublisher> mediatorMock = new();
    private readonly Mock<ICoordinatesRepository> repositoryMock = new();

    public DeleteCoordinatesHandlerTests()
        => this.handler = new DeleteCoordinatesHandler(this.logger, this.mediatorMock.Object, this.repositoryMock.Object);

    [TestMethod]
    public async Task Handle_Should_DeleteCoordinates()
    {
        // Arrange
        DeletedCoordinates? deletedCoordinates = null;

        var cancellationToken = CancellationToken.None;

        var entity = new CoordinatesEntity(Guid.NewGuid(), latitude: 12.34m, longitude: 56.78m);

        var request = new DeleteCoordinates
        {
            Id = entity.Id,
        };

        this.mediatorMock
            .Setup(service => service.Publish(It.IsAny<DeletedCoordinates>(), cancellationToken))
            .Callback<DeletedCoordinates, CancellationToken>((notification, _) => deletedCoordinates = notification);

        this.repositoryMock
            .Setup(service => service.GetCoordinatesAsync(request.Id, cancellationToken))
            .ReturnsAsync(entity);

        // Act
        await this.handler.Handle(request, cancellationToken);

        // Arrange
        this.repositoryMock
            .Verify(x => x.DeleteCoordinatesAsync(entity, cancellationToken), Times.Once);

        this.mediatorMock
            .Verify(service => service.Publish(It.IsAny<DeletedCoordinates>(), cancellationToken), Times.Once);

        var expectedNotification = new DeletedCoordinates
        {
            Latitude = entity.Latitude,
            Longitude = entity.Longitude,
        };

        deletedCoordinates.Should()
            .NotBeNull()
            .And
            .BeEquivalentTo(expectedNotification);
    }

    [TestMethod]
    public async Task Handle_Should_NotThrowException_When_MissingCoordinates()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;

        var request = new DeleteCoordinates
        {
            Id = Guid.NewGuid(),
        };

        this.repositoryMock
            .Setup(service => service.GetCoordinatesAsync(request.Id, cancellationToken))
            .ReturnsAsync((CoordinatesEntity?)null);

        // Act
        var action = async () => await this.handler.Handle(request, cancellationToken);

        // Arrange
        await action.Should()
            .NotThrowAsync();

        this.repositoryMock
            .Verify(x => x.DeleteCoordinatesAsync(It.IsAny<CoordinatesEntity>(), cancellationToken), Times.Never);

        this.mediatorMock
            .Verify(service => service.Publish(It.IsAny<DeletedCoordinates>(), cancellationToken), Times.Never);
    }
}
