

using HelloJobPH.Employer.Pages;
using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Model;
using System.Net.Http.Json;
using System.Text.Json;

namespace HelloJobPH.Employer.Services.JobPosting
{
    public class JobPosting : IJobPosting
    {
        private readonly HttpClient _http;
        public JobPosting(HttpClient http)
        {
            _http = http;
        }
        private readonly string BaseUrl = "/api/JobPosting";

        public async Task<string> AddAsync(JobPostingDtos jobPost)
        {
            var request = await _http.PostAsJsonAsync($"{BaseUrl}",jobPost);
            if (request.IsSuccessStatusCode)
            {
                // Read and return the response body as string
                var result = await request.Content.ReadAsStringAsync();
                return result;
            }
            else
            {
                // You can throw or handle this as needed
                var error = await request.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error posting job: {request.StatusCode} - {error}");
            }
        }

        public async Task<bool> SoftDeleteJobPost(int id)
        {

         

            // Send PUT request
            var response = await _http.PutAsJsonAsync($"", softDeleteData);

            // Return true if successful, false otherwise
            return response.IsSuccessStatusCode;
        }

        public Task<JobPostingDtos> GetSingleJobPost(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<JobPostingDtos>> RetrieveAllAsync()
        {
            var result = await _http.GetFromJsonAsync<List<JobPostingDtos>>($"{BaseUrl}/list");
            return result ?? [];
        }

        public Task<string> UpdateAsync(JobPostingDtos jobpost)
        {
            throw new NotImplementedException();
        }
    }
}