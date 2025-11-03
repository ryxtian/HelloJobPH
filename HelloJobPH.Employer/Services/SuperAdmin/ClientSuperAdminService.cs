using HelloJobPH.Shared.DTOs;
using System.Net.Http.Json;

namespace HelloJobPH.Employer.Services.SuperAdmin
{
    public class ClientSuperAdminService : IClientSuperAdminService
    {
        private readonly HttpClient _http;
        public ClientSuperAdminService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<EmployerListDtos>> EmployerList()
        {
            var result = await _http.GetFromJsonAsync<List<EmployerListDtos>>("api/SuperAdmin/employer-list");
            return result ?? [];
        }
    }
}
