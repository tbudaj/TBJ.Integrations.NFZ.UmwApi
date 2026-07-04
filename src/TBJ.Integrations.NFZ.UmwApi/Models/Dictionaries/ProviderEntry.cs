using System.Text.Json.Serialization;

namespace TBJ.Integrations.NFZ.UmwApi.Models.Dictionaries;

/// <summary>
/// Wpis słownikowy świadczeniodawcy zwracany przez zasób <c>/providers</c>.
/// </summary>
public sealed class ProviderEntry
{
    /// <summary>Typ obiektu — zawsze "dictionary-provider-entry".</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>Atrybuty świadczeniodawcy.</summary>
    [JsonPropertyName("attributes")]
    public ProviderEntryAttributes? Attributes { get; init; }
}
