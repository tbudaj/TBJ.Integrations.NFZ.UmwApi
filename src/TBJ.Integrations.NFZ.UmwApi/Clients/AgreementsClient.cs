using TBJ.Integrations.NFZ.UmwApi.Abstractions.Interfaces;
using TBJ.Integrations.NFZ.UmwApi.Internal;
using TBJ.Integrations.NFZ.UmwApi.Models.Agreements;
using TBJ.Integrations.NFZ.UmwApi.Pagination;
using TBJ.Integrations.NFZ.UmwApi.Requests;
using Microsoft.Extensions.Logging;

namespace TBJ.Integrations.NFZ.UmwApi.Clients;

/// <summary>
/// Implementacja <see cref="IAgreementsClient"/> — obsługuje zasoby umów NFZ.
/// </summary>
internal sealed class AgreementsClient : IAgreementsClient
{
    private readonly UmwApiHttpClient _http;
    private readonly ILogger<AgreementsClient> _logger;

    private const string AgreementsEndpoint = "agreements";
    private const string PlansEndpoint = "plans";
    private const string MonthsEndpoint = "months";

    public AgreementsClient(UmwApiHttpClient http, ILogger<AgreementsClient> logger)
    {
        _http = http;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<IPage<Agreement>> GetPageAsync(GetAgreementsRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Pobieranie strony umów NFZ — oddział {Branch}, rok {Year}, strona {Page}",
            request.Branch,
            request.Year,
            request.Page);
        _logger.LogDebug(
            "UmwApi: GetAgreementsPage — Branch: {Branch}, Year: {Year}, Page: {Page}",
            request.Branch,
            request.Year,
            request.Page);
        var qs = QueryBuilder.Build(request);
        var json = await _http.GetAsync(AgreementsEndpoint, qs, cancellationToken);
        return JsonApiDeserializer.DeserializeAgreementList(json);
    }

    /// <inheritdoc />
    public IAsyncEnumerable<Agreement> GetAllAsync(GetAgreementsRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Rozpoczęcie iteracji po wszystkich umowach NFZ — oddział {Branch}, rok {Year}",
            request.Branch,
            request.Year);
        _logger.LogDebug(
            "UmwApi: GetAllAgreements — Branch: {Branch}, Year: {Year}",
            request.Branch,
            request.Year);
        return PagedAsyncEnumerable.IterateAll(request, GetPageAsync, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<AgreementDetail> GetDetailAsync(GetAgreementDetailRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Pobieranie szczegółów umowy NFZ — Id: {Id}", request.Id);
        _logger.LogDebug("UmwApi: GetAgreementDetail — Id: {Id}", request.Id);
        var qs = QueryBuilder.Build(request);
        var json = await _http.GetAsync($"{AgreementsEndpoint}/{request.Id}", qs, cancellationToken);
        return JsonApiDeserializer.DeserializeAgreementDetail(json);
    }

    /// <inheritdoc />
    public async Task<PlanDetail> GetPlanDetailAsync(GetPlanDetailRequest request, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Pobieranie szczegółów planu NFZ — Id: {Id}", request.Id);
        _logger.LogDebug("UmwApi: GetPlanDetail — Id: {Id}", request.Id);
        var qs = QueryBuilder.Build(request);
        var json = await _http.GetAsync($"{PlansEndpoint}/{request.Id}", qs, cancellationToken);
        return JsonApiDeserializer.DeserializePlanDetail(json);
    }

    /// <inheritdoc />
    public async Task<MonthDetail> GetMonthDetailAsync(string monthId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Pobieranie szczegółów miesiąca planu NFZ — Id: {Id}", monthId);
        _logger.LogDebug("UmwApi: GetMonthDetail — Id: {Id}", monthId);
        var json = await _http.GetAsync($"{MonthsEndpoint}/{monthId}", "?format=json", cancellationToken);
        return JsonApiDeserializer.DeserializeMonthDetail(json);
    }
}
