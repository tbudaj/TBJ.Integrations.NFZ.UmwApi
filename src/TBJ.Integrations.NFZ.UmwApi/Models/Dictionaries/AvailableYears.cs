using System.Text.Json.Serialization;

namespace TBJ.Integrations.NFZ.UmwApi.Models.Dictionaries;

/// <summary>
/// Zakres dostępnych lat danych zwracany przez <c>/available-years</c>.
/// </summary>
public sealed class AvailableYears
{
    /// <summary>Pierwszy rok dla którego dostępne są dane.</summary>
    [JsonPropertyName("start-year")]
    public int StartYear { get; init; }

    /// <summary>Ostatni rok dla którego dostępne są dane.</summary>
    [JsonPropertyName("end-year")]
    public int EndYear { get; init; }
}
