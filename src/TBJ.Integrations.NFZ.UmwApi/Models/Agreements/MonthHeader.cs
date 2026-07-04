using System.Text.Json.Serialization;

namespace TBJ.Integrations.NFZ.UmwApi.Models.Agreements;

/// <summary>
/// Nagłówek odpowiedzi miesięcznego planu umowy (zasób <c>/months/{id}</c>).
/// Zawiera dane identyfikacyjne kontekstu dla całego miesiąca.
/// </summary>
public sealed class MonthHeader
{
    /// <summary>Rok.</summary>
    [JsonPropertyName("year")]
    public int? Year { get; init; }

    /// <summary>Kod oddziału NFZ (np. "07").</summary>
    [JsonPropertyName("branch")]
    public string? Branch { get; init; }

    /// <summary>Kod świadczeniodawcy.</summary>
    [JsonPropertyName("provider-code")]
    public string? ProviderCode { get; init; }

    /// <summary>Nazwa świadczeniodawcy.</summary>
    [JsonPropertyName("provider-name")]
    public string? ProviderName { get; init; }

    /// <summary>Wyróżnik (np. numer porządkowy w ramach umowy).</summary>
    [JsonPropertyName("order")]
    public string? Order { get; init; }

    /// <summary>Kod umowy.</summary>
    [JsonPropertyName("agreement-code")]
    public string? AgreementCode { get; init; }

    /// <summary>Nazwa rodzaju świadczenia.</summary>
    [JsonPropertyName("service-type-name")]
    public string? ServiceTypeName { get; init; }

    /// <summary>Kod produktu kontraktowego.</summary>
    [JsonPropertyName("contract-product-code")]
    public string? ContractProductCode { get; init; }
}
