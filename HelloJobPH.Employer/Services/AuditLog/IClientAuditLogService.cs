using HelloJobPH.Shared.DTOs;

namespace HelloJobPH.Employer.Services.AuditLog
{
    public interface IClientAuditLogService
    {
        Task<List<AuditLogDtos>> RetrieveAuditLogs();
        Task<List<InterviewHistoryDtos>> GetInterviewHistory(int id);
    }
}
