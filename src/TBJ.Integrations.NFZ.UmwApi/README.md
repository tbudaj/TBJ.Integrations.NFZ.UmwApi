# TBJ.Integrations.NFZ.UmwApi

Typowany klient .NET dla publicznego API umów NFZ (`app-umw-api`).

Dostarcza async klientów HTTP do pobierania danych o umowach, planach, środkach ortopedycznych, produktach kontraktowych i świadczeniodawcach. API NFZ nie wymaga uwierzytelniania.

## Wymagania

- .NET 8, 9 lub 10
- Dostęp do internetu (publiczne API NFZ — bez klucza API)

## Rejestracja w DI

```csharp
builder.Services.AddNfzUmwApi(opt =>
{
    opt.BaseUrl = "https://api.nfz.gov.pl/app-umw-api";
    opt.Timeout = TimeSpan.FromSeconds(30);
    opt.DictionaryCacheTtl = TimeSpan.FromHours(24);
});
```

Z `appsettings.json`:

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

- `IAgreementsClient` — umowy, plany, miesiące
- `IDictionariesClient` — słowniki: produkty, środki ortopedyczne, rodzaje świadczeń, świadczeniodawcy
- `IInfoClient` — dostępne lata danych

Szczegółową dokumentację znajdziesz w `docs/TBJ.Integrations.NFZ.UmwApi/TBJ.Integrations.NFZ.UmwApi.md`.
