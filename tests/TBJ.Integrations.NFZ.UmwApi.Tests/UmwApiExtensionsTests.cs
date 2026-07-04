using TBJ.Integrations.NFZ.UmwApi.Abstractions.Interfaces;
using TBJ.Integrations.NFZ.UmwApi.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TBJ.Integrations.NFZ.UmwApi.Tests;

/// <summary>
/// Testy rejestracji DI dla biblioteki klienta API Umowy NFZ.
/// </summary>
public class UmwApiExtensionsTests
{
    [Fact]
    public void AddNfzUmwApi_RejestrujeWszystkichKlientow()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddLogging();

        // Act
        services.AddNfzUmwApi(opt =>
        {
            opt.BaseUrl = "https://api.nfz.gov.pl/app-umw-api";
            opt.Timeout = TimeSpan.FromSeconds(30);
        });

        var provider = services.BuildServiceProvider();

        // Assert
        Assert.NotNull(provider.GetService<IAgreementsClient>());
        Assert.NotNull(provider.GetService<IDictionariesClient>());
        Assert.NotNull(provider.GetService<IInfoClient>());
    }

    [Fact]
    public void AddNfzUmwApi_ZKonfiguracja_RejestrujeOpcje()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddLogging();
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["NfzUmwApi:BaseUrl"] = "https://api.nfz.gov.pl/app-umw-api",
                ["NfzUmwApi:Timeout"] = "00:00:45"
            })
            .Build();

        // Act
        services.AddNfzUmwApi(configuration);
        var provider = services.BuildServiceProvider();
        var options = provider.GetRequiredService<UmwApiOptions>();

        // Assert
        Assert.Equal("https://api.nfz.gov.pl/app-umw-api", options.BaseUrl);
        Assert.Equal(TimeSpan.FromSeconds(45), options.Timeout);
    }

    [Fact]
    public void AddNfzUmwApi_BrakBaseUrl_RzucaWyjatek()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act & Assert
        Assert.Throws<InvalidOperationException>(() =>
            services.AddNfzUmwApi(opt => opt.BaseUrl = string.Empty));
    }
}
