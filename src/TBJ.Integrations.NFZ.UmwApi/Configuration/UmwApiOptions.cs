namespace TBJ.Integrations.NFZ.UmwApi.Configuration;

/// <summary>
/// Opcje konfiguracyjne biblioteki klienta publicznego API umów NFZ (<c>app-umw-api</c>).
/// </summary>
public sealed class UmwApiOptions
{
    /// <summary>
    /// Nazwa sekcji konfiguracyjnej w <c>appsettings.json</c>.
    /// </summary>
    public const string SectionName = "NfzUmwApi";

    /// <summary>
    /// Bazowy adres URL API NFZ.
    /// Domyślnie: <c>https://api.nfz.gov.pl/app-umw-api</c>.
    /// </summary>
    public string BaseUrl { get; set; } = "https://api.nfz.gov.pl/app-umw-api";

    /// <summary>
    /// Timeout dla pojedynczego żądania HTTP do API NFZ.
    /// Domyślnie: 30 sekund.
    /// </summary>
    public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(30);

    /// <summary>
    /// Czas przechowywania wyników słownikowych w pamięci podręcznej.
    /// Dotyczy endpointów: <c>/contract-products</c>, <c>/orthopedic-supplies</c>,
    /// <c>/service-types</c> oraz <c>/available-years</c>.
    /// Domyślnie: 24 godziny.
    /// </summary>
    public TimeSpan DictionaryCacheTtl { get; set; } = TimeSpan.FromHours(24);

    /// <summary>
    /// Maksymalna liczba wpisów w pamięci podręcznej słowników.
    /// Domyślnie: 5000.
    /// </summary>
    public long DictionaryCacheSizeLimit { get; set; } = 5_000;
}
