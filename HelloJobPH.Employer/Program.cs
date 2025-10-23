
using HelloJobPH.Employer;
using HelloJobPH.Employer.JwtAuthStateProvider;
using HelloJobPH.Employer.Services.HumanResource;
using HelloJobPH.Employer.Services.JobPosting;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddAuthorizationCore(); // Needed for Blazor WASM auth
builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthStateProvider>();
builder.Services.AddScoped<IJobPosting, JobPosting>();
builder.Services.AddScoped<IHumanResource, HumanResource>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

await builder.Build().RunAsync();
