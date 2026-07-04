using TBJ.Integrations.NFZ.UmwApi.Models.Dictionaries;

namespace TBJ.Integrations.NFZ.UmwApi.Abstractions.Interfaces;

/// <summary>
/// Kontrakt dostępu do zasobów informacyjnych API Umowy NFZ.
/// Obsługuje endpoint <c>/available-years</c>.
/// </summary>
public interface IInfoClient
{
    /// <summary>
    /// Pobiera zakres lat dla których dostępne są dane w API NFZ.
    /// Wynik jest cachowany — wartość zmienia się rzadko.
    /// </summary>
    /// <param name="cancellationToken">Token anulowania.</param>
    /// <returns>Zakres dostępnych lat.</returns>
    /// <exception cref="Exceptions.NfzApiException">Gdy API zwróci błąd.</exception>
    Task<AvailableYears> GetAvailableYearsAsync(CancellationToken cancellationToken = default);
}
