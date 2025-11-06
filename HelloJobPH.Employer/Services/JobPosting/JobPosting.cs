

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
            // We’re not sending a body, just calling the endpoint
            var response = await _http.PutAsync($"{BaseUrl}/soft-delete/{id}", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<JobPostingDtos> GetSingleJobPost(int id)
        {
            var response = await _http.GetAsync($"{BaseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jobPost = await response.Content.ReadFromJsonAsync<JobPostingDtos>();
                return jobPost;
            }
            else
            {
                // Handle not found or error, maybe return null or throw
                return null;
            }
        }

        public async Task<List<JobPostingDtos>> RetrieveAllAsync()
        {
            var result = await _http.GetFromJsonAsync<List<JobPostingDtos>>($"{BaseUrl}/list");
            return result ?? [];
        }

        public async Task<string> UpdateAsync(JobPostingDtos jobpost)
        {
            var response = await _http.PutAsJsonAsync($"{BaseUrl}/{jobpost.JobPostingId}", jobpost);

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
        public async Task<bool> Activate(int id)
        {
            var response = await _http.PutAsync($"{BaseUrl}/Activate/{id}", null);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> Deactivate(int id)
        {
            var response = await _http.PutAsync($"{BaseUrl}/Deactivate/{id}", null);
            return response.IsSuccessStatusCode;
        }

    }
}