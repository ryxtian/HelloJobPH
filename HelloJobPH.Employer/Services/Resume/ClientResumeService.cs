using static System.Net.WebRequestMethods;

namespace HelloJobPH.Employer.Services.Resume
{
    public class ClientResumeService : IClientResumeService
    {
        private readonly HttpClient _http;
        public ClientResumeService(HttpClient http)
        {
            _http = http;
        }
        public async Task<Stream?> GetResumeAsync(int applicationId)
        {
            var response = await _http.GetAsync($"api/resume/get-resume/{applicationId}");
            if (!response.IsSuccessStatusCode)
                return null;

            return await response.Content.ReadAsStreamAsync();
        }
    }
}
