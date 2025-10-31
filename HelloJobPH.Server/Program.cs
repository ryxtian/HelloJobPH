using AutoMapper;
using HelloJobPH.Employer.JwtAuthStateProviders;
using HelloJobPH.Server.Data;
using HelloJobPH.Server.Mapper;
using HelloJobPH.Server.Service.Auth;
using HelloJobPH.Server.Service.Candidate;
using HelloJobPH.Server.Service.Dashboard;
using HelloJobPH.Server.Service.Email;
using HelloJobPH.Server.Service.HumanResource;
using HelloJobPH.Server.Service.Interview;
using HelloJobPH.Server.Service.JobPost;
using HelloJobPH.Server.Service.Overview;
using HelloJobPH.Server.Service.UserAccountRepository;
using HelloJobPH.Server.Services;
using HelloJobPH.Shared.Model;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//var jwtKey = builder.Configuration["Jwt:Key"];
//var jwtIssuer = builder.Configuration["Jwt:Issuer"];
//var jwtAudience = builder.Configuration["Jwt:Audience"];

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = jwtIssuer,
//            ValidAudience = jwtAudience,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!))
//        };
//    });

var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey!))
        };

        // ? Read token from cookie instead of Authorization header
        options.Events = new Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                // Check if the JWT is stored in the "jwtToken" cookie
                var token = context.Request.Cookies["jwtToken"];

                if (!string.IsNullOrEmpty(token))
                {
                    context.Token = token; // ? Tell ASP.NET Core to use this token
                }

                return Task.CompletedTask;
            }
        };
    });



builder.Services.Configure<EmailService.EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<IEmailService, EmailService>();


builder.Services.AddAuthorization();
builder.Services.AddAuthorizationCore();


builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJobPostService, JobPostService>();
builder.Services.AddScoped<IHumanResourceService, HumanResourceService>();
builder.Services.AddScoped<ICandidateService, CandidateService>();
builder.Services.AddScoped<IInterviewService, InterviewService>();
builder.Services.AddScoped<IOverviewService, OverviewService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
//builder.Services.AddScoped<IUserAccountRepository, UserAccountRepository>();
//builder.Services.AddScoped<IApplicantRepository, ApplicantRepository>();


//builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthStateProvider>();

builder.Services.AddHttpContextAccessor();

//builder.Services.AddTransient<IConfiguration, IConfiguration>();
builder.Services.AddAutoMapper(typeof(Mapping).Assembly);
//builder.Services.AddControllers().AddJsonOptions(options =>
//{
//    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
//});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();