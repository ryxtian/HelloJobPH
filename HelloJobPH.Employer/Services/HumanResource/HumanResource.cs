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

        public Task<string> AddAsync(JobPostingDtos jobPost)
        {
            throw new NotImplementedException();
        }

        public Task<JobPostingDtos> GetSingle(int id)
        {
            throw new NotImplementedException();
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

        public Task<string> UpdateAsync(JobPostingDtos jobpost)
        {
            throw new NotImplementedException();
        }
    }
}
