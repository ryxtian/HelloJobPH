using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Model;
using System.Net.Http;
using System.Net.Http.Json;
using static HelloJobPH.Employer.Pages.SuperAdmin.AdminDashboard;

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
        public async Task<bool> ApprovedEmployer(int id)
        {
            var response = await _http.PutAsync($"api/SuperAdmin/approve-employer/{id}", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DisableEmployer(int id)
        {
            var response = await _http.PutAsync($"api/SuperAdmin/disable-employer/{id}", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DisapprovedEmployer(int id)
        {
            var response = await _http.PutAsync($"api/SuperAdmin/disapprove-employer/{id}", null);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<EmployerListDtos>> ForApprovalList()
        {
            var result = await _http.GetFromJsonAsync<List<EmployerListDtos>>("api/SuperAdmin/ForApprovalList");
            return result ?? [];
        }

        public async Task<int> TotalJobCounts()
        {
            var result = await _http.GetFromJsonAsync<int>("api/SuperAdmin/TotalJobs");
            return result;
        }

        public async Task<int> TotalApplicants()
        {
            var result = await _http.GetFromJsonAsync<int>("api/SuperAdmin/TotalApplicants");
            return result;
        }

        public async Task<int> ScheduledInterviews()
        {
            var result = await _http.GetFromJsonAsync<int>("api/SuperAdmin/ScheduledInterviews");
            return result;
        }

        public async Task<int> SuccessfulHires()
        {
            var result = await _http.GetFromJsonAsync<int>("api/SuperAdmin/SuccessfulHires");
            return result;
        }
        public async Task<List<TopJobDto>> GetTopAppliedJobs()
        {
            return await _http.GetFromJsonAsync<List<TopJobDto>>("api/SuperAdmin/TopAppliedJobs") ?? [];
        }
        public async Task<List<ApplicationStatusDto>> GetApplicationStatusCounts()
        {
            return await _http.GetFromJsonAsync<List<ApplicationStatusDto>>("api/SuperAdmin/applicationstatus") ?? [];
        }

        public async Task<List<JobPostingListDtos>> JobPostList()
        {
            var result = await _http.GetFromJsonAsync<List<JobPostingListDtos>>("api/SuperAdmin/JobPostList") ?? [];
            return result ?? [];
        }



        public async Task<List<ApplicantDtos>> ApplicantList()
        {
            var result = await _http.GetFromJsonAsync<List<ApplicantDtos>>("api/SuperAdmin/ApplicantList");
            return result ?? [];
        }

        public async Task<bool> BlockApplicant()
        {
            var response = await _http.PutAsJsonAsync<ApplicantDtos>("api/superadmin/SuperAdmin/block", null);
            return true;
        }

        public async Task<bool> UnBlockApplicant()
        {
            var response = await _http.PutAsJsonAsync<ApplicantDtos>("api/superadmin/SuperAdmin/unblock", null);
            return true;
        }

        public async Task<ApplicantDtos> ViewApplicant(int id)
        {
            var response = await _http.GetFromJsonAsync<ApplicantDtos>($"api/superadmin/SuperAdmin/{id}");
            return response;
        }
    }
}
