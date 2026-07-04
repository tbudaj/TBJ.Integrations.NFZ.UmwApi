using TBJ.Integrations.NFZ.UmwApi.Models.Common;

namespace TBJ.Integrations.NFZ.UmwApi.Pagination;

/// <summary>
/// Reprezentuje jedną stronę wyników z API Umowy NFZ.
/// Zawiera dane, metadane i linki HATEOAS do nawigacji.
/// </summary>
/// <typeparam name="T">Typ elementu listy wyników.</typeparam>
public interface IPage<T>
{
    /// <summary>Elementy na bieżącej stronie.</summary>
    IReadOnlyList<T> Items { get; }

    /// <summary>Metadane odpowiedzi (count, page, limit, context, ...).</summary>
    PageMeta Meta { get; }

    /// <summary>Linki HATEOAS (first, prev, self, next, last).</summary>
    PageLinks Links { get; }

    /// <summary>Łączna liczba wyników pasujących do zapytania.</summary>
    int TotalCount { get; }

    /// <summary>Numer bieżącej strony (od 1).</summary>
    int CurrentPage { get; }

    /// <summary>Liczba wyników na stronie.</summary>
    int PageSize { get; }

    /// <summary>Łączna liczba stron.</summary>
    int TotalPages { get; }

    /// <summary>Czy istnieje następna strona wyników.</summary>
    bool HasNextPage { get; }

    /// <summary>Czy istnieje poprzednia strona wyników.</summary>
    bool HasPreviousPage { get; }
}
