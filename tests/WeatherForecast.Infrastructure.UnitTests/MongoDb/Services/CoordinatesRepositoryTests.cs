namespace WeatherForecast.Infrastructure.UnitTests.MongoDb.Services;

using FluentAssertions;
using MongoDB.Driver;
using WeatherForecast.Application.Coordinates.Models;
using WeatherForecast.Infrastructure.MongoDb.Constants;
using WeatherForecast.Infrastructure.MongoDb.Interfaces;
using WeatherForecast.Infrastructure.MongoDb.Mappers.Services;
using WeatherForecast.Infrastructure.MongoDb.Models;
using WeatherForecast.Infrastructure.MongoDb.Services;

[TestClass]
public sealed class CoordinatesRepositoryTests
{
    private Mock<IMongoCollectionFactory> collectionFactoryMock = null!;
    private Mock<IMongoCollection<CoordinatesDbModel>> collectionMock = null!;
    private CoordinatesDbMapper mapper = null!;
    private CoordinatesRepository repository = null!;

    [TestMethod]
    public async Task AddCoordinatesAsync_Should_InsertDbModelIntoCollection()
    {
        // Arrange
        CoordinatesDbModel? inserted = null;

        var cancellationToken = CancellationToken.None;

        var coordinates = new CoordinatesEntity(Guid.NewGuid(), latitude: 12.34m, longitude: 56.78m);

        this.collectionMock
            .Setup(collection => collection.InsertOneAsync(It.IsAny<CoordinatesDbModel>(), It.IsAny<InsertOneOptions>(), cancellationToken))
            .Callback<CoordinatesDbModel, InsertOneOptions, CancellationToken>((dbModel, _, _) => inserted = dbModel);

        // Act
        await this.repository.AddCoordinatesAsync(coordinates, cancellationToken);

        // Assert
        this.collectionMock
            .Verify(collection => collection.InsertOneAsync(It.IsAny<CoordinatesDbModel>(), It.IsAny<InsertOneOptions>(), cancellationToken), Times.Once);

        var expectedDbModel = new CoordinatesDbModel
        {
            Id = coordinates.Id,
            Latitude = coordinates.Latitude,
            Longitude = coordinates.Longitude,
        };

        inserted.Should()
            .BeEquivalentTo(expectedDbModel);
    }

    [TestMethod]
    public async Task DeleteCoordinatesAsync_Should_DeleteDbModelFromCollection()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;

        var id = Guid.NewGuid();

        // Act
        await this.repository.DeleteCoordinatesAsync(id, cancellationToken);

        // Assert
        this.collectionMock.Verify(collection => collection.DeleteOneAsync(It.IsAny<FilterDefinition<CoordinatesDbModel>>(), cancellationToken), Times.Once);
    }

    [TestInitialize]
    public void TestInitialize()
    {
        this.collectionMock = new Mock<IMongoCollection<CoordinatesDbModel>>();
        this.collectionFactoryMock = new Mock<IMongoCollectionFactory>();
        this.mapper = new CoordinatesDbMapper();

        this.collectionFactoryMock
            .Setup(factory => factory.GetCollection<CoordinatesDbModel>(DatabaseCollections.COORDINATES_COLLECTION_NAME))
            .Returns(this.collectionMock.Object);

        this.repository = new CoordinatesRepository(this.collectionFactoryMock.Object, this.mapper);
    }
}
