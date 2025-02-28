﻿namespace WeatherForecast.Application.UnitTests.Coordinates.Validators;

using FluentAssertions;
using WeatherForecast.Application.Coordinates.Commands;
using WeatherForecast.Application.Coordinates.Constants;
using WeatherForecast.Application.Coordinates.Interfaces;
using WeatherForecast.Application.Coordinates.Validators;

[TestClass]
public sealed class AddCoordinatesValidatorTests
{
    private readonly Mock<ICoordinatesValidationService> validationServiceMock = new();
    private readonly AddCoordinatesValidator validator;

    public AddCoordinatesValidatorTests()
    {
        this.validationServiceMock
            .Setup(service => service.IsLatitudeAndLongitudeUniqueAsync(It.IsAny<decimal>(), It.IsAny<decimal>(), CancellationToken.None))
            .ReturnsAsync(true);

        this.validator = new AddCoordinatesValidator(this.validationServiceMock.Object);
    }

    [TestMethod]
    public async Task Should_Fail_When_Latitude_HasInvalidDigits()
    {
        //ARRANGE
        var request = new AddCoordinates
        {
            Latitude = 50.555m,
            Longitude = 45.57m,
        };

        //ACT
        var result = await this.validator.ValidateAsync(request);

        //ASSERT
        result.IsValid.Should()
            .BeFalse();

        result.Errors.Should()
            .HaveCount(1)
            .And
            .ContainSingle(error =>
                error.PropertyName == nameof(request.Latitude)
                && error.ErrorCode == ValidationErrorCodes.LATITUDE_HAS_INVALID_FORMAT
                && error.ErrorMessage == ValidationErrorCodes.LATITUDE_HAS_INVALID_FORMAT_MESSAGE);
    }

    [DataTestMethod, DataRow(-91), DataRow(91)]
    public async Task Should_Fail_When_Latitude_IsInvalid(int latitude)
    {
        //ARRANGE
        var request = new AddCoordinates
        {
            Latitude = latitude,
            Longitude = 45.5m,
        };

        //ACT
        var result = await this.validator.ValidateAsync(request);

        //ASSERT
        result.IsValid.Should()
            .BeFalse();

        result.Errors.Should()
            .HaveCount(1)
            .And
            .ContainSingle(error =>
                error.PropertyName == nameof(request.Latitude)
                && error.ErrorCode == ValidationErrorCodes.LATITUDE_HAS_INVALID_VALUE);
    }

    [TestMethod]
    public async Task Should_Fail_When_LatitudeAndLongitude_AreNotUnique()
    {
        //ARRANGE
        var request = new AddCoordinates
        {
            Latitude = 50.55m,
            Longitude = 23.35m,
        };

        this.validationServiceMock
            .Setup(service => service.IsLatitudeAndLongitudeUniqueAsync(request.Latitude, request.Longitude, CancellationToken.None))
            .ReturnsAsync(false);

        //ACT
        var result = await this.validator.ValidateAsync(request);

        //ASSERT
        result.IsValid.Should()
            .BeFalse();

        result.Errors.Should()
            .HaveCount(1)
            .And
            .ContainSingle(error =>
                error.ErrorCode == ValidationErrorCodes.COORDINATES_ALREADY_EXISTS
                && error.ErrorMessage == ValidationErrorCodes.COORDINATES_ALREADY_EXISTS_MESSAGE);
    }

    [TestMethod]
    public async Task Should_Fail_When_Longitude_HasInvalidDigits()
    {
        //ARRANGE
        var request = new AddCoordinates
        {
            Latitude = 50.55m,
            Longitude = 45.567m,
        };

        //ACT
        var result = await this.validator.ValidateAsync(request);

        //ASSERT
        result.IsValid.Should()
            .BeFalse();

        result.Errors.Should()
            .HaveCount(1)
            .And
            .ContainSingle(error =>
                error.PropertyName == nameof(request.Longitude)
                && error.ErrorCode == ValidationErrorCodes.LONGITUDE_HAS_INVALID_FORMAT
                && error.ErrorMessage == ValidationErrorCodes.LONGITUDE_HAS_INVALID_FORMAT_MESSAGE);
    }

    [DataTestMethod, DataRow(-181), DataRow(181)]
    public async Task Should_Fail_When_Longitude_IsInvalid(int longitude)
    {
        //ARRANGE
        var request = new AddCoordinates
        {
            Latitude = 50.55m,
            Longitude = longitude,
        };

        //ACT
        var result = await this.validator.ValidateAsync(request);

        //ASSERT
        result.IsValid.Should()
            .BeFalse();

        result.Errors.Should()
            .HaveCount(1)
            .And
            .ContainSingle(error =>
                error.PropertyName == nameof(request.Longitude)
                && error.ErrorCode == ValidationErrorCodes.LONGITUDE_HAS_INVALID_VALUE);
    }

    [TestMethod]
    public async Task Should_PassValidation()
    {
        //ARRANGE
        var request = new AddCoordinates
        {
            Latitude = 50.55m,
            Longitude = 23.35m,
        };

        //ACT
        var result = await this.validator.ValidateAsync(request);

        //ASSERT
        result.IsValid.Should()
            .BeTrue();
    }
}
