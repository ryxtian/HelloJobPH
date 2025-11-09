using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Enums;
using System.Net.Http;
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
        public async Task<bool> Reschedule(SetScheduleDto dto)
        {
            var url = $"api/interview/Reschedule";
            var response = await _http.PostAsJsonAsync(url, dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<int> NoAppearance(int id)
        {
            var response = await _http.GetAsync($"api/Interview/NoAppearance{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<int>();
                return result;
            }

            return 0;
        }
        public async Task<bool> ForTechnical(SetScheduleDto dto)
        {
            var url = $"api/interview/ForTechnical";
            var response = await _http.PostAsJsonAsync(url, dto);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> ForFinal(SetScheduleDto dto)
        {
            var url = $"api/interview/ForFinal";
            var response = await _http.PostAsJsonAsync(url, dto);
            return response.IsSuccessStatusCode;
        }
        public async Task<int> Failed(int id)
        {
            var response = await _http.GetAsync($"api/Interview/Failed{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<int>();
                return result;
            }

            return 0;
        }
        public async Task<int> Delete(int id)
        {
            var response = await _http.GetAsync($"api/Interview/Delete{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<int>();
                return result;
            }

            return 0;
        }
        public async Task<int> MarkAsCompleted(int id)
        {
            var response = await _http.GetAsync($"api/Interview/MarkAsCompleted{id}");
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<int>();
                return result;
            }

            return 0;
        }
    }
}
