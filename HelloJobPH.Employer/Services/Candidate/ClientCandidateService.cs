
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
        public Task<bool> CandidateAcceptAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CandidateRejectAsync(int id)
        {
            throw new NotImplementedException();
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

    }
}
