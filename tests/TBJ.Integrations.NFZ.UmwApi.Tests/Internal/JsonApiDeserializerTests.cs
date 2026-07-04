using TBJ.Integrations.NFZ.UmwApi.Internal;

namespace TBJ.Integrations.NFZ.UmwApi.Tests.Internal;

/// <summary>
/// Testy deserializacji odpowiedzi API Umowy NFZ.
/// </summary>
public class JsonApiDeserializerTests
{
    [Fact]
    public void DeserializeAvailableYears_ZwracaZakresLat()
    {
        // Arrange
        const string json = "{ \"start-year\": 2020, \"end-year\": 2024 }";

        // Act
        var result = JsonApiDeserializer.DeserializeAvailableYears(json);

        // Assert
        Assert.Equal(2020, result.StartYear);
        Assert.Equal(2024, result.EndYear);
    }

    [Fact]
    public void DeserializeAgreementList_ZwracaStroneZUmowami()
    {
        // Arrange
        const string json = """
        {
          "meta": { "count": 1, "page": 1, "limit": 25 },
          "links": { "self": "https://api.nfz.gov.pl/app-umw-api/agreements?branch=07&year=2024&page=1&limit=25&format=json" },
          "data": {
            "agreements": [
              { "id": "uuid-1", "type": "agreement", "attributes": { "code": "U/2024/1" } }
            ]
          }
        }
        """;

        // Act
        var page = JsonApiDeserializer.DeserializeAgreementList(json);

        // Assert
        Assert.Single(page.Items);
        Assert.Equal(1, page.TotalCount);
        Assert.Equal("uuid-1", page.Items[0].Id);
        Assert.Equal("U/2024/1", page.Items[0].Attributes?.Code);
    }
}
