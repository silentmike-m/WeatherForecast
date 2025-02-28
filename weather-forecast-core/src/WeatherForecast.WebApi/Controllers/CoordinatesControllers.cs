namespace WeatherForecast.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Application.Coordinates.Commands;
using WeatherForecast.WebApi.Models;

[ApiController, Route("Coordinates")]
public sealed class CoordinatesControllers : ApiController
{
    [HttpPost, Route("AddCoordinates")]
    public async Task<IActionResult> AddProduct([FromBody] CoordinatesToAdd coordinates, CancellationToken cancellationToken)
    {
        var request = new AddCoordinates
        {
            Latitude = coordinates.Latitude,
            Longitude = coordinates.Longitude,
        };

        await this.Mediator.Send(request, cancellationToken);

        return this.Ok();
    }

    [HttpDelete, Route("DeleteCoordinates")]
    public async Task<IActionResult> UpdateProduct([FromQuery] Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteCoordinates
        {
            Id = id,
        };

        await this.Mediator.Send(request, cancellationToken);

        return this.Ok();
    }
}
