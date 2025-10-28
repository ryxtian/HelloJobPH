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
            var result = await _context.Application

                .Where(a => a.ApplicationId == id)
                .Select(a => new OverviewDtos
                {
                    ApplicantId = a.Applicant.ApplicantId,
                    Firstname = a.Applicant.Firstname,
                    Lastname = a.Applicant.Surname,
                    Phone = a.Applicant.Phone,
                    Location = a.Applicant.Address,

                    JobTitle = a.JobPosting.Title,
                    SalaryFrom = a.JobPosting.SalaryFrom,
                    SalaryTo = a.JobPosting.SalaryTo,
                    LocationOfjob = a.JobPosting.Location,
                    EmployementType = a.JobPosting.EmploymentType,
                    JobPostedDate = a.JobPosting.PostedDate.ToString("yyyy-MM-dd"),
                    JobDescription = a.JobPosting.Description,
                    JobRequirement = a.JobPosting.JobRequirements,

                    WorkExperiences = a.Applicant.WorkExperiences
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

                    EducationalAttainment = a.Applicant.EducationalAttainments
                        .Select(e => new EducationalAttainmentDtos
                        {
                            SchoolName = e.SchoolName,
                            Degree = e.Degree,
                            YearStarted = e.YearStarted,
                            YearEnded = e.YearEnded,
                            Level = e.Level,
                            IsGraduated = e.IsGraduated
                        }).ToList()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return result;
        }




    }
}
