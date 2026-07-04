# TBJ.Integrations.NFZ.UmwApi

[![build](https://github.com/tbudaj/TBJ.Integrations.NFZ.UmwApi/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/tbudaj/TBJ.Integrations.NFZ.UmwApi/actions/workflows/build-and-test.yml)
[![NuGet](https://img.shields.io/nuget/v/TBJ.Integrations.NFZ.UmwApi)](https://www.nuget.org/packages/TBJ.Integrations.NFZ.UmwApi)

Typowany klient .NET 8, 9, 10 dla publicznego API umów NFZ (`app-umw-api`).

Pakiet dostarcza async klientów HTTP do pobierania danych o umowach, planach, środkach ortopedycznych, produktach kontraktowych, rodzajach świadczeń, świadczeniodawcach oraz dostępnych latach danych. API NFZ jest publiczne i nie wymaga uwierzytelniania.

## Spis treści

- [Wymagania](#wymagania)
- [Instalacja](#instalacja)
- [Rejestracja w DI](#rejestracja-w-di)
- [Klienci](#klienci)
- [Paginacja i strumieniowanie](#paginacja-i-strumieniowanie)
- [Przykłady użycia](#przykłady-użycia)
- [Obsługa błędów](#obsługa-błędów)
- [Cykl życia serwisów](#cykl-życia-serwisów)
- [Wersjonowanie i publikacja](#wersjonowanie-i-publikacja)
- [Repozytorium i paczka](#repozytorium-i-paczka)

## Wymagania

- .NET 8, 9 lub 10
- Dostęp do internetu (publiczne API NFZ — bez klucza API)

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

Możesz też skonfigurować klienta inline:

```csharp
builder.Services.AddNfzUmwApi(opt =>
{
    opt.BaseUrl = "https://api.nfz.gov.pl/app-umw-api";
    opt.Timeout = TimeSpan.FromSeconds(30);
    opt.DictionaryCacheTtl = TimeSpan.FromHours(24);
    opt.DictionaryCacheSizeLimit = 5000;
});
```

## Klienci

| Klient | Odpowiedzialność |
|---|---|
| `IAgreementsClient` | Umowy, plany, miesięczne plany |
| `IDictionariesClient` | Produkty kontraktowe, środki ortopedyczne, rodzaje świadczeń, świadczeniodawcy |
| `IInfoClient` | Dostępne lata danych |

## Paginacja i strumieniowanie

Metody `GetPageAsync` zwracają stronę wyników wraz z całkowitą liczbą rekordów. Metody `GetAllAsync` zwracają `IAsyncEnumerable<T>` i automatycznie przechodzą przez wszystkie strony:

```csharp
await foreach (var agreement in agreements.GetAllAsync(new GetAgreementsRequest
{
    Branch = "01",
    Year = 2024
}))
{
    Console.WriteLine(agreement.Attributes?.ProviderName);
}
```

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

Szczegółową dokumentację i dodatkowe przykłady znajdziesz w folderze `docs/TBJ.Integrations.NFZ.UmwApi`.

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

## Wersjonowanie i publikacja

Projekt używa [MinVer](https://github.com/adamralph/minver) do automatycznego wersjonowania przez tagi Git.

```bash
git tag nfz-umwapi/v1.0.1
git push origin nfz-umwapi/v1.0.1
```

Wypchnięcie tagu uruchamia workflow `release.yml`, który publikuje paczkę na NuGet.org (Trusted Publishing) oraz GitHub Packages i tworzy zamrożoną gałąź `release/nfz-umwapi/1.0.1`.

Wymagane sekrety w ustawieniach repozytorium: `NUGET_USER` oraz `RELEASE_PAT`.

## Repozytorium i paczka

- Repozytorium: <https://github.com/tbudaj/TBJ.Integrations.NFZ.UmwApi>
- Paczka NuGet: <https://www.nuget.org/packages/TBJ.Integrations.NFZ.UmwApi>

## Licencja

[MIT](LICENSE)
