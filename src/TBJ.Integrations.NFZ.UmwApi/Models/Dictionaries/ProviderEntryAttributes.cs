using System.Text.Json.Serialization;

namespace TBJ.Integrations.NFZ.UmwApi.Models.Dictionaries;

/// <summary>
/// Atrybuty wpisu słownikowego świadczeniodawcy (zasób <c>/providers</c>).
/// </summary>
public sealed class ProviderEntryAttributes
{
    /// <summary>Kod oddziału NFZ (np. "07").</summary>
    [JsonPropertyName("branch")]
    public string? Branch { get; init; }

    /// <summary>Kod świadczeniodawcy.</summary>
    [JsonPropertyName("code")]
    public string? Code { get; init; }

    /// <summary>Nazwa świadczeniodawcy.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>NIP świadczeniodawcy.</summary>
    [JsonPropertyName("nip")]
    public string? Nip { get; init; }

    /// <summary>REGON świadczeniodawcy.</summary>
    [JsonPropertyName("regon")]
    public string? Regon { get; init; }

    /// <summary>Numer rejestru. Może być null.</summary>
    [JsonPropertyName("registry-number")]
    public string? RegistryNumber { get; init; }

    /// <summary>Kod pocztowy.</summary>
    [JsonPropertyName("post-code")]
    public string? PostCode { get; init; }

    /// <summary>Ulica i numer.</summary>
    [JsonPropertyName("street")]
    public string? Street { get; init; }

    /// <summary>Miejscowość.</summary>
    [JsonPropertyName("place")]
    public string? Place { get; init; }

    /// <summary>Numer telefonu.</summary>
    [JsonPropertyName("phone")]
    public string? Phone { get; init; }

    /// <summary>Identyfikator gminy (TERYT).</summary>
    [JsonPropertyName("commune")]
    public string? Commune { get; init; }
}
