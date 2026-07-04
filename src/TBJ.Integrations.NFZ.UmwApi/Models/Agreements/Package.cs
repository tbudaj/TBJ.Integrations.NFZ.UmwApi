using System.Text.Json.Serialization;
using TBJ.Integrations.NFZ.UmwApi.Models.Common;

namespace TBJ.Integrations.NFZ.UmwApi.Models.Agreements;

/// <summary>
/// Pakiet w miesięcznym planie umowy NFZ.
/// </summary>
public sealed class Package
{
    /// <summary>Typ obiektu — zawsze "package".</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>Atrybuty pakietu.</summary>
    [JsonPropertyName("attributes")]
    public PackageAttributes? Attributes { get; init; }

    /// <summary>Linki powiązane z pakietem.</summary>
    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }
}
