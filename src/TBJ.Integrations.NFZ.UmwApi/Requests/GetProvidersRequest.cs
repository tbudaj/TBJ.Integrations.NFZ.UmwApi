namespace TBJ.Integrations.NFZ.UmwApi.Requests;

/// <summary>
/// Parametry wyszukiwania świadczeniodawców (<c>GET /providers</c>).
/// Pola <see cref="Branch"/> i <see cref="Year"/> są wymagane przez API.
/// </summary>
public sealed class GetProvidersRequest : PagedRequest
{
    /// <summary>Kod oddziału NFZ (np. "01".."16"). Wymagane.</summary>
    public required string Branch { get; set; }

    /// <summary>Rok, dla którego pobierani są świadczeniodawcy. Wymagane.</summary>
    public required int Year { get; set; }

    /// <summary>Filtr pełnotekstowy po kodzie świadczeniodawcy (max 24 znaki). Opcjonalne.</summary>
    public string? Code { get; set; }

    /// <summary>Filtr pełnotekstowy po nazwie świadczeniodawcy (max 250 znaków). Opcjonalne.</summary>
    public string? Name { get; set; }

    /// <summary>Filtr po NIP (max 13 znaków). Opcjonalne.</summary>
    public string? Nip { get; set; }

    /// <summary>Filtr po REGON (max 19 znaków). Opcjonalne.</summary>
    public string? Regon { get; set; }

    /// <summary>Filtr pełnotekstowy po miejscowości (max 250 znaków). Opcjonalne.</summary>
    public string? Place { get; set; }
}
