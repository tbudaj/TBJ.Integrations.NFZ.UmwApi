using TBJ.Integrations.NFZ.UmwApi.Models.Dictionaries;
using TBJ.Integrations.NFZ.UmwApi.Pagination;
using TBJ.Integrations.NFZ.UmwApi.Requests;

namespace TBJ.Integrations.NFZ.UmwApi.Abstractions.Interfaces;

/// <summary>
/// Kontrakt dostępu do słowników API Umowy NFZ.
/// Obsługuje endpointy: <c>/contract-products</c>, <c>/orthopedic-supplies</c>,
/// <c>/service-types</c> i <c>/providers</c>.
/// Wyniki słownikowe mogą być cachowane zgodnie z konfiguracją.
/// </summary>
public interface IDictionariesClient
{
    // Produkty kontraktowe

    /// <summary>Pobiera jedną stronę produktów kontraktowych.</summary>
    /// <param name="request">Parametry wyszukiwania produktów.</param>
    /// <param name="cancellationToken">Token anulowania.</param>
    /// <returns>Strona wyników z produktami kontraktowymi.</returns>
    Task<IPage<DictionaryEntry>> GetContractProductsPageAsync(GetContractProductsRequest request, CancellationToken cancellationToken = default);

    /// <summary>Iteruje przez wszystkie produkty kontraktowe (wszystkie strony).</summary>
    /// <param name="request">Parametry wyszukiwania produktów.</param>
    /// <param name="cancellationToken">Token anulowania.</param>
    /// <returns>Strumień wszystkich produktów kontraktowych.</returns>
    IAsyncEnumerable<DictionaryEntry> GetAllContractProductsAsync(GetContractProductsRequest request, CancellationToken cancellationToken = default);

    // Środki ortopedyczne

    /// <summary>Pobiera jedną stronę środków ortopedycznych.</summary>
    /// <param name="request">Parametry wyszukiwania środków.</param>
    /// <param name="cancellationToken">Token anulowania.</param>
    /// <returns>Strona wyników ze środkami ortopedycznymi.</returns>
    Task<IPage<DictionaryEntry>> GetOrthopedicSuppliesPageAsync(GetOrthopedicSuppliesRequest request, CancellationToken cancellationToken = default);

    /// <summary>Iteruje przez wszystkie środki ortopedyczne (wszystkie strony).</summary>
    /// <param name="request">Parametry wyszukiwania środków.</param>
    /// <param name="cancellationToken">Token anulowania.</param>
    /// <returns>Strumień wszystkich środków ortopedycznych.</returns>
    IAsyncEnumerable<DictionaryEntry> GetAllOrthopedicSuppliesAsync(GetOrthopedicSuppliesRequest request, CancellationToken cancellationToken = default);

    // Rodzaje świadczeń

    /// <summary>Pobiera jedną stronę rodzajów świadczeń.</summary>
    /// <param name="request">Parametry wyszukiwania rodzajów świadczeń.</param>
    /// <param name="cancellationToken">Token anulowania.</param>
    /// <returns>Strona wyników z rodzajami świadczeń.</returns>
    Task<IPage<DictionaryEntry>> GetServiceTypesPageAsync(GetServiceTypesRequest request, CancellationToken cancellationToken = default);

    /// <summary>Iteruje przez wszystkie rodzaje świadczeń (wszystkie strony).</summary>
    /// <param name="request">Parametry wyszukiwania rodzajów świadczeń.</param>
    /// <param name="cancellationToken">Token anulowania.</param>
    /// <returns>Strumień wszystkich rodzajów świadczeń.</returns>
    IAsyncEnumerable<DictionaryEntry> GetAllServiceTypesAsync(GetServiceTypesRequest request, CancellationToken cancellationToken = default);

    // Świadczeniodawcy

    /// <summary>Pobiera jedną stronę świadczeniodawców.</summary>
    /// <param name="request">Parametry wyszukiwania świadczeniodawców.</param>
    /// <param name="cancellationToken">Token anulowania.</param>
    /// <returns>Strona wyników ze świadczeniodawcami.</returns>
    Task<IPage<ProviderEntry>> GetProvidersPageAsync(GetProvidersRequest request, CancellationToken cancellationToken = default);

    /// <summary>Iteruje przez wszystkich świadczeniodawców (wszystkie strony).</summary>
    /// <param name="request">Parametry wyszukiwania świadczeniodawców.</param>
    /// <param name="cancellationToken">Token anulowania.</param>
    /// <returns>Strumień wszystkich świadczeniodawców.</returns>
    IAsyncEnumerable<ProviderEntry> GetAllProvidersAsync(GetProvidersRequest request, CancellationToken cancellationToken = default);
}
