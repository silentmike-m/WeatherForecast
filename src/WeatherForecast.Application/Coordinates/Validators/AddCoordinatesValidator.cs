namespace WeatherForecast.Application.Coordinates.Validators;

using WeatherForecast.Application.Coordinates.Commands;
using WeatherForecast.Application.Coordinates.Constants;
using WeatherForecast.Application.Coordinates.Interfaces;

internal sealed class AddCoordinatesValidator : AbstractValidator<AddCoordinates>
{
    private const int MAX_DIGITS = 2;
    private const int MAX_LATITUDE = 90;
    private const int MAX_LONGITUDE = 180;
    private const int MIN_LATITUDE = -90;
    private const int MIN_LONGITUDE = -180;

    private readonly ICoordinatesValidationService validationService;

    public AddCoordinatesValidator(ICoordinatesValidationService validationService)
    {
        this.validationService = validationService;

        this.RuleFor(request => request.Latitude)
            .InclusiveBetween(MIN_LATITUDE, MAX_LATITUDE)
            .WithErrorCode(ValidationErrorCodes.LATITUDE_HAS_INVALID_VALUE)
            .WithMessage(string.Format(ValidationErrorCodes.LATITUDE_HAS_INVALID_VALUE_MESSAGE, MIN_LATITUDE, MAX_LATITUDE));

        this.RuleFor(request => request.Latitude)
            .Must(BeValidCoordinate)
            .WithErrorCode(ValidationErrorCodes.LATITUDE_HAS_INVALID_FORMAT)
            .WithMessage(ValidationErrorCodes.LATITUDE_HAS_INVALID_FORMAT_MESSAGE);

        this.RuleFor(request => request.Longitude)
            .InclusiveBetween(MIN_LONGITUDE, MAX_LONGITUDE)
            .WithErrorCode(ValidationErrorCodes.LONGITUDE_HAS_INVALID_VALUE)
            .WithMessage(string.Format(ValidationErrorCodes.LONGITUDE_HAS_INVALID_VALUE_MESSAGE, MIN_LONGITUDE, MAX_LONGITUDE));

        this.RuleFor(request => request.Longitude)
            .Must(BeValidCoordinate)
            .WithErrorCode(ValidationErrorCodes.LONGITUDE_HAS_INVALID_FORMAT)
            .WithMessage(ValidationErrorCodes.LONGITUDE_HAS_INVALID_FORMAT_MESSAGE);

        this.RuleFor(request => request)
            .MustAsync(this.IsLatitudeAndLongitudeUniqueAsync)
            .WithErrorCode(ValidationErrorCodes.COORDINATES_ALREADY_EXISTS)
            .WithMessage(ValidationErrorCodes.COORDINATES_ALREADY_EXISTS_MESSAGE);
    }

    private async Task<bool> IsLatitudeAndLongitudeUniqueAsync(AddCoordinates request, CancellationToken cancellationToken)
        => await this.validationService.IsLatitudeAndLongitudeUniqueAsync(request.Latitude, request.Longitude, cancellationToken);

    private static bool BeValidCoordinate(decimal coordinate)
        => coordinate.Scale <= MAX_DIGITS;
}
