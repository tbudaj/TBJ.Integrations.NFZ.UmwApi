namespace TBJ.Integrations.NFZ.UmwApi.Requests;

/// <summary>
/// Parametry wyszukiwania rodzajów świadczeń (<c>GET /service-types</c>).
/// Pole <see cref="Year"/> jest wymagane przez API.
/// </summary>
public sealed class GetServiceTypesRequest : PagedRequest
{
    /// <summary>Rok, dla którego pobierane są rodzaje świadczeń. Wymagane.</summary>
    public required int Year { get; set; }

    /// <summary>Kod oddziału NFZ (np. "07"). Opcjonalne.</summary>
    public string? Branch { get; set; }
}
