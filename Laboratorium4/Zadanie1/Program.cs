using Zadanie1.Components;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.Extensions.ML;
using SampleClassification.Model;

// 1. NAJPIERW TWORZYMY BUILDER
var builder = WebApplication.CreateBuilder(args);

// 2. POTEM DODAJEMY USŁUGI (Services)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Konfiguracja ML.NET
builder.Services.AddPredictionEnginePool<ModelInput, ModelOutput>()
    .FromFile("../SampleClassification/SampleClassification.Model/MLModel.zip");

// Konfiguracja Certyfikatów
builder.Services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
    .AddCertificate();

// 3. BUDUJEMY APLIKACJĘ
var app = builder.Build();

// 4. KONFIGURUJEMY PIPELINE (Middleware)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

// WAŻNE: Autoryzacja musi być przed Antiforgery i Mapowaniem
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();
app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();