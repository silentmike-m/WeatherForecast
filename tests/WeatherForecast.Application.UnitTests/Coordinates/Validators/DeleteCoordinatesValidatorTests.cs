namespace WeatherForecast.Application.UnitTests.Coordinates.Validators;

using FluentAssertions;
using WeatherForecast.Application.Coordinates.Commands;
using WeatherForecast.Application.Coordinates.Constants;
using WeatherForecast.Application.Coordinates.Interfaces;
using WeatherForecast.Application.Coordinates.Validators;

[TestClass]
public sealed class DeleteCoordinatesValidatorTests
{
    private static readonly Guid EXISTING_COORDINATES_ID = Guid.NewGuid();

    private readonly Mock<ICoordinatesValidationService> validationServiceMock = new();
    private readonly DeleteCoordinatesValidator validator;

    public DeleteCoordinatesValidatorTests()
    {
        this.validationServiceMock
            .Setup(service => service.ExistsAsync(EXISTING_COORDINATES_ID, It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        this.validator = new DeleteCoordinatesValidator(this.validationServiceMock.Object);
    }

    [TestMethod]
    public async Task Should_Fail_When_CoordinatesNotExists()
    {
        // Arrange
        var request = new DeleteCoordinates
        {
            Id = Guid.NewGuid(),
        };

        // Act
        var result = await this.validator.ValidateAsync(request);

        // Arrange
        result.IsValid.Should()
            .BeFalse();

        result.Errors.Should()
            .HaveCount(1)
            .And
            .ContainSingle(error =>
                error.PropertyName == nameof(request.Id)
                && error.ErrorCode == ValidationErrorCodes.COORDINATES_NOT_FOUND
                && error.ErrorMessage == ValidationErrorCodes.COORDINATES_NOT_FOUND_MESSAGE
            );
    }

    [TestMethod]
    public async Task Should_PassValidation()
    {
        // Arrange
        var request = new DeleteCoordinates
        {
            Id = EXISTING_COORDINATES_ID,
        };

        // Act
        var result = await this.validator.ValidateAsync(request);

        // Arrange
        result.IsValid.Should()
            .BeTrue();
    }
}
