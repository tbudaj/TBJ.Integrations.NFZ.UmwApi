using System.Text.Json.Serialization;

namespace TBJ.Integrations.NFZ.UmwApi.Models.Agreements;

/// <summary>
/// Atrybuty umowy zawartej przez NFZ ze świadczeniodawcą.
/// </summary>
public sealed class AgreementAttributes
{
    /// <summary>Kod umowy.</summary>
    [JsonPropertyName("code")]
    public string? Code { get; init; }

    /// <summary>Techniczny kod umowy.</summary>
    [JsonPropertyName("technical-code")]
    public string? TechnicalCode { get; init; }

    /// <summary>Oryginalny kod umowy (np. numer z OW NFZ).</summary>
    [JsonPropertyName("origin-code")]
    public string? OriginCode { get; init; }

    /// <summary>Kod rodzaju świadczenia (np. "12" = Zaopatrzenie w wyroby medyczne).</summary>
    [JsonPropertyName("service-type")]
    public string? ServiceType { get; init; }

    /// <summary>Nazwa rodzaju świadczenia.</summary>
    [JsonPropertyName("service-name")]
    public string? ServiceName { get; init; }

    /// <summary>Kwota umowy. Może być null.</summary>
    [JsonPropertyName("amount")]
    public decimal? Amount { get; init; }

    /// <summary>Data ostatniej aktualizacji umowy.</summary>
    [JsonPropertyName("updated-at")]
    public DateTime? UpdatedAt { get; init; }

    /// <summary>Kod świadczeniodawcy.</summary>
    [JsonPropertyName("provider-code")]
    public string? ProviderCode { get; init; }

    /// <summary>NIP świadczeniodawcy.</summary>
    [JsonPropertyName("provider-nip")]
    public string? ProviderNip { get; init; }

    /// <summary>REGON świadczeniodawcy.</summary>
    [JsonPropertyName("provider-regon")]
    public string? ProviderRegon { get; init; }

    /// <summary>Numer rejestru świadczeniodawcy. Może być null.</summary>
    [JsonPropertyName("provider-registry-number")]
    public string? ProviderRegistryNumber { get; init; }

    /// <summary>Nazwa świadczeniodawcy.</summary>
    [JsonPropertyName("provider-name")]
    public string? ProviderName { get; init; }

    /// <summary>Miejscowość siedziby świadczeniodawcy.</summary>
    [JsonPropertyName("provider-place")]
    public string? ProviderPlace { get; init; }

    /// <summary>Rok obowiązywania umowy.</summary>
    [JsonPropertyName("year")]
    public int? Year { get; init; }

    /// <summary>Kod oddziału NFZ (np. "07").</summary>
    [JsonPropertyName("branch")]
    public string? Branch { get; init; }
}
