using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace LuxuryHub.Infrastructure.Services;

public interface ICacheService
{
    Task<T?> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value, TimeSpan? expiration = null);
    Task RemoveAsync(string key);
    Task RemoveByPatternAsync(string pattern);
}

public class CacheService : ICacheService
{
    private readonly ILogger<CacheService> _logger;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly Dictionary<string, (string Value, DateTime Expiration)> _memoryCache;

    public CacheService(ILogger<CacheService> logger)
    {
        _logger = logger;
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };
        _memoryCache = new Dictionary<string, (string, DateTime)>();
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        try
        {
            if (_memoryCache.TryGetValue(key, out var cachedItem))
            {
                if (DateTime.UtcNow < cachedItem.Expiration)
                {
                    _logger.LogDebug("Cache hit for key: {Key}", key);
                    return JsonSerializer.Deserialize<T>(cachedItem.Value, _jsonOptions);
                }
                else
                {
                    _memoryCache.Remove(key);
                }
            }

            _logger.LogDebug("Cache miss for key: {Key}", key);
            await Task.CompletedTask;
            return default;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Error retrieving from cache for key: {Key}", key);
            await Task.CompletedTask;
            return default;
        }
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        try
        {
            var jsonValue = JsonSerializer.Serialize(value, _jsonOptions);
            var expirationTime = DateTime.UtcNow.Add(expiration ?? TimeSpan.FromMinutes(5));
            
            _memoryCache[key] = (jsonValue, expirationTime);
            _logger.LogDebug("Cached value for key: {Key} with expiration: {Expiration}", key, expiration);
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Error setting cache for key: {Key}", key);
            await Task.CompletedTask;
        }
    }

    public async Task RemoveAsync(string key)
    {
        try
        {
            _memoryCache.Remove(key);
            _logger.LogDebug("Removed cache for key: {Key}", key);
            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Error removing cache for key: {Key}", key);
            await Task.CompletedTask;
        }
    }

    public async Task RemoveByPatternAsync(string pattern)
    {
        try
        {
            // Note: This is a simplified implementation
            // In production, you might want to use Redis SCAN command
            _logger.LogDebug("Pattern cache removal requested for: {Pattern}", pattern);
            // For now, we'll just log it. In a real implementation, you'd scan Redis keys
            await Task.CompletedTask; // Placeholder for async operation
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Error removing cache by pattern: {Pattern}", pattern);
        }
    }
}
