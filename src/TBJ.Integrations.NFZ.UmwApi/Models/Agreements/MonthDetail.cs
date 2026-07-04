namespace TBJ.Integrations.NFZ.UmwApi.Models.Agreements;

/// <summary>
/// Szczegółowe dane miesięcznego planu zwracane przez <c>/months/{id}</c>.
/// Zawiera nagłówek, miesiąc i pakiety.
/// </summary>
public sealed class MonthDetail
{
    /// <summary>Nagłówek kontekstu (identyfikacja umowy, świadczeniodawcy).</summary>
    public MonthHeader? Header { get; init; }

    /// <summary>Dane miesiąca planu.</summary>
    public Month? Month { get; init; }

    /// <summary>Pakiety w tym miesiącu planu.</summary>
    public IReadOnlyList<Package> Packages { get; init; } = [];
}
