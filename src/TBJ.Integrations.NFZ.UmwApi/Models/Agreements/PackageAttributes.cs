using System.Text.Json.Serialization;

namespace TBJ.Integrations.NFZ.UmwApi.Models.Agreements;

/// <summary>
/// Atrybuty pakietu w miesięcznym planie umowy.
/// </summary>
public sealed class PackageAttributes
{
    /// <summary>Kod pakietu.</summary>
    [JsonPropertyName("code")]
    public string? Code { get; init; }

    /// <summary>Nazwa pakietu.</summary>
    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>Kwota pakietu.</summary>
    [JsonPropertyName("amount")]
    public decimal? Amount { get; init; }

    /// <summary>Liczba punktów w pakiecie.</summary>
    [JsonPropertyName("points")]
    public decimal? Points { get; init; }
}
