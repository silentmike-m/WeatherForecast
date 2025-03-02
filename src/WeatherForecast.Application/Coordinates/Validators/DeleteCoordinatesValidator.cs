namespace WeatherForecast.Application.Coordinates.Validators;

using WeatherForecast.Application.Coordinates.Commands;
using WeatherForecast.Application.Coordinates.Constants;
using WeatherForecast.Application.Coordinates.Interfaces;

internal sealed class DeleteCoordinatesValidator : AbstractValidator<DeleteCoordinates>
{
    private readonly ICoordinatesValidationService coordinatesValidationService;

    public DeleteCoordinatesValidator(ICoordinatesValidationService coordinatesValidationService)
    {
        this.coordinatesValidationService = coordinatesValidationService;

        this.RuleFor(request => request.Id)
            .MustAsync(this.CoordinatesExistsAsync)
            .WithErrorCode(ValidationErrorCodes.COORDINATES_NOT_FOUND)
            .WithMessage(ValidationErrorCodes.COORDINATES_NOT_FOUND_MESSAGE);
    }

    private async Task<bool> CoordinatesExistsAsync(Guid id, CancellationToken cancellationToken)
        => await this.coordinatesValidationService.ExistsAsync(id, cancellationToken);
}
