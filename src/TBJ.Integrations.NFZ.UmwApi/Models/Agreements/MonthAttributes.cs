using System.Text.Json.Serialization;

namespace TBJ.Integrations.NFZ.UmwApi.Models.Agreements;

/// <summary>
/// Atrybuty miesięcznego planu umowy.
/// </summary>
public sealed class MonthAttributes
{
    /// <summary>Numer miesiąca (1-12).</summary>
    [JsonPropertyName("month")]
    public int? Month { get; init; }

    /// <summary>Rok.</summary>
    [JsonPropertyName("year")]
    public int? Year { get; init; }

    /// <summary>Kwota na dany miesiąc.</summary>
    [JsonPropertyName("amount")]
    public decimal? Amount { get; init; }

    /// <summary>Liczba punktów.</summary>
    [JsonPropertyName("points")]
    public decimal? Points { get; init; }
}
