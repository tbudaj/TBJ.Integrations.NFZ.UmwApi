namespace TBJ.Integrations.NFZ.UmwApi.Models.Agreements;

/// <summary>
/// Szczegółowe dane planu zwracane przez <c>/plans/{id}</c>.
/// Zawiera plan wraz z powiązanymi miesiącami.
/// </summary>
public sealed class PlanDetail
{
    /// <summary>Dane planu.</summary>
    public Plan? Plan { get; init; }

    /// <summary>Miesięczne plany powiązane z tym planem.</summary>
    public IReadOnlyList<Month> Months { get; init; } = [];
}
