using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using Reciclaje.Info.Client;
using Reciclaje.Info.Client.Services;
using Reciclaje.Info.Shared.Types;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});
// Inserción de dependencias de los servicios Reciclaje.Info
builder.Services.AddScoped<IGeolocationService,GeolocationService>();
builder.Services.AddScoped<IGenericGeoService<PuntosLimpiosType>, PuntoLimpioService>();
builder.Services.AddScoped<IGenericGeoService<ContenedorType>, ContenedorService>();
builder.Services.AddScoped<IEquipamientoService, EquipamientoService>();



await builder.Build().RunAsync();