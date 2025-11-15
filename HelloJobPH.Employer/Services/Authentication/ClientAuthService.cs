using Blazored.LocalStorage;
using HelloJobPH.Employer.JwtAuthStateProviders;
using HelloJobPH.Shared.DTOs;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace HelloJobPH.Employer.Services.Authentication
{
    public class ClientAuthService : IClientIAuthService
    {
        private readonly HttpClient _http;
        private readonly JwtAuthStateProvider _authStateProvider;

        public ClientAuthService(HttpClient http, JwtAuthStateProvider authStateProvider)
        {
            _http = http;
            _authStateProvider = authStateProvider;
        }

        public async Task<bool> LoginAsync(LoginDtos user)
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", user);

            if (!response.IsSuccessStatusCode)
                return false;

            var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
            if (string.IsNullOrEmpty(result?.Token))
                return false;

            await _authStateProvider.MarkUserAsAuthenticated(result.Token);
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);
            return true;
        }
        public async Task<bool> LoginAdminAsync(LoginDtos user)
        {
            var response = await _http.PostAsJsonAsync("api/auth/loginadmin", user);

            if (!response.IsSuccessStatusCode)
                return false;

            var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
            if (string.IsNullOrEmpty(result?.Token))
                return false;

            await _authStateProvider.MarkUserAsAuthenticated(result.Token);
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);
            return true;
        }

        //public async Task Logout()
        //{
        //    await _authStateProvider.MarkUserAsLoggedOut();
        //    _http.DefaultRequestHeaders.Authorization = null;
        //}
    }
}
