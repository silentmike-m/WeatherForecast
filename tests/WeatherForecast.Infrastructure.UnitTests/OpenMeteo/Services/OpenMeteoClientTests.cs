namespace WeatherForecast.Infrastructure.UnitTests.OpenMeteo.Services;

using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Moq.Protected;
using WeatherForecast.Infrastructure.OpenMeteo;
using WeatherForecast.Infrastructure.OpenMeteo.Models;
using WeatherForecast.Infrastructure.OpenMeteo.Services;

[TestClass]
public sealed class OpenMeteoClientTests
{
    private readonly OpenMeteoOptions options = new()
    {
        ForecastDays = 7,
    };

    private OpenMeteoClient client = null!;
    private Mock<HttpMessageHandler> handlerMock = null!;
    private Mock<IHttpClientFactory> httpClientFactoryMock = null!;

    [TestMethod]
    public async Task GetWeatherForecastAsync_Should_ConstructsCorrectEndpoint()
    {
        // Arrange
        Uri? executedUri = null;

        const decimal latitude = 10.12m;
        const decimal longitude = 20.15m;

        using var httpClient = new HttpClient(this.handlerMock.Object)
        {
            BaseAddress = new Uri("https://test.domain.com"),
        };

        this.httpClientFactoryMock
            .Setup(factory => factory.CreateClient(OpenMeteoOptions.HTTP_CLIENT_NAME))
            .Returns(httpClient);

        this.handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(m => m.Method == HttpMethod.Get), ItExpr.IsAny<CancellationToken>())
            .Callback<HttpRequestMessage, CancellationToken>((request, _) => executedUri = request.RequestUri)
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(new GetWeatherForecastResponse()),
            });

        // Act
        await this.client.GetWeatherForecastAsync(latitude, longitude, CancellationToken.None);

        // Assert
        var expectedUri = new Uri($"{httpClient.BaseAddress}forecast?latitude={latitude}&longitude={longitude}&daily=weather_code,temperature_2m_max,temperature_2m_min,apparent_temperature_max,apparent_temperature_min,rain_sum,showers_sum,snowfall_sum&forecast_days={this.options.ForecastDays}");

        executedUri.Should()
            .BeEquivalentTo(expectedUri);
    }

    [TestMethod]
    public async Task GetWeatherForecastAsync_Should_ReturnNull_WhenApiReturnsNoData()
    {
        // Arrange
        using var httpClient = new HttpClient(this.handlerMock.Object)
        {
            BaseAddress = new Uri("https://test.domain.com"),
        };

        this.httpClientFactoryMock
            .Setup(factory => factory.CreateClient(OpenMeteoOptions.HTTP_CLIENT_NAME))
            .Returns(httpClient);

        this.handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(m => m.Method == HttpMethod.Get), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create((GetWeatherForecastResponse?)null),
            });

        // Act
        var result = await this.client.GetWeatherForecastAsync(latitude: 10.12345m, longitude: 20.12345m, CancellationToken.None);

        // Assert
        result.Should()
            .BeNull();
    }

    [TestMethod]
    public async Task GetWeatherForecastAsync_Should_ReturnSuccessResult()
    {
        // Arrange
        var response = new GetWeatherForecastResponse();

        using var httpClient = new HttpClient(this.handlerMock.Object)
        {
            BaseAddress = new Uri("https://test.domain.com"),
        };

        this.httpClientFactoryMock
            .Setup(factory => factory.CreateClient(OpenMeteoOptions.HTTP_CLIENT_NAME))
            .Returns(httpClient);

        this.handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(m => m.Method == HttpMethod.Get), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(response),
            });

        // Act
        var result = await this.client.GetWeatherForecastAsync(latitude: 10.12345m, longitude: 20.12345m, CancellationToken.None);

        // Assert
        result.Should()
            .NotBeNull()
            .And
            .BeEquivalentTo(response);
    }

    [TestMethod]
    public async Task GetWeatherForecastAsync_Should_ThrowsException_OnApiError()
    {
        // Arrange
        using var httpClient = new HttpClient(this.handlerMock.Object)
        {
            BaseAddress = new Uri("https://test.domain.com"),
        };

        this.httpClientFactoryMock
            .Setup(factory => factory.CreateClient(OpenMeteoOptions.HTTP_CLIENT_NAME))
            .Returns(httpClient);

        this.handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.Is<HttpRequestMessage>(m => m.Method == HttpMethod.Get), ItExpr.IsAny<CancellationToken>())
            .ThrowsAsync(new HttpRequestException());

        // Act
        var action = async () => await this.client.GetWeatherForecastAsync(latitude: 10.12345m, longitude: 20.12345m, CancellationToken.None);

        // Assert
        await action.Should()
            .ThrowAsync<HttpRequestException>();
    }

    [TestInitialize]
    public void Initialize()
    {
        this.httpClientFactoryMock = new Mock<IHttpClientFactory>();

        this.handlerMock = new Mock<HttpMessageHandler>();

        this.client = new OpenMeteoClient(this.httpClientFactoryMock.Object, this.options);
    }
}
