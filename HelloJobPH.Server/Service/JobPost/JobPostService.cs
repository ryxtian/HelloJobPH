using AutoMapper;
using HelloJobPH.Server.Data;
using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Model;  // Assuming JobPosting entity is here
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HelloJobPH.Server.Service.JobPost
{
    public class JobPostService : IJobPostService
    {
        private readonly ApplicationDbContext _context;
            //private readonly JobPostRepository _repository;
     
        IHttpContextAccessor _httpContextAccessor;

        public JobPostService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
          
          
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
      
        public async Task<List<JobPostingDtos>> RetrieveAllAsync()
        {
            List<JobPostingDtos> response = await _context.JobPosting
                .Select(i => new JobPostingDtos
                {
                    JobPostingId = i.JobPostingId,
                    Title = i.Title,
                    Description = i.Description,
                    Location = i.Location,
                    EmploymentType = i.EmploymentType,
                    SalaryFrom = i.SalaryFrom,
                    SalaryTo = i.SalaryTo,
                    JobRequirements = i.JobRequirements,
                    JobCategory = i.JobCategory,
                    PostedDate = i.PostedDate,
                    IsDeleted = i.IsDeleted
                }).Where(i=>i.IsDeleted == 0)
                .ToListAsync();



            return response;

        }

        public async Task<JobPostingDtos> GetByIdAsync(int id)
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

        public async Task<bool> AddAsync(JobPostingDtos jobPostingDto)
        {

            try
            {
                var user = _httpContextAccessor.HttpContext?.User;

                // ✅ Get the user’s ID from claims
                var userIdClaim = user?.FindFirstValue(ClaimTypes.NameIdentifier);
                if (!int.TryParse(userIdClaim, out int userId))
                    throw new Exception("Invalid or missing user ID in claims.");

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
                    HumanResourceId = userId,
                };

                await _context.AddAsync(saving);
                await _context.SaveChangesAsync();
                return true;
                //return new JobPostingDtos
                //{
                //    JobPostingId = saving.JobPostingId,
                //    Title = saving.Title,
                //    Description = saving.Description,
                //    Location = saving.Location,
                //    EmploymentType = saving.EmploymentType,
                //    PostedDate = saving.PostedDate,
                //    ExpiredDate = saving.ExpiredDate
                //};
            }
            catch (Exception)
            {

                throw;
            }

        }


        public async Task<bool> UpdateAsync(JobPostingDtos jobPostingDto)
           {

            var user = _httpContextAccessor.HttpContext?.User;

      
            var userIdClaim = user?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdClaim, out int userId))
                throw new Exception("Invalid or missing user ID in claims.");
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
            existing.HumanResourceId = userId;
            existing.PostedDate = DateTime.Now;
            existing.ExpiredDate = jobPostingDto.ExpiredDate ?? DateTime.Now;

        
            _context.JobPosting.Update(existing);
            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<bool> DeleteAsync(int id)
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

    }
}

