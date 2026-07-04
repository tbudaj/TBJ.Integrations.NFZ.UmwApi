namespace TBJ.Integrations.NFZ.UmwApi.Requests;

/// <summary>
/// Bazowe parametry stronicowania wspólne dla wszystkich zapytań listujących.
/// </summary>
public abstract class PagedRequest
{
    /// <summary>Numer strony wyników (numeracja od 1). Domyślnie 1.</summary>
    public int Page { get; set; } = 1;

    /// <summary>
    /// Liczba wyników na stronie. Maksymalnie 25 (limit API NFZ).
    /// Domyślnie 25.
    /// </summary>
    public int Limit { get; set; } = 25;
}
