namespace WeatherForecast.Infrastructure.Cache.Interfaces;

using WeatherForecast.Infrastructure.Cache.Models;

internal interface ICacheService
{
    Task ClearAsync<TResponse>(CacheKey<TResponse> key, CancellationToken cancellationToken = default);
    Task<TResponse?> GetAsync<TResponse>(CacheKey<TResponse> key, CancellationToken cancellationToken = default);
    Task SetAsync<TResponse>(CacheKey<TResponse> key, TResponse value, int keyTimeoutInMinutes, CancellationToken cancellationToken = default);
}
