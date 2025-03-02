namespace WeatherForecast.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;

public sealed class HomeController : Controller
{
    private readonly ILogger<HomeController> logger;

    public HomeController(ILogger<HomeController> logger)
    {
        this.logger = logger;
    }

    [Route(""), HttpGet, ApiExplorerSettings(IgnoreApi = true)]
    public RedirectResult Index()
    {
        this.logger.LogInformation("Redirect to Swagger");

        return this.Redirect("swagger/index.html");
    }
}
