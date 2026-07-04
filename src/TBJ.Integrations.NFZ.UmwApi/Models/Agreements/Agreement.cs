using System.Text.Json.Serialization;
using TBJ.Integrations.NFZ.UmwApi.Models.Common;

namespace TBJ.Integrations.NFZ.UmwApi.Models.Agreements;

/// <summary>
/// Umowa zawarta przez NFZ ze świadczeniodawcą (element listy z <c>/agreements</c>).
/// </summary>
public sealed class Agreement
{
    /// <summary>Identyfikator umowy (UUID).</summary>
    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>Typ obiektu — zawsze "agreement".</summary>
    [JsonPropertyName("type")]
    public string? Type { get; init; }

    /// <summary>Atrybuty umowy.</summary>
    [JsonPropertyName("attributes")]
    public AgreementAttributes? Attributes { get; init; }

    /// <summary>Linki powiązane z umową (np. link do szczegółów).</summary>
    [JsonPropertyName("links")]
    public PageLinks? Links { get; init; }
}
