namespace WeatherForecast.Application.Coordinates.Models;

public sealed class CoordinatesEntity
{
    public Guid Id { get; }
    public decimal Latitude { get; }
    public decimal Longitude { get; }

    public CoordinatesEntity(Guid id, decimal latitude, decimal longitude)
    {
        //TODO validation coordinates
        this.Id = id;
        this.Latitude = latitude;
        this.Longitude = longitude;
    }
}
