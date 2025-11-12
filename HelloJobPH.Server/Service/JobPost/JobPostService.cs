using AutoMapper;
using HelloJobPH.Employer.Pages.JobPost;
using HelloJobPH.Employer.Pages.SuperAdmin;
using HelloJobPH.Employer.Services.HumanResource;
using HelloJobPH.Server.Data;
using HelloJobPH.Server.GeneralReponse;
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
            var userId = Utilities.GetUserId();

            var hr = await _context.HumanResource
                .FirstOrDefaultAsync(i => i.UserAccountId == userId);

            var employer = await _context.Employer
                .FirstOrDefaultAsync(i => i.UserAccountId == userId);

            try
            {
                var employerId = hr?.EmployerId ?? employer?.EmployerId;

                if (employerId == null)
                    throw new Exception("No associated employer found for this user.");

                var response = await _context.JobPosting
                    .Where(j => j.IsDeleted == 0 && j.EmployerId == employerId)
                    .Select(j => new JobPostingDtos
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
                        IsActive = j.IsActive,
                        IsDeleted = j.IsDeleted,
                        Location = j.Employer.CompanyAddress + ", " + j.Employer.City + ", " + j.Employer.Province,
                        CompanyName = j.Employer.CompanyName
                    })
                    .ToListAsync();

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<GeneralResponse<JobPostingDtos>> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _context.JobPosting
                    .FirstOrDefaultAsync(i => i.JobPostingId == id);

                if (entity == null)
                    return GeneralResponse<JobPostingDtos>.Fail($"Job post with ID {id} not found.");

                var dto = new JobPostingDtos
                {
                    JobPostingId = entity.JobPostingId,
                    Title = entity.Title,
                    Description = entity.Description,
                    Location = entity.Location,
                    EmploymentType = entity.EmploymentType,
                    PostedDate = entity.PostedDate,
                    JobCategory = entity.JobCategory,
                    SalaryFrom = entity.SalaryFrom,
                    SalaryTo = entity.SalaryTo,
                    JobRequirements = entity.JobRequirements
                };

                return GeneralResponse<JobPostingDtos>.Ok("Job post retrieved successfully.", dto);
            }
            catch (Exception ex)
            {
                return GeneralResponse<JobPostingDtos>.Fail("Error retrieving job post: " + ex.Message);
            }
        }

        public async Task<GeneralResponse<JobPostingDtos>> AddAsync(JobPostingDtos jobPostingDto)
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
                    HumanResourceId = hr.HumanResourceId,
                    EmployerId = hr.EmployerId,
                };

                await _context.AddAsync(saving);
                await _context.SaveChangesAsync();


                var postaudit = new JobPostAudit
                {
                    Action = $"Added new Job post {saving.Title}",
                    ChangedAt = DateTime.UtcNow,
                    EmployerId = hr.EmployerId,
                    JobPostingId = saving.JobPostingId,
                   HumanResourceId = hr.HumanResourceId,
                };
                await _context.JobPostAudit.AddAsync(postaudit);
                await _context.SaveChangesAsync();

                return GeneralResponse<JobPostingDtos>.Ok("Job post added successfully.", jobPostingDto);

            }
            catch (Exception ex)
            {
                return GeneralResponse<JobPostingDtos>.Fail("Error adding job post: " + ex.Message);
            }

        }

        public async Task<GeneralResponse<JobPostingDtos>> UpdateAsync(JobPostingDtos jobPostingDto)
        {
            try
            {
                var userId = Utilities.GetUserId();
                if (userId is null)
                    throw new Exception("Invalid or missing user ID in claims.");

                var hr = await _context.HumanResource.FirstOrDefaultAsync(i => i.UserAccountId == userId);
                if (hr is null)
                    throw new Exception("Human Resource not found for user.");

                var existing = await _context.JobPosting
                    .FirstOrDefaultAsync(j => j.JobPostingId == jobPostingDto.JobPostingId);

                if (existing == null)
                    return GeneralResponse<JobPostingDtos>.Fail("job post failed to update");

                // Capture original values
                var original = new
                {
                    existing.Title,
                    existing.Description,
                    existing.Location,
                    existing.EmploymentType,
                    existing.SalaryFrom,
                    existing.SalaryTo,
                    existing.JobRequirements,
                    existing.JobCategory,
                    existing.IsDeleted
                };

                // Track changed fields only
                var changes = new List<string>();

                void Compare<T>(string propName, T oldVal, T newVal)
                {
                    if (!Equals(oldVal, newVal))
                    {
                        changes.Add($"{propName}: '{oldVal}' → '{newVal}'");
                    }
                }

                // Compare properties
                Compare(nameof(existing.Title), original.Title, jobPostingDto.Title);
                Compare(nameof(existing.Description), original.Description, jobPostingDto.Description);
                Compare(nameof(existing.Location), original.Location, jobPostingDto.Location);
                Compare(nameof(existing.EmploymentType), original.EmploymentType, jobPostingDto.EmploymentType);
                Compare(nameof(existing.SalaryFrom), original.SalaryFrom, jobPostingDto.SalaryFrom);
                Compare(nameof(existing.SalaryTo), original.SalaryTo, jobPostingDto.SalaryTo);
                Compare(nameof(existing.JobRequirements), original.JobRequirements, jobPostingDto.JobRequirements);
                Compare(nameof(existing.JobCategory), original.JobCategory, jobPostingDto.JobCategory);
                Compare(nameof(existing.IsDeleted), original.IsDeleted, jobPostingDto.IsDeleted);

                // Update entity values
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
                existing.EmployerId = hr.EmployerId;

                _context.JobPosting.Update(existing);
                await _context.SaveChangesAsync();

                // Create audit log only if there are changes
                if (changes.Count > 0)
                {
                    var postaudit = new JobPostAudit
                    {
                        JobPostingId = existing.JobPostingId,
                        EmployerId = hr.EmployerId,
                        HumanResourceId = hr.HumanResourceId,
                        ChangedAt = DateTime.UtcNow,
                        Action = "Updated fields: " + string.Join(", ", changes)
                    };

                    await _context.JobPostAudit.AddAsync(postaudit);
                    await _context.SaveChangesAsync();
                }

                return GeneralResponse<JobPostingDtos>.Ok("Successfully updated");
            }
            catch (Exception)
            {
                throw;
            }
        }



        public async Task<GeneralResponse<bool>> DeleteAsync(int id)
        {
            try
            {
                var userId = Utilities.GetUserId();
                if (userId is null)
                    throw new Exception("Invalid or missing user ID in claims.");

                var hr = await _context.HumanResource.FirstOrDefaultAsync(i => i.UserAccountId == userId);
                if (hr is null)
                    throw new Exception("Human Resource not found for user.");

                var existing = await _context.JobPosting
                    .FirstOrDefaultAsync(i => i.JobPostingId == id);

                if (existing == null)
                    return GeneralResponse<bool>.Fail("Job post is not existing");

                // Perform soft delete
                existing.IsDeleted = 1;
                _context.JobPosting.Update(existing);
                await _context.SaveChangesAsync();

                // Create audit entry
                var postaudit = new JobPostAudit
                {
                    JobPostingId = existing.JobPostingId,
                    EmployerId = hr.EmployerId,
                    HumanResourceId = hr.HumanResourceId,
                    ChangedAt = DateTime.UtcNow,
                    Action = $"Deleted Job Post: '{existing.Title}' (ID: {existing.JobPostingId})"
                };

                await _context.JobPostAudit.AddAsync(postaudit);
                await _context.SaveChangesAsync();

                return GeneralResponse<bool>.Ok("Successfully Deleted");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GeneralResponse<bool>> Activate(int id)
        {
            try
            {
                var userId = Utilities.GetUserId();

                var hr = await _context.HumanResource
                    .FirstOrDefaultAsync(i => i.UserAccountId == userId);

                if (hr == null)
                    throw new Exception("Human resource not found.");

                var existing = await _context.JobPosting
                    .FirstOrDefaultAsync(i => i.JobPostingId == id);

                if (existing == null)
                    return GeneralResponse<bool>.Fail("Job post is not existing");

                existing.IsActive = 1;

                   _context.JobPosting.Update(existing);
                await _context.SaveChangesAsync();

                var postaudit = new JobPostAudit
                {
                    JobPostingId = existing.JobPostingId,
                    EmployerId = hr.EmployerId,
                    HumanResourceId = hr.HumanResourceId,
                    ChangedAt = DateTime.UtcNow,
                    Action = $"Activated Job Post: '{existing.Title}' (ID: {existing.JobPostingId})"
                };

    
                await _context.JobPostAudit.AddAsync(postaudit);

                await _context.SaveChangesAsync();

                return GeneralResponse<bool>.Ok("Successfully activated");
            }
            catch (Exception ex)
            {
                // Optionally log error
                return GeneralResponse<bool>.Fail("Error"+ ex.Message);
            }
        }

        public async Task<GeneralResponse<bool>> Deactivate(int id)
        {
            try
            {
                var userId = Utilities.GetUserId();

                var hr = await _context.HumanResource
                    .FirstOrDefaultAsync(i => i.UserAccountId == userId);

                if (hr == null)
                    throw new Exception("Human resource not found.");

                var existing = await _context.JobPosting
                    .FirstOrDefaultAsync(i => i.JobPostingId == id);

                if (existing == null)
                    return GeneralResponse<bool>.Fail("Job post is not existing");

                existing.IsActive = 0;
                _context.JobPosting.Update(existing);
                await _context.SaveChangesAsync();

                var postaudit = new JobPostAudit
                {
                    JobPostingId = existing.JobPostingId,
                    EmployerId = hr.EmployerId,
                    HumanResourceId = hr.HumanResourceId,
                    ChangedAt = DateTime.UtcNow,
                    Action = $"Deactivated Job Post: '{existing.Title}' (ID: {existing.JobPostingId})"
                };

                await _context.JobPostAudit.AddAsync(postaudit);

                await _context.SaveChangesAsync();

                return GeneralResponse<bool>.Ok("Successfully deactivated the post");
            }
            catch (Exception ex)
            {
                return GeneralResponse<bool>.Fail("Error"+ex.Message);
            }
        }


    }
}

