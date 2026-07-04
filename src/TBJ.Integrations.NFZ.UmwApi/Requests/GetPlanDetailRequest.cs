namespace TBJ.Integrations.NFZ.UmwApi.Requests;

/// <summary>
/// Parametry pobrania szczegółów planu umowy NFZ (<c>GET /plans/{id}</c>).
/// </summary>
public sealed class GetPlanDetailRequest : PagedRequest
{
    /// <summary>Identyfikator planu (UUID). Wymagane.</summary>
    public required string Id { get; set; }
}
