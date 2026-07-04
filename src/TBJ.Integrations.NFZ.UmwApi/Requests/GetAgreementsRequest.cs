namespace TBJ.Integrations.NFZ.UmwApi.Requests;

/// <summary>
/// Parametry wyszukiwania umów NFZ (<c>GET /agreements</c>).
/// Pola <see cref="Branch"/> i <see cref="Year"/> są wymagane przez API.
/// </summary>
public sealed class GetAgreementsRequest : PagedRequest
{
    /// <summary>
    /// Kod oddziału NFZ (np. "01".."16"). Wymagane.
    /// </summary>
    public required string Branch { get; set; }

    /// <summary>
    /// Rok, dla którego pobierane są umowy. Wymagane.
    /// </summary>
    public required int Year { get; set; }

    /// <summary>
    /// Filtr po kodzie rodzaju świadczenia (np. "12" = wyroby medyczne). Opcjonalne.
    /// </summary>
    public string? ServiceType { get; set; }

    /// <summary>Filtr pełnotekstowy po kodzie świadczeniodawcy (max 24 znaki). Opcjonalne.</summary>
    public string? ProviderCode { get; set; }

    /// <summary>Filtr pełnotekstowy po nazwie świadczeniodawcy (max 250 znaków). Opcjonalne.</summary>
    public string? ProviderName { get; set; }

    /// <summary>Filtr po NIP świadczeniodawcy (max 13 znaków). Opcjonalne.</summary>
    public string? ProviderNip { get; set; }

    /// <summary>Filtr po REGON świadczeniodawcy (max 19 znaków). Opcjonalne.</summary>
    public string? ProviderRegon { get; set; }

    /// <summary>Filtr pełnotekstowy po miejscowości świadczeniodawcy (max 250 znaków). Opcjonalne.</summary>
    public string? Place { get; set; }

    /// <summary>
    /// Filtr po dacie ostatniej aktualizacji umowy (ISO-8601, np. "2024-01-01"). Opcjonalne.
    /// </summary>
    public string? UpdatedAt { get; set; }
}
