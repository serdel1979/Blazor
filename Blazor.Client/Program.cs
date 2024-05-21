using Blazor.Client;
using Blazor.Client.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5216") });

builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();
builder.Services.AddScoped<IDepartamentoService, DepartamentoService>();

builder.Services.AddSweetAlert2();

await builder.Build().RunAsync();
