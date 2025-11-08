using HelloJobPH.Server.Data;
using HelloJobPH.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HelloJobPH.Server.Service.AuditLog
{
    public class AuditLogService : IAuditLogService
    {
        private readonly ApplicationDbContext _context;
        public AuditLogService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<AuditLogDtos>> GetAuditLogsAsync()
        {
            var userId = Utility.Utilities.GetUserId();

            var hr = await _context.HumanResource.FirstOrDefaultAsync(i => i.UserAccountId == userId);

            if(hr is null)
            {
                throw new Exception("Human Resource Not foud");
            }



            var auditLogs = await _context.AuditLog
              
                .Where(i=>i.EmployerId == hr.EmployerId)
                .Select(a => new AuditLogDtos
                {
                    AuditLogId = a.AuditLogId,
                    ApplicationId = a.ApplicationId,
                    CandidateFirstName = a.Applicant != null ? a.Applicant.Firstname : string.Empty,
                    CandidateLastName = a.Applicant != null ? a.Applicant.Surname : string.Empty,
                    JobTitle = a.JobPosting != null ? a.JobPosting.Title : string.Empty,
                    HRPersonnelName = a.HumanResources != null ? $"{a.HumanResources.Firstname} {a.HumanResources.Lastname}" : string.Empty,
                    HRDepartment = a.HumanResources != null ? a.HumanResources.JobTitle : string.Empty,
                    Action = a.Action,
                    Details = a.Details,
                    Timestamp = a.Timestamp,
                    Notes = a.Details // or leave empty if you want a separate Notes field
                })
                .ToListAsync();

            return auditLogs;
        }

        public async Task<List<InterviewHistoryDtos>> GetInterviewHistory(int applicationId)
        {
            var history = await _context.InterviewHistory
                .Where(h => h.ApplicationId == applicationId)
                .OrderByDescending(h => h.CreatedAt)
                .Select(h => new InterviewHistoryDtos
                {
                    InterviewHistoryId = h.InterviewHistoryId,
                    ApplicationId = h.ApplicationId,
                    CandidateName = h.CandidateName,
                    Stage = h.Stage,
                    Status = h.Status,
                    InterviewBy = h.InterviewBy,
                    ScheduledDate = h.ScheduledDate,
                    CreatedAt = h.CreatedAt
                })
                .ToListAsync();

            return history;
        }
    }
}
