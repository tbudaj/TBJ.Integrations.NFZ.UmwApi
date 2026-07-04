using System.Text.Json.Serialization;

namespace TBJ.Integrations.NFZ.UmwApi.Models.Agreements;

/// <summary>
/// Środek ortopedyczny powiązany z umową (z odpowiedzi <c>/agreements/{id}</c>).
/// </summary>
public sealed class OrthopedicSupplyItem
{
    /// <summary>Typ obiektu — zawsze "orthopedic-supply".</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>Atrybuty środka ortopedycznego.</summary>
    [JsonPropertyName("attributes")]
    public OrthopedicSupplyItemAttributes? Attributes { get; init; }
}

/// <summary>
/// Atrybuty środka ortopedycznego powiązanego z umową.
/// </summary>
public sealed class OrthopedicSupplyItemAttributes
{
    /// <summary>Kod środka ortopedycznego (np. "O.02.01").</summary>
    [JsonPropertyName("code")]
    public string? Code { get; init; }

    /// <summary>Nazwa środka ortopedycznego.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }
}
