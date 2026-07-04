using TBJ.Integrations.NFZ.UmwApi.Models.Agreements;
using TBJ.Integrations.NFZ.UmwApi.Pagination;
using TBJ.Integrations.NFZ.UmwApi.Requests;

namespace TBJ.Integrations.NFZ.UmwApi.Abstractions.Interfaces;

/// <summary>
/// Kontrakt dostępu do zasobu umów API Umowy NFZ.
/// Obsługuje endpointy: <c>/agreements</c>, <c>/agreements/{id}</c>,
/// <c>/plans/{id}</c> i <c>/months/{id}</c>.
/// </summary>
public interface IAgreementsClient
{
    /// <summary>
    /// Pobiera jedną stronę listy umów NFZ spełniających kryteria wyszukiwania.
    /// </summary>
    /// <param name="request">Parametry wyszukiwania umów.</param>
    /// <param name="cancellationToken">Token anulowania.</param>
    /// <returns>Strona wyników z listą umów.</returns>
    /// <exception cref="Exceptions.NfzApiException">Gdy API zwróci błąd.</exception>
    Task<IPage<Agreement>> GetPageAsync(GetAgreementsRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Iteruje przez wszystkie umowy NFZ spełniające kryteria wyszukiwania (wszystkie strony).
    /// </summary>
    /// <param name="request">Parametry wyszukiwania umów.</param>
    /// <param name="cancellationToken">Token anulowania.</param>
    /// <returns>Strumień wszystkich umów pasujących do zapytania.</returns>
    /// <exception cref="Exceptions.NfzApiException">Gdy API zwróci błąd w trakcie iteracji.</exception>
    IAsyncEnumerable<Agreement> GetAllAsync(GetAgreementsRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Pobiera szczegóły umowy NFZ wraz z powiązanymi planami i środkami ortopedycznymi.
    /// </summary>
    /// <param name="request">Parametry pobrania szczegółów umowy.</param>
    /// <param name="cancellationToken">Token anulowania.</param>
    /// <returns>Szczegóły umowy.</returns>
    /// <exception cref="Exceptions.NfzApiException">Gdy umowa o podanym ID nie istnieje (HTTP 400/404).</exception>
    Task<AgreementDetail> GetDetailAsync(GetAgreementDetailRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Pobiera szczegóły planu umowy wraz z miesięcznymi planami.
    /// </summary>
    /// <param name="request">Parametry pobrania szczegółów planu.</param>
    /// <param name="cancellationToken">Token anulowania.</param>
    /// <returns>Szczegóły planu.</returns>
    /// <exception cref="Exceptions.NfzApiException">Gdy plan o podanym ID nie istnieje.</exception>
    Task<PlanDetail> GetPlanDetailAsync(GetPlanDetailRequest request, CancellationToken cancellationToken = default);

    /// <summary>
    /// Pobiera szczegóły miesięcznego planu umowy wraz z pakietami.
    /// </summary>
    /// <param name="monthId">Identyfikator miesiąca planu (UUID).</param>
    /// <param name="cancellationToken">Token anulowania.</param>
    /// <returns>Szczegóły miesięcznego planu.</returns>
    /// <exception cref="Exceptions.NfzApiException">Gdy miesiąc o podanym ID nie istnieje.</exception>
    Task<MonthDetail> GetMonthDetailAsync(string monthId, CancellationToken cancellationToken = default);
}
