using System.Text.Json.Serialization;
using TBJ.Integrations.NFZ.UmwApi.Models.Common;

namespace TBJ.Integrations.NFZ.UmwApi.Models.Agreements;

/// <summary>
/// Miesięczny plan umowy NFZ.
/// </summary>
public sealed class Month
{
    /// <summary>Identyfikator miesiąca (UUID).</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Typ obiektu — zawsze "month".</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>Atrybuty miesiąca planu.</summary>
    [JsonPropertyName("attributes")]
    public MonthAttributes? Attributes { get; init; }

    /// <summary>Linki powiązane z miesiącem.</summary>
    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }
}
