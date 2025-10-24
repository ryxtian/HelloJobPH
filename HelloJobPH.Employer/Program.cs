
using Blazored.LocalStorage;
using HelloJobPH.Employer;
using HelloJobPH.Employer.JwtAuthStateProviders;
using HelloJobPH.Employer.Services.Authentication;
using HelloJobPH.Employer.Services.HumanResource;
using HelloJobPH.Employer.Services.JobPosting;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7172/") 
});

// ? Register Blazored LocalStorage
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddMudServices();


builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthStateProvider>();


builder.Services.AddScoped<IJobPosting, JobPosting>();
builder.Services.AddScoped<IHumanResource, HumanResource>();
builder.Services.AddScoped<IAuthService, AuthService>();



await builder.Build().RunAsync();
