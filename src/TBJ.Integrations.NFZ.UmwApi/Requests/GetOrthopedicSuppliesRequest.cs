namespace TBJ.Integrations.NFZ.UmwApi.Requests;

/// <summary>
/// Parametry wyszukiwania środków ortopedycznych (<c>GET /orthopedic-supplies</c>).
/// Pole <see cref="Year"/> jest wymagane przez API.
/// </summary>
public sealed class GetOrthopedicSuppliesRequest : PagedRequest
{
    /// <summary>Rok, dla którego pobierane są środki ortopedyczne. Wymagane.</summary>
    public required int Year { get; set; }

    /// <summary>Kod oddziału NFZ (np. "07"). Opcjonalne.</summary>
    public string? Branch { get; set; }

    /// <summary>Filtr pełnotekstowy po kodzie środka (max 13 znaków). Opcjonalne.</summary>
    public string? Code { get; set; }

    /// <summary>Filtr pełnotekstowy po nazwie środka (max 250 znaków). Opcjonalne.</summary>
    public string? Name { get; set; }
}
