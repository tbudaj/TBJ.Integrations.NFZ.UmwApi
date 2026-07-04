using System.Text.Json.Serialization;

namespace TBJ.Integrations.NFZ.UmwApi.Models.Agreements;

/// <summary>
/// Atrybuty planu umowy (zasób <c>/plans/{id}</c>).
/// </summary>
public sealed class PlanAttributes
{
    /// <summary>Rok planu.</summary>
    [JsonPropertyName("year")]
    public int? Year { get; init; }

    /// <summary>Kod oddziału NFZ.</summary>
    [JsonPropertyName("branch")]
    public string? Branch { get; init; }

    /// <summary>Data początku obowiązywania planu.</summary>
    [JsonPropertyName("date-from")]
    public DateOnly? DateFrom { get; init; }

    /// <summary>Data końca obowiązywania planu.</summary>
    [JsonPropertyName("date-to")]
    public DateOnly? DateTo { get; init; }

    /// <summary>Kwota planu.</summary>
    [JsonPropertyName("amount")]
    public decimal? Amount { get; init; }

    /// <summary>Liczba punktów.</summary>
    [JsonPropertyName("points")]
    public decimal? Points { get; init; }

    /// <summary>Wartość punktu.</summary>
    [JsonPropertyName("point-value")]
    public decimal? PointValue { get; init; }
}
