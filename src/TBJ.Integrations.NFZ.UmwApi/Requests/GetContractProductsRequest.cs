namespace TBJ.Integrations.NFZ.UmwApi.Requests;

/// <summary>
/// Parametry wyszukiwania produktów kontraktowych (<c>GET /contract-products</c>).
/// Pole <see cref="Year"/> jest wymagane przez API.
/// </summary>
public sealed class GetContractProductsRequest : PagedRequest
{
    /// <summary>Rok, dla którego pobierane są produkty. Wymagane.</summary>
    public required int Year { get; set; }

    /// <summary>Kod oddziału NFZ (np. "07"). Opcjonalne.</summary>
    public string? Branch { get; set; }

    /// <summary>Filtr pełnotekstowy po kodzie produktu (max 14 znaków). Opcjonalne.</summary>
    public string? Code { get; set; }

    /// <summary>Filtr pełnotekstowy po nazwie produktu (max 250 znaków). Opcjonalne.</summary>
    public string? Name { get; set; }
}
