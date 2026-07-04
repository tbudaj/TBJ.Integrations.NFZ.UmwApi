namespace TBJ.Integrations.NFZ.UmwApi.Requests;

/// <summary>
/// Parametry pobrania szczegółów umowy NFZ (<c>GET /agreements/{id}</c>).
/// </summary>
public sealed class GetAgreementDetailRequest : PagedRequest
{
    /// <summary>Identyfikator umowy (UUID). Wymagane.</summary>
    public required string Id { get; set; }
}
