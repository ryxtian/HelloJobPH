
using HelloJobPH.Shared.DTOs;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using static HelloJobPH.Employer.Pages.Shared.Dialogs.AIOverviewDialog;

namespace HelloJobPH.Employer.Services.Candidate
{
    public class ClientCandidateService : IClientCandidateService
    {
        private readonly HttpClient _http;
        public ClientCandidateService(HttpClient http)
        {
            _http = http;
        }
        public async Task<bool> CandidateAcceptAsync(int id)
        {
            var response = await _http.PutAsJsonAsync($"api/candidate/Accept/{id}", new { });

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            // Optionally log the error or throw for handling in UI
            var error = await response.Content.ReadAsStringAsync();
            throw new ApplicationException($"Failed to reject candidate. Server returned: {error}");
        }

        public async Task<bool> CandidateRejectAsync(int id)
        {
            var response = await _http.PutAsJsonAsync($"api/candidate/reject/{id}", new { });

            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            // Optionally log the error or throw for handling in UI
            var error = await response.Content.ReadAsStringAsync();
            throw new ApplicationException($"Failed to reject candidate. Server returned: {error}");
        }

        public async Task<List<ApplicationListDtos>> RetrieveAllAcceptedCandidate()
        {
            try
            {
                var response = await _http.GetAsync("api/Candidate/Accepted-List");

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

        public async Task<bool> ForInitial(SetScheduleDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/Candidate/SendEmail", dto);
            return response.IsSuccessStatusCode;
        }
        public class OverviewResponse
        {
            public string Overview { get; set; }
        }
        public async Task<OverviewResponse?> AIOverviewAsync(int id)
        {
            var response = await _http.PostAsync($"api/aioverview/ai-overview/{id}", null);

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };

                var result = await response.Content.ReadFromJsonAsync<OverviewResponse>(options);
                return result;
            }

            return null;
        }


    }
}
