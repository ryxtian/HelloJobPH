using AutoMapper;
using HelloJobPH.Employer.JwtAuthStateProviders;
using HelloJobPH.Server.Data;
using HelloJobPH.Server.Mapper;
using HelloJobPH.Server.Service.Auth;
using HelloJobPH.Server.Service.Candidate;
using HelloJobPH.Server.Service.HumanResource;
using HelloJobPH.Server.Service.JobPost;
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

var key = builder.Configuration["AppSettings:Token"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key!))
        };
    });


builder.Services.AddAuthorization();
builder.Services.AddAuthorizationCore();



builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJobPostService, JobPostService>();
builder.Services.AddScoped<IHumanResourceService, HumanResourceService>();
builder.Services.AddScoped<ICandidateService, CandidateService>();
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
