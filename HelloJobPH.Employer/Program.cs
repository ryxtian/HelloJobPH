
using Blazored.LocalStorage;
using HelloJobPH.Employer;
using HelloJobPH.Employer.JwtAuthStateProviders;
using HelloJobPH.Employer.Services.AuditLog;
using HelloJobPH.Employer.Services.Authentication;
using HelloJobPH.Employer.Services.Candidate;
using HelloJobPH.Employer.Services.Chat;
using HelloJobPH.Employer.Services.Dashboard;
using HelloJobPH.Employer.Services.HumanResource;
using HelloJobPH.Employer.Services.Interview;
using HelloJobPH.Employer.Services.JobPosting;
using HelloJobPH.Employer.Services.Overview;
using HelloJobPH.Employer.Services.Resume;
using HelloJobPH.Employer.Services.SuperAdmin;
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

builder.Services.AddScoped<JwtAuthStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<JwtAuthStateProvider>());



builder.Services.AddScoped<IJobPosting, JobPosting>();
builder.Services.AddScoped<IHumanResource, HumanResource>();
builder.Services.AddScoped<IClientIAuthService, ClientAuthService>();
builder.Services.AddScoped<IClientCandidateService, ClientCandidateService>();
builder.Services.AddScoped<IClientInterviewService, ClientInterviewService>();
builder.Services.AddScoped<IClientOverview, ClientOverview>();
builder.Services.AddScoped<IClientDashboardService, ClientDashboardService>();
builder.Services.AddScoped<IClientSuperAdminService, ClientSuperAdminService>();
builder.Services.AddScoped<IClientChatService, ClientChatService>();
builder.Services.AddScoped<IClientAuditLogService, ClientAuditLogService>();
builder.Services.AddScoped<IClientResumeService, ClientResumeService>();


await builder.Build().RunAsync();
