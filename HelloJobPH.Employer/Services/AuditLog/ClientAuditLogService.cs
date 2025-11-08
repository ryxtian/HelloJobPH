using HelloJobPH.Shared.DTOs;
using System.Net.Http;
using System.Net.Http.Json;

namespace HelloJobPH.Employer.Services.AuditLog
{
    public class ClientAuditLogService : IClientAuditLogService
    {
        private readonly HttpClient _http;
        public ClientAuditLogService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<InterviewHistoryDtos>> GetInterviewHistory(int applicationId)
        {
            var response = await _http.GetFromJsonAsync<List<InterviewHistoryDtos>>(
                $"api/auditlog/interview-history{applicationId}"
            );

            return response ?? new List<InterviewHistoryDtos>();
        }

        public async Task<List<AuditLogDtos>> RetrieveAuditLogs()
        {
            var response = await _http.GetFromJsonAsync<List<AuditLogDtos>>("api/auditlog/audit-list");
            return response ?? new List<AuditLogDtos>();
        }
    }
}
