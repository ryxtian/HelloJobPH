using HelloJobPH.Shared.DTOs;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace HelloJobPH.Employer.Services.Interview
{
    public class ClientInterviewService : IClientInterviewService
    {
        private readonly HttpClient _http;
        public ClientInterviewService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<InterviewListDtos>> FinalList()
        {
            var result = await _http.GetFromJsonAsync<List<InterviewListDtos>>("api/Interview/final");
            return result ?? [];
        }

        public async Task<List<InterviewListDtos>> InitialList()
        {
            var result = await _http.GetFromJsonAsync<List<InterviewListDtos>>("api/Interview/initial");
            return result ?? [];
        }

        public async Task<List<InterviewListDtos>> TechnicalList()
        {
            var result = await _http.GetFromJsonAsync<List<InterviewListDtos>>("api/Interview/technical");
            return result ?? [];
        }
    }
}
