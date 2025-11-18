using HelloJobPH.Employer.GeneralResponse;
using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Enums;
using HelloJobPH.Shared.Model;
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
        //public async Task<List<InterviewListDtos>> FinalList()
        //{
        //    var result = await _http.GetFromJsonAsync<List<InterviewListDtos>>("api/Interview/final");
        //    return result ?? [];
        //}

        public async Task<GeneralResponse<List<InterviewListDtos>>> InitialList()
        {
            try
            {
                // Deserialize the API response directly into GeneralResponse
                var response = await _http.GetFromJsonAsync<GeneralResponse<List<InterviewListDtos>>>("api/Interview/initial");

                // Ensure it’s not null
                return response ?? new GeneralResponse<List<InterviewListDtos>>
                {
                    Success = false,
                    Data = new List<InterviewListDtos>(),
                    Message = "No data received from server."
                };
            }
            catch (Exception ex)
            {
                return new GeneralResponse<List<InterviewListDtos>>
                {
                    Success = false,
                    Data = new List<InterviewListDtos>(),
                    Message = ex.Message
                };
            }
        }



        //public async Task<List<InterviewListDtos>> TechnicalList()
        //{
        //    var result = await _http.GetFromJsonAsync<List<InterviewListDtos>>("api/Interview/technical");
        //    return result ?? [];
        //}
        public async Task<GeneralResponse<bool>> Reschedule(SetScheduleDto dto)
        {
            var url = $"api/interview/Reschedule";
            var response = await _http.PostAsJsonAsync(url, dto);

            return new GeneralResponse<bool>
            {
                Success = response.IsSuccessStatusCode,
                Data = response.IsSuccessStatusCode
            };
        }

        public async Task<GeneralResponse<bool>> NoAppearance(int id)
        {
            try
            {
                var response = await _http.PutAsync($"api/Interview/NoAppearance/{id}", null);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<GeneralResponse<bool>>();
                    return result!;
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API Error: {content}");
                    return new GeneralResponse<bool> { Success = false, Data = false };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception in NoAppearance: {ex.Message}");
                return new GeneralResponse<bool> { Success = false, Data = false };
            }
        }
        public async Task<GeneralResponse<bool>> ForTechnical(SetScheduleDto dto)
        {
            var url = $"api/interview/ForTechnical";
            var response = await _http.PostAsJsonAsync(url, dto);

            return new GeneralResponse<bool>
            {
                Success = response.IsSuccessStatusCode,
                Data = response.IsSuccessStatusCode
            };
        }
        public async Task<GeneralResponse<bool>> ForFinal(SetScheduleDto dto)
        {
            var url = $"api/interview/ForFinal";
            var response = await _http.PostAsJsonAsync(url, dto);

            return new GeneralResponse<bool>
            {
                Success = response.IsSuccessStatusCode,
                Data = response.IsSuccessStatusCode
            };
        }
        public async Task<GeneralResponse<bool>> Failed(int id)
        {
            var response = await _http.GetAsync($"api/Interview/Failed/{id}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<bool>();
                return new GeneralResponse<bool> { Success = true, Data = result };
            }

            return new GeneralResponse<bool> { Success = false, Data = false };
        }
        public async Task<GeneralResponse<bool>> Delete(int id)
        {
            var response = await _http.GetAsync($"api/Interview/Delete/{id}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<bool>();
                return new GeneralResponse<bool> { Success = true, Data = result};
            }

            return new GeneralResponse<bool> { Success = false, Data = false };
        }
        public async Task<GeneralResponse<bool>> MarkAsCompleted(int id)
        {
            var response = await _http.GetAsync($"api/Interview/MarkAsCompleted/{id}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<bool>();
                return new GeneralResponse<bool> { Success = true, Data = result };
            }

            return new GeneralResponse<bool> { Success = false, Data = false };
        }
        public async Task<GeneralResponse<bool>> Hired(int id)
        {
            var response = await _http.GetAsync($"api/Interview/hired/{id}");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<bool>();
                return new GeneralResponse<bool> { Success = true, Data = result };
            }

            return new GeneralResponse<bool> { Success = false, Data = false };
        }
        public async Task<List<InterviewerDtos>> RetrieveAllInterviewer()
        {
            try
            {
                var response = await _http.GetAsync("api/Interview/interviewer-list");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"⚠️ Failed to fetch candidates: {response.StatusCode}");
                    return new List<InterviewerDtos>();
                }

                var data = await response.Content.ReadFromJsonAsync<List<InterviewerDtos>>();
                return data ?? new List<InterviewerDtos>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error retrieving candidates: {ex.Message}");
                return new List<InterviewerDtos>();
            }
        }
    }
}
