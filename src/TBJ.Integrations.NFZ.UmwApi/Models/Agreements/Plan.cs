using System.Text.Json.Serialization;
using TBJ.Integrations.NFZ.UmwApi.Models.Common;

namespace TBJ.Integrations.NFZ.UmwApi.Models.Agreements;

/// <summary>
/// Plan umowy NFZ.
/// </summary>
public sealed class Plan
{
    /// <summary>Identyfikator planu (UUID).</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Typ obiektu — zawsze "plan".</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>Atrybuty planu.</summary>
    [JsonPropertyName("attributes")]
    public PlanAttributes? Attributes { get; init; }

    /// <summary>Linki powiązane z planem.</summary>
    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }
}
