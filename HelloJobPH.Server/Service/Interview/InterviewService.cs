using HelloJobPH.Server.Data;
using HelloJobPH.Shared.DTOs;
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
        public Task<List<InterviewListDtos>> FinalList()
        {
            throw new NotImplementedException();
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
                    select new InterviewListDtos
                    {
                        ApplicationId = app.ApplicationId,
                        ResumeUrl = app.ResumeUrl,
                        Firstname = applicant.Firstname,
                        Lastname = applicant.Surname,
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

        public Task<List<InterviewListDtos>> TechnicalList()
        {
            throw new NotImplementedException();
        }
    }
}
