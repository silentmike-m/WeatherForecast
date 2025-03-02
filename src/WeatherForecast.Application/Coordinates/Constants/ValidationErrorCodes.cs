namespace WeatherForecast.Application.Coordinates.Constants;

public static class ValidationErrorCodes
{
    public static readonly string COORDINATES_ALREADY_EXISTS = nameof(COORDINATES_ALREADY_EXISTS);
    public static readonly string COORDINATES_ALREADY_EXISTS_MESSAGE = "Coordinates with provided latitude and longitude already exists";
    public static readonly string COORDINATES_NOT_FOUND = nameof(COORDINATES_NOT_FOUND);
    public static readonly string COORDINATES_NOT_FOUND_MESSAGE = "Coordinates has not been found";
    public static readonly string LATITUDE_HAS_INVALID_FORMAT = nameof(LATITUDE_HAS_INVALID_FORMAT);
    public static readonly string LATITUDE_HAS_INVALID_FORMAT_MESSAGE = "Latitude has invalid format";
    public static readonly string LATITUDE_HAS_INVALID_VALUE = nameof(LATITUDE_HAS_INVALID_VALUE);
    public static readonly string LATITUDE_HAS_INVALID_VALUE_MESSAGE = "Latitude should be between {0} and {1}";
    public static readonly string LONGITUDE_HAS_INVALID_FORMAT = nameof(LONGITUDE_HAS_INVALID_FORMAT);
    public static readonly string LONGITUDE_HAS_INVALID_FORMAT_MESSAGE = "Longitude has invalid format";
    public static readonly string LONGITUDE_HAS_INVALID_VALUE = nameof(LONGITUDE_HAS_INVALID_VALUE);
    public static readonly string LONGITUDE_HAS_INVALID_VALUE_MESSAGE = "Longitude should be between {0} and {1}";
}
