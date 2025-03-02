namespace WeatherForecast.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;

public class ApiController : ControllerBase
{
    private ISender? mediator;
    protected ISender Mediator => this.mediator ??= this.HttpContext.RequestServices.GetRequiredService<ISender>();
}
