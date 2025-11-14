using HelloJobPH.Employer.Pages;
using HelloJobPH.Shared.DTOs;
using System.Net.Http.Json;

namespace HelloJobPH.Employer.Services.HumanResource
{
    public class HumanResource : IHumanResource
    {
        private readonly string BaseUrl = "https://localhost:7172/api/HumanResource";
        private readonly HttpClient _http;
        public HumanResource(HttpClient http)
        {
            _http = http;
        }
        public async Task<bool> AddAsync(HumanResourceDtos entity)
        {
            var response = await _http.PostAsJsonAsync("api/HumanResource/create", entity);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error posting HumanResource: {response.StatusCode} - {error}");
            }
            return true;
        }

        public async Task<HumanResourceDtos?> GetSingle(int id)
        {
            var response = await _http.GetAsync($"{BaseUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                var humanResource = await response.Content.ReadFromJsonAsync<HumanResourceDtos>();
                return humanResource;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null; // HR not found
            }
            else
            {
                // Other errors
                var error = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error fetching HR: {response.StatusCode}, {error}");
            }
        }


        public async Task<List<HumanResourceDtos>> RetrieveAllAsync()
        {
            var result = await _http.GetFromJsonAsync<List<HumanResourceDtos>>($"{BaseUrl}");
            return result ?? [];
        }

        public async Task<bool> SoftDeleteJobPost(int id)
        {
            // We’re not sending a body, just calling the endpoint
            var response = await _http.PutAsync($"{BaseUrl}/soft-delete/{id}", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<string> UpdateAsync(HumanResourceDtos hr)
        {
            var response = await _http.PutAsJsonAsync($"{BaseUrl}/{hr.HumanResourceId}", hr);

            if (response.IsSuccessStatusCode)
            {
                return "Job post updated successfully.";
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                return $"Failed to update job post. Server responded with: {error}";
            }
        }
    }
}
