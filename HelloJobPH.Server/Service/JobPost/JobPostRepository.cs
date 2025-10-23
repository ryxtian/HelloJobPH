using AutoMapper;
using HelloJobPH.Server.Data;
using HelloJobPH.Server.Mapper;
using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Enums;
using HelloJobPH.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace HelloJobPH.Server.Service.JobPost
{
    public class JobPostRepository : IJobPostRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        public JobPostRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<List<JobPosting>> RetrieveAllAsync()
        {
            return await _context.JobPosting.Where(j => j.IsDeleted == 0).ToListAsync();
        }

        public async Task<JobPosting> GetByIdAsync(int id)
        {
            return await _context.JobPosting.FirstOrDefaultAsync(j => j.JobPostingId == id && j.IsDeleted == 0);
        }

        public async Task<JobPosting> AddAsync(JobPosting jobPosting)
        {
            jobPosting.PostedDate = DateTime.UtcNow;
            _context.JobPosting.Add(jobPosting);
            await _context.SaveChangesAsync();
            return jobPosting;
        }

        public async Task<JobPosting> UpdateAsync(JobPosting jobPosting)
        {
            var existing = await GetByIdAsync(jobPosting.JobPostingId);
            if (existing == null) return null;

            // Update fields here or just replace properties
            existing.Title = jobPosting.Title;
            existing.Description = jobPosting.Description;
            existing.Location = jobPosting.Location;
            existing.EmploymentType = jobPosting.EmploymentType;
            existing.SalaryFrom = jobPosting.SalaryFrom;
            existing.SalaryTo = jobPosting.SalaryTo;
            existing.JobRequirements = jobPosting.JobRequirements;
            existing.ExpiredDate = jobPosting.ExpiredDate;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await GetByIdAsync(id);
            if (existing == null) return false;

            existing.IsDeleted = 1;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
