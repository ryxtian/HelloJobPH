using AutoMapper;
using HelloJobPH.Employer.Pages.SuperAdmin;
using HelloJobPH.Employer.Services.HumanResource;
using HelloJobPH.Server.Data;
using HelloJobPH.Server.Utility;
using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Model;  // Assuming JobPosting entity is here
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HelloJobPH.Server.Service.JobPost
{
    public class JobPostService : IJobPostService
    {
        private readonly ApplicationDbContext _context;

     
        IHttpContextAccessor _httpContextAccessor;

        public JobPostService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
          
          
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<JobPostingDtos>> RetrieveAllAsync()
        {
            try
            {
       

                // Step 2: Project to DTO in memory, safely accessing navigation properties
                var response = await _context.JobPosting.
                    Select(j => new JobPostingDtos
                {
                    JobPostingId = j.JobPostingId,
                    Title = j.Title,
                    Description = j.Description,
                    EmploymentType = j.EmploymentType,
                    SalaryFrom = j.SalaryFrom,
                    SalaryTo = j.SalaryTo,
                    JobRequirements = j.JobRequirements,
                    JobCategory = j.JobCategory,
                    PostedDate = j.PostedDate,
                    IsDeleted = j.IsDeleted,
                    Location = j.Employers.CompanyAddress,
                    CompanyName = j.Employers.CompanyName
                }).Where(j => j.IsDeleted == 0)
                    .ToListAsync();

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<JobPostingDtos> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _context.JobPosting.FirstOrDefaultAsync(i => i.JobPostingId == id);
                return new JobPostingDtos
                {
                    JobPostingId = entity.JobPostingId,
                    Title = entity.Title,
                    Description = entity.Description,
                    Location = entity.Location,
                    EmploymentType = entity.EmploymentType,
                    PostedDate = entity.PostedDate,
                    JobCategory = entity.JobCategory,
                    ExpiredDate = entity.ExpiredDate,
                    SalaryFrom = entity.SalaryFrom,
                    JobRequirements = entity.JobRequirements,
                    SalaryTo = entity.SalaryTo,
                };
            }
            catch (Exception)
            {

                throw;
            }
   

        }

        public async Task<bool> AddAsync(JobPostingDtos jobPostingDto)
        {

            try
            {
                var userId = Utilities.GetUserId();
                if(userId is null )
                {
                    throw new Exception("Invalid or missing user ID in claims.");
                }

                var hr = await _context.HumanResource.FirstOrDefaultAsync(i => i.UserAccountId == userId);

                var saving = new JobPosting
                {
                    Title = jobPostingDto.Title,
                    Description = jobPostingDto.Description,
                    Location = jobPostingDto.Location,
                    EmploymentType = jobPostingDto.EmploymentType,
                    SalaryFrom = jobPostingDto.SalaryFrom,
                    SalaryTo = jobPostingDto.SalaryTo,
                    JobRequirements = jobPostingDto.JobRequirements,
                    JobCategory = jobPostingDto.JobCategory,
                    PostedDate = DateTime.Now,
                    ExpiredDate = jobPostingDto.ExpiredDate ?? DateTime.Now,
                    HumanResourceId = hr.HumanResourceId,
                };

                await _context.AddAsync(saving);
                await _context.SaveChangesAsync();
                return true;

            }
            catch (Exception)
            {

                throw;
            }

        }


        public async Task<bool> UpdateAsync(JobPostingDtos jobPostingDto)
           {

            try
            {
                var userId = Utilities.GetUserId();
                if (userId is null)
                {
                    throw new Exception("Invalid or missing user ID in claims.");
                }


                var hr = await _context.HumanResource.FirstOrDefaultAsync(i => i.UserAccountId == userId);

                var existing = await _context.JobPosting
                    .FirstOrDefaultAsync(j => j.JobPostingId == jobPostingDto.JobPostingId);

                if (existing == null)
                    return false;


                existing.Title = jobPostingDto.Title;
                existing.Description = jobPostingDto.Description;
                existing.Location = jobPostingDto.Location;
                existing.EmploymentType = jobPostingDto.EmploymentType;
                existing.SalaryFrom = jobPostingDto.SalaryFrom;
                existing.SalaryTo = jobPostingDto.SalaryTo;
                existing.JobRequirements = jobPostingDto.JobRequirements;
                existing.JobCategory = jobPostingDto.JobCategory;
                existing.IsDeleted = jobPostingDto.IsDeleted;
                existing.HumanResourceId = hr.HumanResourceId;
                existing.PostedDate = DateTime.Now;
                existing.ExpiredDate = jobPostingDto.ExpiredDate ?? DateTime.Now;


                _context.JobPosting.Update(existing);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var existing = await _context.JobPosting
     .FirstOrDefaultAsync(i => i.JobPostingId == id);

                if (existing == null)
                    return false;

                existing.IsDeleted = 1;

                _context.JobPosting.Update(existing);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}

