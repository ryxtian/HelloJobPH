using HelloJobPH.Shared.DTOs;

namespace HelloJobPH.Server.Service.AuditLog
{
    public interface IAuditLogService
    {
        Task<List<AuditLogDtos>> GetAuditLogsAsync();
        Task<List<InterviewHistoryDtos>> GetInterviewHistory(int id);
    }
}
