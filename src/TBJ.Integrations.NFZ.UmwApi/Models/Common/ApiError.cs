using System.Text.Json.Serialization;

namespace TBJ.Integrations.NFZ.UmwApi.Models.Common;

/// <summary>
/// Błąd zwrócony przez API Umowy NFZ w sekcji <c>errors</c>.
/// </summary>
public sealed class ApiError
{
    /// <summary>Unikalny identyfikator błędu (UUID).</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Krótki opis — wynik błędu (np. "Cannot return results").</summary>
    [JsonPropertyName("error-result")]
    public string? ErrorResult { get; init; }

    /// <summary>Przyczyna błędu.</summary>
    [JsonPropertyName("error-reason")]
    public string? ErrorReason { get; init; }

    /// <summary>Sugestia rozwiązania.</summary>
    [JsonPropertyName("error-solution")]
    public string? ErrorSolution { get; init; }

    /// <summary>URL do dokumentacji błędu.</summary>
    [JsonPropertyName("error-help")]
    public string? ErrorHelp { get; init; }

    /// <summary>Kod błędu (np. 9201001).</summary>
    [JsonPropertyName("error-code")]
    public int? ErrorCode { get; init; }
}
