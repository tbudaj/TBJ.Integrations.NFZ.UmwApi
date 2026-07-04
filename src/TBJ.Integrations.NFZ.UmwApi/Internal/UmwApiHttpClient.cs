using TBJ.Integrations.NFZ.UmwApi.Abstractions.Exceptions;
using Microsoft.Extensions.Logging;

namespace TBJ.Integrations.NFZ.UmwApi.Internal;

/// <summary>
/// Wewnętrzny wrapper nad <see cref="HttpClient"/> wyspecjalizowany do komunikacji
/// z API Umowy NFZ. Obsługuje wysyłanie żądań GET, obsługę błędów i logowanie.
/// </summary>
internal sealed class UmwApiHttpClient
{
    private readonly HttpClient _http;
    private readonly ILogger<UmwApiHttpClient> _logger;

    public UmwApiHttpClient(HttpClient http, ILogger<UmwApiHttpClient> logger)
    {
        _http = http;
        _logger = logger;
    }

    /// <summary>
    /// Wysyła żądanie GET pod wskazany endpoint z query string i zwraca treść odpowiedzi jako string.
    /// </summary>
    /// <param name="endpoint">Ścieżka zasobu (np. "agreements").</param>
    /// <param name="queryString">Query string (np. "?branch=07&amp;year=2024&amp;page=1&amp;limit=25&amp;format=json").</param>
    /// <param name="cancellationToken">Token anulowania.</param>
    /// <returns>Treść odpowiedzi HTTP jako string.</returns>
    /// <exception cref="NfzApiException">Gdy API zwróci błąd HTTP lub odpowiedź z sekcją <c>errors</c>.</exception>
    public async Task<string> GetAsync(string endpoint, string queryString, CancellationToken cancellationToken = default)
    {
        var url = endpoint + queryString;
        _logger.LogInformation("Wysyłanie żądania GET do API NFZ: {Url}", url);
        _logger.LogDebug("NFZ UMW API: GET {Url}", url);

        HttpResponseMessage response;
        try
        {
            response = await _http.GetAsync(url, cancellationToken);
        }
        catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException || !cancellationToken.IsCancellationRequested)
        {
            _logger.LogWarning(ex, "Przekroczono limit czasu oczekiwania na odpowiedź API NFZ dla {Url}", url);
            throw new NfzApiException($"Przekroczono limit czasu oczekiwania na odpowiedź API NFZ dla {url}.", ex);
        }
        catch (HttpRequestException ex)
        {
            _logger.LogWarning(ex, "Błąd komunikacji z API NFZ dla {Url}: {Message}", url, ex.Message);
            throw new NfzApiException($"Błąd komunikacji z API NFZ dla {url}: {ex.Message}", ex);
        }

        var body = await response.Content.ReadAsStringAsync(cancellationToken);
        _logger.LogDebug("NFZ UMW API: GET {Url} — HTTP {StatusCode}", url, (int)response.StatusCode);

        if (!response.IsSuccessStatusCode)
        {
            var errors = JsonApiDeserializer.TryDeserializeErrors(body);
            var reason = errors.Count > 0
                ? string.Join("; ", errors.Select(e => e.ErrorReason ?? e.ErrorResult ?? e.ErrorCode?.ToString()))
                : body;
            _logger.LogWarning(
                "API NFZ zwróciło błąd HTTP {StatusCode} dla {Url}: {Reason}",
                (int)response.StatusCode,
                url,
                reason);
            throw new NfzApiException(
                $"API NFZ zwróciło błąd HTTP {(int)response.StatusCode} dla {url}: {reason}",
                (int)response.StatusCode,
                errors);
        }

        _logger.LogInformation("Otrzymano odpowiedź z API NFZ: {Url} — HTTP {StatusCode}", url, (int)response.StatusCode);
        return body;
    }
}
