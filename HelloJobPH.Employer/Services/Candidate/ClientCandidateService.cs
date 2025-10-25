
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

        public async Task<List<ApplicationDtos>> RetriveAllCandidate()
        {
            var request = await _http.GetFromJsonAsync<List<ApplicationDtos>>("api/Candidate");
            return request ?? [];
        }
    }
}
