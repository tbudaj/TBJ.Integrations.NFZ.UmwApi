using TBJ.Integrations.NFZ.UmwApi.Abstractions.Interfaces;
using TBJ.Integrations.NFZ.UmwApi.Clients;
using TBJ.Integrations.NFZ.UmwApi.Configuration;
using TBJ.Integrations.NFZ.UmwApi.Internal;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace TBJ.Integrations.NFZ.UmwApi;

/// <summary>
/// Rozszerzenia DI dla biblioteki klienta API Umowy NFZ.
/// </summary>
public static class UmwApiExtensions
{
    /// <summary>
    /// Rejestruje klientów API Umowy NFZ (<see cref="IAgreementsClient"/>,
    /// <see cref="IDictionariesClient"/>, <see cref="IInfoClient"/>) w kontenerze DI.
    /// </summary>
    /// <param name="services">Kolekcja serwisów.</param>
    /// <param name="configure">Opcjonalna akcja konfiguracji (BaseUrl, Timeout, CacheTtl).</param>
    /// <returns>Kolekcja serwisów (fluent API).</returns>
    /// <example>
    /// <code>
    /// builder.Services.AddNfzUmwApi(opt =>
    /// {
    ///     opt.BaseUrl = "https://api.nfz.gov.pl/app-umw-api";
    ///     opt.Timeout = TimeSpan.FromSeconds(30);
    ///     opt.DictionaryCacheTtl = TimeSpan.FromHours(24);
    /// });
    /// </code>
    /// </example>
    public static IServiceCollection AddNfzUmwApi(
        this IServiceCollection services,
        Action<UmwApiOptions>? configure = null)
    {
        ArgumentNullException.ThrowIfNull(services);

        var options = new UmwApiOptions();
        configure?.Invoke(options);

        ValidateOptions(options);

        // Singleton opcji — niezmienne po starcie aplikacji
        services.AddSingleton(options);

        // Cache słowników z limitem rozmiaru
        services.AddMemoryCache(cacheOpts =>
        {
            cacheOpts.SizeLimit = options.DictionaryCacheSizeLimit;
        });

        // HttpClient dla UmwApiHttpClient
        services.AddHttpClient<UmwApiHttpClient>((sp, client) =>
        {
            var baseUrl = options.BaseUrl.TrimEnd('/') + "/";
            client.BaseAddress = new Uri(baseUrl);
            client.Timeout = options.Timeout;
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        });

        // Klienci jako Scoped — per-request, korzystają z IHttpClientFactory
        services.AddScoped<IAgreementsClient>(sp =>
            new AgreementsClient(
                sp.GetRequiredService<UmwApiHttpClient>(),
                sp.GetRequiredService<ILogger<AgreementsClient>>()));

        services.AddScoped<IDictionariesClient>(sp =>
            new DictionariesClient(
                sp.GetRequiredService<UmwApiHttpClient>(),
                sp.GetRequiredService<IMemoryCache>(),
                options.DictionaryCacheTtl,
                sp.GetRequiredService<ILogger<DictionariesClient>>()));

        services.AddScoped<IInfoClient>(sp =>
            new InfoClient(
                sp.GetRequiredService<UmwApiHttpClient>(),
                sp.GetRequiredService<IMemoryCache>(),
                options.DictionaryCacheTtl,
                sp.GetRequiredService<ILogger<InfoClient>>()));

        return services;
    }

    /// <summary>
    /// Rejestruje klientów API Umowy NFZ z konfiguracją z sekcji <see cref="UmwApiOptions.SectionName"/>
    /// w <c>IConfiguration</c>.
    /// </summary>
    /// <remarks>
    /// Wymaga obecności sekcji <c>NfzUmwApi</c> w konfiguracji aplikacji.
    /// </remarks>
    /// <param name="services">Kolekcja serwisów.</param>
    /// <param name="configuration">Konfiguracja aplikacji.</param>
    /// <returns>Kolekcja serwisów (fluent API).</returns>
    public static IServiceCollection AddNfzUmwApi(
        this IServiceCollection services,
        Microsoft.Extensions.Configuration.IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        var section = configuration.GetSection(UmwApiOptions.SectionName);
        return services.AddNfzUmwApi(opt =>
        {
            var baseUrl = section["BaseUrl"];
            if (!string.IsNullOrWhiteSpace(baseUrl))
                opt.BaseUrl = baseUrl;

            var timeout = section["Timeout"];
            if (!string.IsNullOrWhiteSpace(timeout) && TimeSpan.TryParse(timeout, out var ts))
                opt.Timeout = ts;

            var cacheTtl = section["DictionaryCacheTtl"];
            if (!string.IsNullOrWhiteSpace(cacheTtl) && TimeSpan.TryParse(cacheTtl, out var ttl))
                opt.DictionaryCacheTtl = ttl;

            var cacheSize = section["DictionaryCacheSizeLimit"];
            if (!string.IsNullOrWhiteSpace(cacheSize) && long.TryParse(cacheSize, out var size))
                opt.DictionaryCacheSizeLimit = size;
        });
    }

    private static void ValidateOptions(UmwApiOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.BaseUrl))
            throw new InvalidOperationException($"Brak konfiguracji {nameof(UmwApiOptions.BaseUrl)} dla NfzUmwApi.");

        if (!Uri.TryCreate(options.BaseUrl, UriKind.Absolute, out _))
            throw new InvalidOperationException($"Nieprawidłowy URL '{options.BaseUrl}' w konfiguracji {nameof(UmwApiOptions.BaseUrl)}.");

        if (options.Timeout <= TimeSpan.Zero)
            throw new InvalidOperationException($"Wartość {nameof(UmwApiOptions.Timeout)} musi być większa od zera.");
    }
}
