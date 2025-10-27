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
                                    ApplicantId = applicant.ApplicantId,
                                    Firstname = applicant.Firstname,
                                    Lastname = applicant.Surname,
                                    Phone = applicant.Phone,
                                    //Email = applicant.Email,
                                    Location = applicant.Address,

                                    JobTitle = job.Title,
                                    SalaryFrom = job.SalaryFrom,
                                    SalaryTo = job.SalaryTo,
                                    LocationOfjob = job.Location,
                                    EmployementType = job.EmploymentType,
                                    JobPostedDate = job.PostedDate.ToString("yyyy-MM-dd"),
                                    JobDescription = job.Description,

                                    WorkExperiences = _context.WorkExperience
                                        .Where(w => w.ApplicantId == applicant.ApplicantId)
                                        .Select(w => new WorkExperienceDtos
                                        {
                                            CompanyName = w.CompanyName,
                                            PositionTitle = w.PositionTitle,
                                            Department = w.Department,
                                            StartDate = w.StartDate,
                                            EndDate = w.EndDate,
                                            IsPresent = w.IsPresent,
                                            Responsibilities = w.Responsibilities,
                                            CompanyAddress = w.CompanyAddress
                                        }).ToList(),
                                    EducationalAttainment = _context.EducationalAttainment
                                        .Where(w => w.ApplicantId == applicant.ApplicantId)
                                        .Select(w => new EducationalAttainmentDtos
                                        {
                                            SchoolName = w.SchoolName,
                                            Degree = w.Degree,
                                            YearStarted = w.YearStarted,
                                            YearEnded = w.YearEnded,
                                            Level = w.Level,
                                            IsGraduated = w.IsGraduated,
                                      
                                        }).ToList(),

                                }).FirstOrDefaultAsync();

            return result;
        }



    }
}
