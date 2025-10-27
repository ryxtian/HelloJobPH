using HelloJobPH.Server.Data;
using HelloJobPH.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace HelloJobPH.Server.Service.Overview
{
    public class OverviewService : IOverviewService
    {
        private readonly ApplicationDbContext _context;
        public OverviewService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<OverviewDtos?> ListOverview(int id)
        {
            var result = await (from applicant in _context.Applicant
                                join application in _context.Application on applicant.ApplicantId equals application.ApplicantId
                                join job in _context.JobPosting on application.JobPostId equals job.JobPostingId
                                where application.ApplicationId == id
                                select new OverviewDtos
                                {
                                    // Applicant Info
                                    ApplicantId = applicant.ApplicantId,
                                    Firstname = applicant.Firstname,
                                    Lastname = applicant.Surname,
                                    Phone = applicant.Phone,
                                    //Email = applicant.Email,
                                    //Location = applicant.Location,

                                    // Job Info
                                    JobTitle = job.Title,
                                    LocationOfjob = job.Location,
                                    //SalaryRange = job.SalaryFrom?.ToString("C0") + " - " + job.SalaryTo?.ToString("C0"),
                                    EmployementType = job.EmploymentType,
                                    JobPostedDate = job.PostedDate.ToString("yyyy-MM-dd"),
                                    JobDescription = job.Description
                                }).FirstOrDefaultAsync();

            return result;
        }


    }
}
