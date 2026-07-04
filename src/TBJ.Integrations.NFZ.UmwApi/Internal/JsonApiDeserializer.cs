using System.Text.Json;
using System.Text.Json.Nodes;
using TBJ.Integrations.NFZ.UmwApi.Abstractions.Exceptions;
using TBJ.Integrations.NFZ.UmwApi.Models.Agreements;
using TBJ.Integrations.NFZ.UmwApi.Models.Common;
using TBJ.Integrations.NFZ.UmwApi.Models.Dictionaries;
using TBJ.Integrations.NFZ.UmwApi.Pagination;

namespace TBJ.Integrations.NFZ.UmwApi.Internal;

/// <summary>
/// Deserializuje odpowiedzi API Umowy NFZ (format JSON API) do typowanych obiektów C#.
/// Obsługuje sekcje <c>meta</c>, <c>links</c>, <c>data</c> i <c>errors</c>.
/// </summary>
internal static class JsonApiDeserializer
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    // Listy umów

    /// <summary>Deserializuje odpowiedź <c>/agreements</c> do strony listy umów.</summary>
    public static IPage<Agreement> DeserializeAgreementList(string json)
    {
        var root = ParseRoot(json);
        var meta = DeserializeMeta(root);
        var links = DeserializeLinks(root);
        var data = root["data"]?["agreements"]
            .Deserialize<List<Agreement>>(JsonOptions) ?? [];
        return new Page<Agreement>(data, meta, links);
    }

    /// <summary>Deserializuje odpowiedź <c>/agreements/{id}</c> do szczegółów umowy.</summary>
    public static AgreementDetail DeserializeAgreementDetail(string json)
    {
        var root = ParseRoot(json);
        var dataNode = root["data"] ?? throw new NfzApiException("Brak sekcji 'data' w odpowiedzi /agreements/{id}.");

        var agreement = dataNode["agreement"]
            .Deserialize<Agreement>(JsonOptions);
        var plans = dataNode["plans"]
            .Deserialize<List<Plan>>(JsonOptions) ?? [];
        var supplies = dataNode["orthopedic-supplies"]
            .Deserialize<List<OrthopedicSupplyItem>>(JsonOptions) ?? [];

        return new AgreementDetail
        {
            Agreement = agreement,
            Plans = plans,
            OrthopedicSupplies = supplies
        };
    }

    /// <summary>Deserializuje odpowiedź <c>/plans/{id}</c> do szczegółów planu.</summary>
    public static PlanDetail DeserializePlanDetail(string json)
    {
        var root = ParseRoot(json);
        var dataNode = root["data"] ?? throw new NfzApiException("Brak sekcji 'data' w odpowiedzi /plans/{id}.");

        var plan = dataNode["plan"].Deserialize<Plan>(JsonOptions);
        var months = dataNode["months"].Deserialize<List<Month>>(JsonOptions) ?? [];

        return new PlanDetail { Plan = plan, Months = months };
    }

    /// <summary>Deserializuje odpowiedź <c>/months/{id}</c> do szczegółów miesiąca planu.</summary>
    public static MonthDetail DeserializeMonthDetail(string json)
    {
        var root = ParseRoot(json);
        var dataNode = root["data"] ?? throw new NfzApiException("Brak sekcji 'data' w odpowiedzi /months/{id}.");

        var header = dataNode["header"].Deserialize<MonthHeader>(JsonOptions);
        var month = dataNode["month"].Deserialize<Month>(JsonOptions);
        var packages = dataNode["packages"].Deserialize<List<Package>>(JsonOptions) ?? [];

        return new MonthDetail { Header = header, Month = month, Packages = packages };
    }

    // Słowniki

    /// <summary>Deserializuje odpowiedź słownikową (contract-products, orthopedic-supplies, service-types) do strony listy wpisów.</summary>
    public static IPage<DictionaryEntry> DeserializeDictionaryEntryList(string json)
    {
        var root = ParseRoot(json);
        var meta = DeserializeMeta(root);
        var links = DeserializeLinks(root);
        var data = root["data"]?["entries"]
            .Deserialize<List<DictionaryEntry>>(JsonOptions) ?? [];
        return new Page<DictionaryEntry>(data, meta, links);
    }

    /// <summary>Deserializuje odpowiedź <c>/providers</c> do strony listy świadczeniodawców.</summary>
    public static IPage<ProviderEntry> DeserializeProviderList(string json)
    {
        var root = ParseRoot(json);
        var meta = DeserializeMeta(root);
        var links = DeserializeLinks(root);
        var data = root["data"]?["entries"]
            .Deserialize<List<ProviderEntry>>(JsonOptions) ?? [];
        return new Page<ProviderEntry>(data, meta, links);
    }

    /// <summary>Deserializuje odpowiedź <c>/available-years</c>.</summary>
    public static AvailableYears DeserializeAvailableYears(string json)
    {
        return JsonSerializer.Deserialize<AvailableYears>(json, JsonOptions)
               ?? throw new NfzApiException("Nie udało się zdeserializować odpowiedzi /available-years.");
    }

    // Błędy

    /// <summary>
    /// Próbuje wyodrębnić błędy API z sekcji <c>errors</c>.
    /// Zwraca pustą listę jeśli sekcja nie istnieje lub nie można jej sparsować.
    /// </summary>
    public static IReadOnlyList<ApiError> TryDeserializeErrors(string json)
    {
        try
        {
            var root = JsonNode.Parse(json);
            var errors = root?["errors"].Deserialize<List<ApiError>>(JsonOptions);
            return errors ?? [];
        }
        catch
        {
            return [];
        }
    }

    // Pomocnicze

    private static JsonNode ParseRoot(string json)
    {
        return JsonNode.Parse(json)
               ?? throw new NfzApiException("Odpowiedź API jest pusta lub nie jest poprawnym JSON.");
    }

    private static PageMeta DeserializeMeta(JsonNode root)
        => root["meta"].Deserialize<PageMeta>(JsonOptions) ?? new PageMeta();

    private static PageLinks DeserializeLinks(JsonNode root)
        => root["links"].Deserialize<PageLinks>(JsonOptions) ?? new PageLinks();
}
