using System.Text.Json.Serialization;

namespace TBJ.Integrations.NFZ.UmwApi.Models.Common;

/// <summary>
/// Linki HATEOAS zwracane w sekcji <c>links</c> odpowiedzi API Umowy NFZ.
/// Umożliwiają nawigację po stronicowanych wynikach.
/// </summary>
public sealed class PageLinks
{
    /// <summary>URL pierwszej strony wyników.</summary>
    [JsonPropertyName("first")]
    public string? First { get; init; }

    /// <summary>URL poprzedniej strony wyników. Null na pierwszej stronie.</summary>
    [JsonPropertyName("prev")]
    public string? Prev { get; init; }

    /// <summary>URL bieżącej strony wyników.</summary>
    [JsonPropertyName("self")]
    public string? Self { get; init; }

    /// <summary>URL następnej strony wyników. Null na ostatniej stronie.</summary>
    [JsonPropertyName("next")]
    public string? Next { get; init; }

    /// <summary>URL ostatniej strony wyników.</summary>
    [JsonPropertyName("last")]
    public string? Last { get; init; }

    /// <summary>URL powiązanego zasobu (np. szczegółów umowy).</summary>
    [JsonPropertyName("related")]
    public string? Related { get; init; }
}
