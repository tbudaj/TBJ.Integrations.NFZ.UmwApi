# TBJ.Integrations.NFZ.UmwApi

Typowany klient .NET dla publicznego API umów NFZ (`app-umw-api`).

## Instalacja

```bash
dotnet add package TBJ.Integrations.NFZ.UmwApi
```

## Rejestracja w DI

```csharp
builder.Services.AddNfzUmwApi(builder.Configuration);
```

```json
{
  "NfzUmwApi": {
    "BaseUrl": "https://api.nfz.gov.pl/app-umw-api",
    "Timeout": "00:00:30",
    "DictionaryCacheTtl": "24:00:00",
    "DictionaryCacheSizeLimit": 5000
  }
}
```

## Klienci

| Klient | Odpowiedzialność |
|---|---|
| `IAgreementsClient` | Umowy, plany, miesięczne plany |
| `IDictionariesClient` | Produkty kontraktowe, środki ortopedyczne, rodzaje świadczeń, świadczeniodawcy |
| `IInfoClient` | Dostępne lata danych |

## Przykłady użycia

```csharp
public class NfzService(IAgreementsClient agreements, IDictionariesClient dictionaries)
{
    public async Task ShowAgreementsAsync(string branch, int year)
    {
        var page = await agreements.GetPageAsync(new GetAgreementsRequest
        {
            Branch = branch,
            Year = year
        });

        Console.WriteLine($"Znaleziono {page.TotalCount} umów");

        await foreach (var agreement in agreements.GetAllAsync(new GetAgreementsRequest
        {
            Branch = branch,
            Year = year
        }))
        {
            Console.WriteLine(agreement.Attributes?.ProviderName);
        }
    }
}
```

## Obsługa błędów

Wszystkie błędy HTTP oraz błędy semantyczne API NFZ są konwertowane do wyjątku `NfzApiException`.

```csharp
try
{
    var detail = await agreements.GetDetailAsync(new GetAgreementDetailRequest { Id = "nieistniejące-id" });
}
catch (NfzApiException ex)
{
    Console.WriteLine($"HTTP {ex.HttpStatusCode}: {ex.Message}");
}
```

## Cykl życia serwisów

| Serwis | Lifetime | Uzasadnienie |
|---|---|---|
| `IAgreementsClient` | `Scoped` | Per-request |
| `IDictionariesClient` | `Scoped` | Per-request, korzysta z cache |
| `IInfoClient` | `Scoped` | Per-request, korzysta z cache |
| `IMemoryCache` | `Singleton` | Współdzielony cache słowników |
| `HttpClient` | przez `IHttpClientFactory` | Automatyczna rotacja handlerów |
