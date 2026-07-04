using TBJ.Integrations.NFZ.UmwApi.Models.Common;

namespace TBJ.Integrations.NFZ.UmwApi.Pagination;

/// <summary>
/// Konkretna implementacja <see cref="IPage{T}"/> — strona wyników z API Umowy NFZ.
/// </summary>
internal sealed class Page<T> : IPage<T>
{
    public IReadOnlyList<T> Items { get; }
    public PageMeta Meta { get; }
    public PageLinks Links { get; }

    public int TotalCount => Meta.Count ?? 0;
    public int CurrentPage => Meta.Page ?? 1;
    public int PageSize => Meta.Limit ?? Items.Count;
    public int TotalPages => PageSize > 0 ? (int)Math.Ceiling(TotalCount / (double)PageSize) : 0;
    public bool HasNextPage => Links.Next is not null;
    public bool HasPreviousPage => Links.Prev is not null;

    public Page(IReadOnlyList<T> items, PageMeta meta, PageLinks links)
    {
        Items = items;
        Meta = meta;
        Links = links;
    }
}
