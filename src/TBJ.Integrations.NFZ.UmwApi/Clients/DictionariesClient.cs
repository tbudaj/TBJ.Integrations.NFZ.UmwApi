using TBJ.Integrations.NFZ.UmwApi.Abstractions.Interfaces;
using TBJ.Integrations.NFZ.UmwApi.Internal;
using TBJ.Integrations.NFZ.UmwApi.Models.Dictionaries;
using TBJ.Integrations.NFZ.UmwApi.Pagination;
using TBJ.Integrations.NFZ.UmwApi.Requests;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace TBJ.Integrations.NFZ.UmwApi.Clients;

/// <summary>
/// Implementacja <see cref="IDictionariesClient"/> — obsługuje słowniki API Umowy NFZ.
/// Wyniki słownikowe są cachowane w <see cref="IMemoryCache"/> z konfigurowalnym TTL.
/// </summary>
internal sealed class DictionariesClient : IDictionariesClient
{
    private readonly UmwApiHttpClient _http;
    private readonly IMemoryCache _cache;
    private readonly TimeSpan _cacheTtl;
    private readonly ILogger<DictionariesClient> _logger;

    private const string ContractProductsEndpoint = "contract-products";
    private const string OrthopedicSuppliesEndpoint = "orthopedic-supplies";
    private const string ServiceTypesEndpoint = "service-types";
    private const string ProvidersEndpoint = "providers";

    public DictionariesClient(UmwApiHttpClient http, IMemoryCache cache, TimeSpan cacheTtl, ILogger<DictionariesClient> logger)
    {
        _http = http;
        _cache = cache;
        _cacheTtl = cacheTtl;
        _logger = logger;
    }

    // Produkty kontraktowe

    /// <inheritdoc />
    public async Task<IPage<DictionaryEntry>> GetContractProductsPageAsync(GetContractProductsRequest request, CancellationToken cancellationToken = default)
        => await GetDictionaryPageCachedAsync(ContractProductsEndpoint, QueryBuilder.Build(request),
            json => JsonApiDeserializer.DeserializeDictionaryEntryList(json), cancellationToken);

    /// <inheritdoc />
    public IAsyncEnumerable<DictionaryEntry> GetAllContractProductsAsync(GetContractProductsRequest request, CancellationToken cancellationToken = default)
        => PagedAsyncEnumerable.IterateAll(request, GetContractProductsPageAsync, cancellationToken);

    // Środki ortopedyczne

    /// <inheritdoc />
    public async Task<IPage<DictionaryEntry>> GetOrthopedicSuppliesPageAsync(GetOrthopedicSuppliesRequest request, CancellationToken cancellationToken = default)
        => await GetDictionaryPageCachedAsync(OrthopedicSuppliesEndpoint, QueryBuilder.Build(request),
            json => JsonApiDeserializer.DeserializeDictionaryEntryList(json), cancellationToken);

    /// <inheritdoc />
    public IAsyncEnumerable<DictionaryEntry> GetAllOrthopedicSuppliesAsync(GetOrthopedicSuppliesRequest request, CancellationToken cancellationToken = default)
        => PagedAsyncEnumerable.IterateAll(request, GetOrthopedicSuppliesPageAsync, cancellationToken);

    // Rodzaje świadczeń

    /// <inheritdoc />
    public async Task<IPage<DictionaryEntry>> GetServiceTypesPageAsync(GetServiceTypesRequest request, CancellationToken cancellationToken = default)
        => await GetDictionaryPageCachedAsync(ServiceTypesEndpoint, QueryBuilder.Build(request),
            json => JsonApiDeserializer.DeserializeDictionaryEntryList(json), cancellationToken);

    /// <inheritdoc />
    public IAsyncEnumerable<DictionaryEntry> GetAllServiceTypesAsync(GetServiceTypesRequest request, CancellationToken cancellationToken = default)
        => PagedAsyncEnumerable.IterateAll(request, GetServiceTypesPageAsync, cancellationToken);

    // Świadczeniodawcy

    /// <inheritdoc />
    public async Task<IPage<ProviderEntry>> GetProvidersPageAsync(GetProvidersRequest request, CancellationToken cancellationToken = default)
        => await GetDictionaryPageCachedAsync(ProvidersEndpoint, QueryBuilder.Build(request),
            json => JsonApiDeserializer.DeserializeProviderList(json), cancellationToken);

    /// <inheritdoc />
    public IAsyncEnumerable<ProviderEntry> GetAllProvidersAsync(GetProvidersRequest request, CancellationToken cancellationToken = default)
        => PagedAsyncEnumerable.IterateAll(request, GetProvidersPageAsync, cancellationToken);

    // Wspólna logika cache

    private async Task<IPage<T>> GetDictionaryPageCachedAsync<T>(
        string endpoint,
        string queryString,
        Func<string, IPage<T>> deserialize,
        CancellationToken cancellationToken)
    {
        var cacheKey = $"umw-api:{endpoint}{queryString}";

        if (_cache.TryGetValue<IPage<T>>(cacheKey, out var cached) && cached is not null)
        {
            _logger.LogInformation("UmwApi cache hit: {CacheKey}", cacheKey);
            _logger.LogDebug("UmwApi cache hit: {CacheKey}", cacheKey);
            return cached;
        }

        _logger.LogInformation("UmwApi cache miss: {CacheKey}", cacheKey);
        _logger.LogDebug("UmwApi cache miss: {CacheKey}", cacheKey);
        var json = await _http.GetAsync(endpoint, queryString, cancellationToken);
        var page = deserialize(json);

        _cache.Set(cacheKey, page, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = _cacheTtl,
            Size = 1
        });

        return page;
    }
}
