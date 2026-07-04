using TBJ.Integrations.NFZ.UmwApi.Requests;

namespace TBJ.Integrations.NFZ.UmwApi.Internal;

/// <summary>
/// Buduje query string dla zapytań do API Umowy NFZ na podstawie obiektów <see cref="PagedRequest"/>.
/// </summary>
internal static class QueryBuilder
{
    /// <summary>
    /// Buduje query string dla zapytania o listę umów.
    /// </summary>
    /// <param name="r">Parametry wyszukiwania umów.</param>
    /// <returns>Query string zaczynający się od znaku zapytania lub pusty ciąg.</returns>
    public static string Build(GetAgreementsRequest r)
    {
        var q = new QueryStringBuilder()
            .Add("branch", r.Branch)
            .Add("year", r.Year)
            .AddIfNotNull("service-type", r.ServiceType)
            .AddIfNotNull("provider-code", r.ProviderCode)
            .AddIfNotNull("provider-name", r.ProviderName)
            .AddIfNotNull("provider-nip", r.ProviderNip)
            .AddIfNotNull("provider-regon", r.ProviderRegon)
            .AddIfNotNull("place", r.Place)
            .AddIfNotNull("updated-at", r.UpdatedAt)
            .AddPaging(r);
        return q.Build();
    }

    /// <summary>Buduje query string dla szczegółów umowy.</summary>
    public static string Build(GetAgreementDetailRequest r)
        => new QueryStringBuilder().AddPaging(r).Build();

    /// <summary>Buduje query string dla szczegółów planu.</summary>
    public static string Build(GetPlanDetailRequest r)
        => new QueryStringBuilder().AddPaging(r).Build();

    /// <summary>Buduje query string dla produktów kontraktowych.</summary>
    public static string Build(GetContractProductsRequest r)
    {
        var q = new QueryStringBuilder()
            .Add("year", r.Year)
            .AddIfNotNull("branch", r.Branch)
            .AddIfNotNull("code", r.Code)
            .AddIfNotNull("name", r.Name)
            .AddPaging(r);
        return q.Build();
    }

    /// <summary>Buduje query string dla środków ortopedycznych.</summary>
    public static string Build(GetOrthopedicSuppliesRequest r)
    {
        var q = new QueryStringBuilder()
            .Add("year", r.Year)
            .AddIfNotNull("branch", r.Branch)
            .AddIfNotNull("code", r.Code)
            .AddIfNotNull("name", r.Name)
            .AddPaging(r);
        return q.Build();
    }

    /// <summary>Buduje query string dla rodzajów świadczeń.</summary>
    public static string Build(GetServiceTypesRequest r)
    {
        var q = new QueryStringBuilder()
            .Add("year", r.Year)
            .AddIfNotNull("branch", r.Branch)
            .AddPaging(r);
        return q.Build();
    }

    /// <summary>Buduje query string dla świadczeniodawców.</summary>
    public static string Build(GetProvidersRequest r)
    {
        var q = new QueryStringBuilder()
            .Add("branch", r.Branch)
            .Add("year", r.Year)
            .AddIfNotNull("code", r.Code)
            .AddIfNotNull("name", r.Name)
            .AddIfNotNull("nip", r.Nip)
            .AddIfNotNull("regon", r.Regon)
            .AddIfNotNull("place", r.Place)
            .AddPaging(r);
        return q.Build();
    }

    // Wewnętrzny budowniczy query string

    private sealed class QueryStringBuilder
    {
        private readonly List<(string Key, string Value)> _params = [];

        public QueryStringBuilder Add(string key, string value)
        {
            _params.Add((key, value));
            return this;
        }

        public QueryStringBuilder Add(string key, int value)
        {
            _params.Add((key, value.ToString()));
            return this;
        }

        public QueryStringBuilder AddIfNotNull(string key, string? value)
        {
            if (!string.IsNullOrWhiteSpace(value))
                _params.Add((key, value));
            return this;
        }

        public QueryStringBuilder AddPaging(PagedRequest r)
        {
            _params.Add(("page", r.Page.ToString()));
            _params.Add(("limit", r.Limit.ToString()));
            _params.Add(("format", "json"));
            return this;
        }

        public string Build()
        {
            if (_params.Count == 0)
                return string.Empty;

            var parts = _params.Select(p => $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value)}");
            return "?" + string.Join("&", parts);
        }
    }
}
