using HelloJobPH.Server.Data;
using HelloJobPH.Server.Utility;
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
            var userId = Utilities.GetUserId();

            // Try to find HR or Employer linked to this user
            var hr = await _context.HumanResource.FirstOrDefaultAsync(i => i.UserAccountId == userId);
            var employer = await _context.Employer.FirstOrDefaultAsync(i => i.UserAccountId == userId);

            var employerId = hr?.EmployerId ?? employer?.EmployerId;


            if (employerId == null)
                throw new Exception("No associated employer found for this user.");


            var auditLogs = await _context.AuditLog
              
                .Where(i=>i.EmployerId == employerId)
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
                    Notes = a.Details
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
                    InterviewBy = h.Interviewer.Name,
                    ScheduledDate = h.ScheduledDate,
                    CreatedAt = h.CreatedAt
                })
                .ToListAsync();

            return history;
        }

        public async Task<List<JobpostAuditDtos>> GetJobAuditLogsAsync()
        {
            var userId = Utilities.GetUserId();

            // Try to find HR or Employer linked to this user
            var hr = await _context.HumanResource.FirstOrDefaultAsync(i => i.UserAccountId == userId);
            var employer = await _context.Employer.FirstOrDefaultAsync(i => i.UserAccountId == userId);

            var employerId = hr?.EmployerId ?? employer?.EmployerId;


            if (employerId == null)
                throw new Exception("No associated employer found for this user.");

            var auditLogs = await _context.JobPostAudit

               .Where(i => i.EmployerId == employerId)
               .Select(a => new JobpostAuditDtos
               {
                    JobpostId = a.JobPostingId,
                    Title = a.JobPosting.Title,
                    Action = a.Action,
                    ChangedBy =a.HumanResource.Firstname +" " +a.HumanResource.Lastname,
                    ChangedAt = a.ChangedAt

               })
               .ToListAsync();


            return auditLogs;
        }
    }
}
