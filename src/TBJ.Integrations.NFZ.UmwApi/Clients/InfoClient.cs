using TBJ.Integrations.NFZ.UmwApi.Abstractions.Interfaces;
using TBJ.Integrations.NFZ.UmwApi.Internal;
using TBJ.Integrations.NFZ.UmwApi.Models.Dictionaries;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace TBJ.Integrations.NFZ.UmwApi.Clients;

/// <summary>
/// Implementacja <see cref="IInfoClient"/> — obsługuje zasoby informacyjne API Umowy NFZ.
/// </summary>
internal sealed class InfoClient : IInfoClient
{
    private readonly UmwApiHttpClient _http;
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _cacheTtl;
    private readonly ILogger<InfoClient> _logger;

    private const string AvailableYearsEndpoint = "available-years";
    private const string AvailableYearsCacheKey = "umw-api:available-years";

    public InfoClient(UmwApiHttpClient http, IMemoryCache cache, TimeSpan cacheTtl, ILogger<InfoClient> logger)
    {
        _http = http;
        _cache = cache;
        _cacheTtl = cacheTtl;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<AvailableYears> GetAvailableYearsAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Pobieranie dostępnych lat danych z API NFZ");

        if (_cache.TryGetValue<AvailableYears>(AvailableYearsCacheKey, out var cached) && cached is not null)
        {
            _logger.LogInformation("UmwApi cache hit: {CacheKey}", AvailableYearsCacheKey);
            _logger.LogDebug("UmwApi cache hit: {CacheKey}", AvailableYearsCacheKey);
            return cached;
        }

        _logger.LogInformation("UmwApi cache miss: {CacheKey}", AvailableYearsCacheKey);
        _logger.LogDebug("UmwApi: GetAvailableYears");
        var json = await _http.GetAsync(AvailableYearsEndpoint, string.Empty, cancellationToken);
        var result = JsonApiDeserializer.DeserializeAvailableYears(json);

        _cache.Set(AvailableYearsCacheKey, result, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = _cacheTtl,
            Size = 1
        });

        return result;
    }
}
