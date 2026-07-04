namespace TBJ.Integrations.NFZ.UmwApi.Models.Agreements;

/// <summary>
/// Szczegółowe dane umowy zwracane przez <c>/agreements/{id}</c>.
/// Zawiera umowę wraz z powiązanymi planami i środkami ortopedycznymi.
/// </summary>
public sealed class AgreementDetail
{
    /// <summary>Dane umowy.</summary>
    public Agreement? Agreement { get; init; }

    /// <summary>Plany powiązane z umową.</summary>
    public IReadOnlyList<Plan> Plans { get; init; } = [];

    /// <summary>Środki ortopedyczne objęte umową.</summary>
    public IReadOnlyList<OrthopedicSupplyItem> OrthopedicSupplies { get; init; } = [];
}
