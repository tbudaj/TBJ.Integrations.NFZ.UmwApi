using System.Text.Json.Serialization;

namespace TBJ.Integrations.NFZ.UmwApi.Models.Dictionaries;

/// <summary>
/// Atrybuty wpisu słownikowego (dla zasobów <c>/contract-products</c>,
/// <c>/orthopedic-supplies</c> i <c>/service-types</c>).
/// </summary>
public sealed class DictionaryEntryAttributes
{
    /// <summary>Kod wpisu słownikowego.</summary>
    [JsonPropertyName("code")]
    public string? Code { get; init; }

    /// <summary>Nazwa wpisu słownikowego.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Rok, dla którego obowiązuje wpis.</summary>
    [JsonPropertyName("year")]
    public int? Year { get; init; }
}
