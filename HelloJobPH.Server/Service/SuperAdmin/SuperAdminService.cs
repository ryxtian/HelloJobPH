using HelloJobPH.Employer.Pages.Candidate;
using HelloJobPH.Server.Data;
using HelloJobPH.Server.Service.Email;
using HelloJobPH.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using static HelloJobPH.Employer.Pages.SuperAdmin.AdminDashboard;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HelloJobPH.Server.Service.SuperAdmin
{
    public class SuperAdminService : ISuperAdminService
    {
        private readonly IEmailService _emailService;
        private readonly ApplicationDbContext _context;
        public SuperAdminService(IEmailService emailService, ApplicationDbContext context)
        {
            _context = context;
            _emailService = emailService;
        }
        public async Task<List<EmployerListDtos>> EmployersList()
        {

            List<EmployerListDtos> employers = await _context.Employer.Where(i=>i.IsDeleted ==0 && i.Status != "Pending")
                .Select(e => new EmployerListDtos
                {
                    EmployerId = e.EmployerId,
                    CompanyName = e.CompanyName,
                    Industry = e.Industry,
                    Description = e.Description,
                    CompanyAddress = e.CompanyAddress,
                    City = e.City,
                    Status = e.Status,
                    Province = e.Province,
                    ContactEmail = e.ContactEmail,
                    ContactNumber = e.ContactNumber
                })
                .ToListAsync();

            return employers;
        }
        public async Task<List<EmployerListDtos>> ForApprovalList()
        {

            List<EmployerListDtos> employers = await _context.Employer.Where(i=>i.Status == "Pending")
                .Select(e => new EmployerListDtos
                {
                    EmployerId = e.EmployerId,
                    CompanyName = e.CompanyName,
                    Industry = e.Industry,
                    Description = e.Description,
                    CompanyAddress = e.CompanyAddress,
                    City = e.City,
                    Status = e.Status,
                    Province = e.Province,
                    ContactEmail = e.ContactEmail,
                    ContactNumber = e.ContactNumber
                })
                .ToListAsync();

            return employers;
        }
        public async Task<bool> ApprovedEmployer(int id)
        {
            var employer = await _context.Employer.FindAsync(id);
            if (employer == null)
                return false;

            employer.Status = "Active";
            _context.Employer.Update(employer);
            await _context.SaveChangesAsync();

            // Subject and plain text body for account approval
            var subject = "Your Employer Account Has Been Approved!";
            var body = $@"
Dear {employer.CompanyName},

Congratulations! Your employer account with HelloJobPH has been approved and is now active.

You can now log in and start posting job openings, managing applications, and connecting with potential candidates.

We are excited to have you on board!

Best regards,
HelloJobPH Team";

            // Send the plain text email
            await _emailService.SendEmailAsync(employer.ContactEmail, subject, body);

            return true;
        }



        public async Task<bool> DisableEmployer(int id)
        {
            var employer = await _context.Employer.FindAsync(id);
            if (employer == null)
                return false;

            employer.Status = "Disabled";
            _context.Employer.Update(employer);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DisapprovedEmployer(int id)
        {
            var employer = await _context.Employer.FindAsync(id);
            if (employer == null)
                return false;

            employer.IsDeleted = 1;
            _context.Employer.Update(employer);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<int> TotalJobCounts()
        {
            return await _context.JobPosting.CountAsync(); // Count all job postings
        }

        public async Task<int> TotalApplicants()
        {
            return await _context.Applicant.CountAsync(); // Count all applicants
        }

        public async Task<int> ScheduledInterviews()
        {
            return await _context.Interview
                .Where(i => i.ScheduledDate >= DateTime.Today) // Count only upcoming interviews
                .CountAsync();
        }

        public async Task<int> SuccessfulHires()
        {
            return await _context.Application
                .Where(h => h.ApplicationStatus == Shared.Enums.ApplicationStatus.Hired) // Count only successful hires
                .CountAsync();
        }
        public async Task<List<TopJobDto>> GetTopAppliedJobs()
        {
            var result = await _context.JobPosting
                .Select(j => new TopJobDto
                {
                    JobTitle = j.Title,
                    ApplicantCount = j.Application.Count()
                })
                .OrderByDescending(j => j.ApplicantCount)
                .Take(7)
                .ToListAsync();

            return result;
        }
        public async Task<List<ApplicationStatusDto>> GetApplicationStatusCounts()
        {
            return await _context.Application
                .GroupBy(a => a.ApplicationStatus)
                .Select(g => new ApplicationStatusDto
                {
                    Status = g.Key.ToString(),
                    Count = g.Count()
                })
                .ToListAsync();
        }
        public async Task<List<JobPostingListDtos>> JobPostList()
        {
            var today = DateTime.UtcNow;

            var jobPosts = await _context.JobPosting
                .Select(j => new JobPostingListDtos
                {
                    JobPostingId = j.JobPostingId,
                    Title = j.Title,
                    Description = j.Description,
                    Location = j.Location,
                    EmploymentType = j.EmploymentType,
                    SalaryFrom = j.SalaryFrom,
                    SalaryTo = j.SalaryTo,
                    JobCategory = j.JobCategory,
                    JobRequirements = j.JobRequirements,
                    PostedDate = j.PostedDate,

                    IsDeleted = j.IsDeleted
                })
.Where(j => j.ExpiredDate == null || j.ExpiredDate >= today)
.OrderByDescending(j => j.PostedDate)
                .ToListAsync();

            return jobPosts;
        }


        public async Task<List<ApplicantDtos>> ApplicantList()
        {
            var applicants = await _context.Applicant
                .Select(a => new ApplicantDtos
                {
                    ApplicantId = a.ApplicantId,
                    Firstname = a.Firstname,
                    Middlename = a.Middlename,
                    Surname = a.Surname,
                    Address = a.Address,
                    Phone = a.Phone,
                    Email = a.UserAccount.Email,
                    IsDeleted = a.UserAccount.IsDeleted


                })
                .ToListAsync();

            return applicants;
        }


        public async Task<bool> BlockApplicantAsync(int id)
        {
            var applicant = await _context.Applicant.FindAsync(id);
            if (applicant == null) return false;
            var user = await _context.UserAccount.FirstOrDefaultAsync(i => i.UserAccountId == applicant.UserAccountId);
            if (user == null) return false;

            user.IsDeleted = 1; // or applicant.Status = "Blocked";
            _context.Applicant.Update(applicant);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UnBlockApplicantAsync(int id)
        {
            var applicant = await _context.Applicant.FindAsync(id);
            if (applicant == null) return false;
            var user = await _context.UserAccount.FirstOrDefaultAsync(i => i.UserAccountId == applicant.UserAccountId);
            if (user == null) return false;

            user.IsDeleted = 0; // or applicant.Status = "Active";
            _context.Applicant.Update(applicant);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<ApplicantViewDtos?> ViewApplicantAsync(int id)
        {
            var applicant = await _context.Applicant
                .Where(a => a.ApplicantId == id)
                .Select(a => new ApplicantViewDtos
                {
                    ApplicantId = a.ApplicantId,
                    Firstname = a.Firstname,
                    Lastname = a.Surname, // Assuming Surname maps to Lastname
                    Phone = a.Phone,
                    Email = a.UserAccount.Email,
                    Location = a.Address,
                    WorkExperiences = a.WorkExperiences
                                       .Select(w => new WorkExperienceDtos
                                       {
                                           CompanyName = w.CompanyName,
                                           PositionTitle = w.PositionTitle,
                                           StartDate = w.StartDate,
                                           EndDate = w.EndDate
                                       }).ToList(),
                    EducationalAttainment = a.EducationalAttainments
                                             .Select(e => new EducationalAttainmentDtos
                                             {
                                                 SchoolName = e.SchoolName,
                                                 Degree = e.Degree,
                                                 YearEnded = e.YearEnded
                                             }).ToList()
                })
                .FirstOrDefaultAsync();

            return applicant;
        }

        public async Task<JobPostingDtos> GetJobDetails(int id)
        {
            var job = await _context.JobPosting
                .Where(j => j.JobPostingId == id)
                .Select(j => new JobPostingDtos
                {
                    JobPostingId = j.JobPostingId,
                    Title = j.Title,
                    Description = j.Description,
                    Location = j.Location,
                    EmploymentType = j.EmploymentType,
                    SalaryFrom = j.SalaryFrom,
                    SalaryTo = j.SalaryTo,
                    JobCategory = j.JobCategory,
                    JobRequirements = j.JobRequirements,
                    PostedDate = j.PostedDate,

                    IsDeleted = j.IsDeleted
                })
                .FirstOrDefaultAsync();

            return job;
        }

        public async Task<bool> UnBlockJob(int id)
        {
            var job = await _context.JobPosting.FindAsync(id);
            if (job == null)
                return false;

            job.IsDeleted = 0; // or applicant.Status = "Blocked";
            _context.JobPosting.Update(job);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> BlockJob(int id)
        {
            var job = await _context.JobPosting.FindAsync(id);
            if (job == null)
                return false;

            job.IsDeleted = 1;
            _context.JobPosting.Update(job);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
