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
        public Task<List<InterviewListDtos>> FinalList()
        {
            throw new NotImplementedException();
        }

        public async Task<List<InterviewListDtos>> InitialList()
        {
            var result = await _http.GetFromJsonAsync<List<InterviewListDtos>>("api/Interview");
            return result ?? [];
        }

        public Task<List<InterviewListDtos>> TechnicalList()
        {
            throw new NotImplementedException();
        }
    }
}
