namespace WeatherForecast.Infrastructure.OpenMeteo;

internal sealed record OpenMeteoOptions
{
    public const string HTTP_CLIENT_NAME = "OpenMeteo";

    public Uri BaseAddress { get; set; } = new("about:blank");
    public int ForecastDays { get; set; } = 3;
    public int RetryCount { get; set; } = 3;
    public int RetrySleepDurationInSeconds { get; set; } = 5;
    public int TimeoutInSeconds { get; set; } = 30;
}
