
using HelloJobPH.Shared.DTOs;
using System.Net.Http.Json;

namespace HelloJobPH.Employer.Services.Candidate
{
    public class ClientCandidateService : IClientCandidateService
    {
        private readonly HttpClient _http;
        public ClientCandidateService(HttpClient http)
        {
            _http = http;
        }
        public async Task<ApplicationListDtos> CandidateAcceptAsync(int id)
        {
            var response = await _http.GetAsync($"api/Candidate/{id}");
            if (response.IsSuccessStatusCode)
            {
                var jobPost = await response.Content.ReadFromJsonAsync<ApplicationListDtos>();
                return jobPost;
            }
            else
            {
                // Handle not found or error, maybe return null or throw
                return null;
            }
        }

        public Task<bool> CandidateRejectAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ApplicationListDtos>> RetrieveAllAcceptedCandidate()
        {
            try
            {
                var response = await _http.GetAsync("api/Candidate/AcceptedList");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"⚠️ Failed to fetch candidates: {response.StatusCode}");
                    return new List<ApplicationListDtos>();
                }

                var data = await response.Content.ReadFromJsonAsync<List<ApplicationListDtos>>();
                return data ?? new List<ApplicationListDtos>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error retrieving candidates: {ex.Message}");
                return new List<ApplicationListDtos>();
            }
        }
        public async Task<List<ApplicationListDtos>> RetrieveAllCandidate()
        {
            try
            {
                var response = await _http.GetAsync("api/Candidate");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"⚠️ Failed to fetch candidates: {response.StatusCode}");
                    return new List<ApplicationListDtos>();
                }

                var data = await response.Content.ReadFromJsonAsync<List<ApplicationListDtos>>();
                return data ?? new List<ApplicationListDtos>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error retrieving candidates: {ex.Message}");
                return new List<ApplicationListDtos>();
            }
        }

        public async Task<bool> SendEmail(int id, string time, string date,string?location)
        {
            var url = $"api/Candidate/SendEmail{id}?date={date:yyyy-MM-dd}&time={time}&location={Uri.EscapeDataString(location ?? "")}";

            var response = await _http.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
