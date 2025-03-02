namespace WeatherForecast.WebApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Application.Coordinates.Commands;
using WeatherForecast.Application.Coordinates.Queries;
using WeatherForecast.WebApi.Models;

[ApiController, Route("Coordinates")]
public sealed class CoordinatesControllers : ApiController
{
    [HttpPost, Route("AddCoordinates")]
    public async Task<IActionResult> AddProduct([FromBody] CoordinatesToAddModel coordinates, CancellationToken cancellationToken)
    {
        var request = new AddCoordinates
        {
            Latitude = coordinates.Latitude,
            Longitude = coordinates.Longitude,
        };

        await this.Mediator.Send(request, cancellationToken);

        return this.Ok();
    }

    [HttpGet, Route("GetCoordinates")]
    public async Task<IReadOnlyList<CoordinatesGetModel>> GetCoordinates(CancellationToken cancellationToken)
    {
        var coordinates = await this.Mediator.Send(new GetCoordinates(), cancellationToken);

        var result = coordinates
            .Select(dto => new CoordinatesGetModel
            {
                Id = dto.Id,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
            }).ToList();

        return result;
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
