using System.Text.Json.Serialization;

namespace TBJ.Integrations.NFZ.UmwApi.Models.Common;

/// <summary>
/// Metadane zwracane w sekcji <c>meta</c> każdej odpowiedzi API Umowy NFZ.
/// </summary>
public sealed class PageMeta
{
    /// <summary>URL schematu danych zwracanych w tej odpowiedzi.</summary>
    [JsonPropertyName("@context")]
    public string? Context { get; init; }

    /// <summary>Łączna liczba wyników pasujących do zapytania (tylko dla list).</summary>
    [JsonPropertyName("count")]
    public int? Count { get; init; }

    /// <summary>Numer bieżącej strony wyników (tylko dla list).</summary>
    [JsonPropertyName("page")]
    public int? Page { get; init; }

    /// <summary>Liczba wyników na stronie (tylko dla list).</summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    /// <summary>Nazwa zasobu informacyjnego.</summary>
    [JsonPropertyName("title")]
    public string? Title { get; init; }

    /// <summary>URL do pobrania zasobu informacyjnego.</summary>
    [JsonPropertyName("url")]
    public string? Url { get; init; }

    /// <summary>Dostawca zasobu — Narodowy Fundusz Zdrowia.</summary>
    [JsonPropertyName("provider")]
    public string? Provider { get; init; }

    /// <summary>Data i czas pierwszej publikacji zasobu (ISO-8601).</summary>
    [JsonPropertyName("date-published")]
    public DateTimeOffset? DatePublished { get; init; }

    /// <summary>Data i czas ostatniej modyfikacji zasobu (ISO-8601).</summary>
    [JsonPropertyName("date-modified")]
    public DateTimeOffset? DateModified { get; init; }

    /// <summary>Opis zasobu informacyjnego.</summary>
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    /// <summary>Słowa kluczowe opisujące zasób.</summary>
    [JsonPropertyName("keywords")]
    public string? Keywords { get; init; }

    /// <summary>Język zasobu (np. "PL").</summary>
    [JsonPropertyName("language")]
    public string? Language { get; init; }

    /// <summary>Typ zawartości i kodowanie (np. "application/json; charset=utf-8").</summary>
    [JsonPropertyName("content-type")]
    public string? ContentType { get; init; }

    /// <summary>Przynależność zasobu do grupy (np. "Umowy", "Słowniki").</summary>
    [JsonPropertyName("is-part-of")]
    public string? IsPartOf { get; init; }

    /// <summary>Wersja API, z której pochodzi odpowiedź.</summary>
    [JsonPropertyName("version")]
    public string? Version { get; init; }
}
