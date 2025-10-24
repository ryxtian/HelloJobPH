using HelloJobPH.Employer.JwtAuthStateProviders;
using HelloJobPH.Shared.DTOs;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;

namespace HelloJobPH.Employer.Services.Authentication
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private readonly AuthenticationStateProvider _authProvider;
        public AuthService(HttpClient http, AuthenticationStateProvider authProvider)
        {
            _http = http;
            _authProvider = authProvider;
        }

        public async Task<LoginDtos> Login(LoginDtos user)
        {
            var response = await _http.PostAsJsonAsync("api/auth/login",user);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error posting HumanResource: {response.StatusCode} - {error}");
            }
       
           return await response.Content.ReadFromJsonAsync<LoginDtos>();
        }
        //public async Task<IEnumerable<ClaimsDtos>> GetClaims()
        //{
        //    var response = await _http.GetAsync("api/auth/claims");

        //    if (!response.IsSuccessStatusCode)
        //    {
        //        throw new HttpRequestException($"Failed to get claims: {response.StatusCode}");
        //    }

        //    return await response.Content.ReadFromJsonAsync<IEnumerable<ClaimsDtos>>();
        //}
    }
}
