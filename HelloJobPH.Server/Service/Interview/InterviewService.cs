using HelloJobPH.Server.Data;
using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace HelloJobPH.Server.Service.Interview
{
    public class InterviewService : IInterviewService
    {
        private readonly ApplicationDbContext _context;
        public InterviewService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<InterviewListDtos>> FinalList()
        {
            try
            {
                var result = await(
                    from app in _context.Application
                    join applicant in _context.Applicant on app.ApplicantId equals applicant.ApplicantId
                    join user in _context.UserAccount on applicant.UserAccountId equals user.UserAccountId
                    join job in _context.JobPosting on app.JobPostId equals job.JobPostingId
                    join interview in _context.Interview on app.ApplicationId equals interview.ApplicationId//xx
                    where app.ApplicationStatus == Shared.Enums.ApplicationStatus.Final
                    select new InterviewListDtos
                    {
                        ApplicationId = app.ApplicationId,
                        ResumeUrl = app.ResumeUrl,
                        Firstname = applicant.Firstname,
                        Lastname = applicant.Surname,
                        Email = user.Email,
                        TimeInterview = interview.ScheduledTime,
                        JobTitle = job.Title,
                        Type = job.EmploymentType,
                        DateApplied = app.DateApply,
                        DateInterview = interview.ScheduledDate,
                        Status = app.ApplicationStatus
                    }).ToListAsync();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<InterviewListDtos>> InitialList()
        {
            try
            {
                var result = await (
                    from app in _context.Application
                    join applicant in _context.Applicant on app.ApplicantId equals applicant.ApplicantId
                    join user in _context.UserAccount on applicant.UserAccountId equals user.UserAccountId
                    join job in _context.JobPosting on app.JobPostId equals job.JobPostingId
                    join interview in _context.Interview on app.ApplicationId equals interview.ApplicationId //xx
                    where app.ApplicationStatus == Shared.Enums.ApplicationStatus.Initial
                    select new InterviewListDtos
                    {
                        ApplicationId = app.ApplicationId,
                        ResumeUrl = app.ResumeUrl,
                        Firstname = applicant.Firstname,
                        Lastname = applicant.Surname,
                        Type = job.EmploymentType,
                        TimeInterview = interview.ScheduledTime,
                        Email = user.Email,
                        JobTitle = job.Title,
                        DateInterview = interview.ScheduledDate,
                        DateApplied = app.DateApply,
                        Status = app.ApplicationStatus
                    }).ToListAsync();

                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<List<InterviewListDtos>> TechnicalList()
        {
            try
            {
                var result = await(
                    from app in _context.Application
                    join applicant in _context.Applicant on app.ApplicantId equals applicant.ApplicantId
                    join user in _context.UserAccount on applicant.UserAccountId equals user.UserAccountId
                    join job in _context.JobPosting on app.JobPostId equals job.JobPostingId
                    join interview in _context.Interview on app.ApplicationId equals interview.ApplicationId //xx
                    where app.ApplicationStatus == Shared.Enums.ApplicationStatus.Technical
                    select new InterviewListDtos
                    {
                        ApplicationId = app.ApplicationId,
                        ResumeUrl = app.ResumeUrl,
                        Firstname = applicant.Firstname,
                        Lastname = applicant.Surname,
                        DateInterview = interview.ScheduledDate,
                        Type = job.EmploymentType,
                        TimeInterview = interview.ScheduledTime,
                        Email = user.Email,
                        JobTitle = job.Title,
                        DateApplied = app.DateApply,
                        Status = app.ApplicationStatus
                    }).ToListAsync();

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
