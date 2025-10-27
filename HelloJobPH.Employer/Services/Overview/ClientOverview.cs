using HelloJobPH.Shared.DTOs;
using System.Net.Http.Json;

namespace HelloJobPH.Employer.Services.Overview
{
    public class ClientOverview : IClientOverview
    {
        private readonly HttpClient _http;
        public ClientOverview(HttpClient http)
        {
            _http = http;
        }
        public async Task<OverviewDtos?> OverviewApplication(int id)
        {
            var response = await _http.GetAsync($"api/Overview/{id}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<OverviewDtos>();
                return result;
            }
            else
            {
                return null;
            }
        }

    }
}
