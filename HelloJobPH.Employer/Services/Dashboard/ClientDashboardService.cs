using System.Net.Http.Json;

namespace HelloJobPH.Employer.Services.Dashboard
{
    public class ClientDashboardService : IClientDashboardService
    {
        private readonly HttpClient _http;
        public ClientDashboardService(HttpClient http)
        {
            _http = http;
        }
        public async Task<int> GetTotalJobPostAsync()
        {
            return await _http.GetFromJsonAsync<int>("api/dashboard/TotalJobPost");
        }

        public async Task<int> GetActiveApplicationAsync()
        {
            return await _http.GetFromJsonAsync<int>("api/dashboard/ActiveApplication");
        }

        public async Task<int> GetApprovedHiredAsync()
        {
            return await _http.GetFromJsonAsync<int>("api/dashboard/ApprovedHired");
        }
    }
}
