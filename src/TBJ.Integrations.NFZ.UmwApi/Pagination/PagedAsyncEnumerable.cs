using TBJ.Integrations.NFZ.UmwApi.Requests;

namespace TBJ.Integrations.NFZ.UmwApi.Pagination;

/// <summary>
/// Pomocnik do iteracji przez wszystkie strony wyników z API Umowy NFZ.
/// Automatycznie pobiera kolejne strony aż do wyczerpania wyników.
/// </summary>
internal static class PagedAsyncEnumerable
{
    /// <summary>
    /// Iteruje przez wszystkie elementy ze wszystkich stron wyników.
    /// Automatycznie inkrementuje <see cref="PagedRequest.Page"/> i pobiera kolejne strony.
    /// </summary>
    /// <typeparam name="TRequest">Typ żądania dziedziczący po <see cref="PagedRequest"/>.</typeparam>
    /// <typeparam name="TItem">Typ elementu listy wyników.</typeparam>
    /// <param name="request">Obiekt żądania (będzie mutowany — <c>Page</c> jest inkrementowane).</param>
    /// <param name="fetchPage">Funkcja pobierająca pojedynczą stronę wyników.</param>
    /// <param name="cancellationToken">Token anulowania.</param>
    public static async IAsyncEnumerable<TItem> IterateAll<TRequest, TItem>(
        TRequest request,
        Func<TRequest, CancellationToken, Task<IPage<TItem>>> fetchPage,
        [System.Runtime.CompilerServices.EnumeratorCancellation] CancellationToken cancellationToken = default)
        where TRequest : PagedRequest
    {
        request.Page = 1;

        while (true)
        {
            var page = await fetchPage(request, cancellationToken);

            foreach (var item in page.Items)
                yield return item;

            if (!page.HasNextPage)
                break;

            request.Page++;
        }
    }
}
