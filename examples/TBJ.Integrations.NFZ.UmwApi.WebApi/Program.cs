using TBJ.Integrations.NFZ.UmwApi;
using TBJ.Integrations.NFZ.UmwApi.Abstractions.Interfaces;
using TBJ.Integrations.NFZ.UmwApi.Requests;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddNfzUmwApi(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "TBJ.Integrations.NFZ.UmwApi WebApi",
        Version = "v1",
        Description = "Przykładowe API demonstrujące użycie pakietu TBJ.Integrations.NFZ.UmwApi."
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

/// <summary>Pobiera dostępne lata danych z API NFZ.</summary>
app.MapGet("/api/available-years", async (IInfoClient info) =>
{
    var years = await info.GetAvailableYearsAsync();
    return Results.Ok(years);
})
.WithName("GetAvailableYears")
.WithOpenApi();

/// <summary>Pobiera pierwszą stronę umów dla wskazanego oddziału i roku.</summary>
app.MapGet("/api/agreements", async (IAgreementsClient agreements, string branch, int year) =>
{
    var page = await agreements.GetPageAsync(new GetAgreementsRequest
    {
        Branch = branch,
        Year = year
    });
    return Results.Ok(page);
})
.WithName("GetAgreements")
.WithOpenApi();

/// <summary>Pobiera szczegóły umowy NFZ.</summary>
app.MapGet("/api/agreements/{id}", async (IAgreementsClient agreements, string id) =>
{
    var detail = await agreements.GetDetailAsync(new GetAgreementDetailRequest { Id = id });
    return Results.Ok(detail);
})
.WithName("GetAgreementDetail")
.WithOpenApi();

app.Run();
