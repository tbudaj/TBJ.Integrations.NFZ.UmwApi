using System.Text.Json.Serialization;

namespace TBJ.Integrations.NFZ.UmwApi.Models.Dictionaries;

/// <summary>
/// Wpis słownikowy zwracany przez <c>/contract-products</c>, <c>/orthopedic-supplies</c>
/// i <c>/service-types</c>.
/// </summary>
public sealed class DictionaryEntry
{
    /// <summary>Typ obiektu — np. "dictionary-entry".</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>Atrybuty wpisu słownikowego.</summary>
    [JsonPropertyName("attributes")]
    public DictionaryEntryAttributes? Attributes { get; init; }
}
