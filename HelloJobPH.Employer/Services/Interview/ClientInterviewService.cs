using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Enums;
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

        public async Task<bool> Reschedule(int id, string time, string date, string? location)
        {
            var url = $"api/Interview/Reschedule{id}?date={date:yyyy-MM-dd}&time={time}&location={Uri.EscapeDataString(location ?? "")}";

            var response = await _http.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
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
        public async Task<bool> ForTechnical(int id, string time, string date, string? location)
        {
            var url = $"api/Interview/ForTechnical{id}?date={date:yyyy-MM-dd}&time={time}&location={Uri.EscapeDataString(location ?? "")}";

            var response = await _http.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> ForFinal(int id, string time, string date, string? location)
        {
            var url = $"api/Interview/ForFinal{id}?date={date:yyyy-MM-dd}&time={time}&location={Uri.EscapeDataString(location ?? "")}";

            var response = await _http.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
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
