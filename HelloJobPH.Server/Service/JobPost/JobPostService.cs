using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Model;  // Assuming JobPosting entity is here
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using HelloJobPH.Server.Data;

namespace HelloJobPH.Server.Service.JobPost
{
    public class JobPostService : IJobPostService
    {
        private readonly ApplicationDbContext _context;
            private readonly JobPostRepository _repository;
        private readonly IMapper _mapper;

        public JobPostService(JobPostRepository repository, IMapper mapper,ApplicationDbContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
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
                    JobRequirements = i.JobRequirements,
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
                ExpiredDate = entity.ExpiredDate,
                SalaryFrom = entity.SalaryFrom,
                JobRequirements = entity.JobRequirements,
                SalaryTo = entity.SalaryTo,
            };


            //var jobEntity = await _repository.GetByIdAsync(id);
            //return _mapper.Map<JobPostingDtos>(jobEntity);
        }

        public async Task<JobPostingDtos> AddAsync(JobPostingDtos jobPostingDto)
        {


            var saving = new JobPosting
            {
                Title = jobPostingDto.Title,
                Description = jobPostingDto.Description,
                Location = jobPostingDto.Location,
                EmploymentType = jobPostingDto.EmploymentType,
                JobRequirements = jobPostingDto.JobRequirements,
                PostedDate = DateTime.Now,
                ExpiredDate = jobPostingDto.ExpiredDate ?? DateTime.Now,
                HumanResourceId=1,
            };
            await _context.AddAsync(saving);
            await _context.SaveChangesAsync();

            return new JobPostingDtos

            {
                JobPostingId = saving.JobPostingId,
                Title = saving.Title,
                Description = saving.Description,
                Location = saving.Location,
                EmploymentType = saving.EmploymentType,
                PostedDate = saving.PostedDate,
                ExpiredDate = saving.ExpiredDate
            };

            //var entity = _mapper.Map<JobPosting>(jobPostingDto);
            //var createdEntity = await _repository.AddAsync(entity);
            //return _mapper.Map<JobPostingDtos>(createdEntity);
        }

        public async Task<JobPostingDtos> UpdateAsync(JobPostingDtos jobPostingDto)
           {
            var existing = await _context.JobPosting
                .FirstOrDefaultAsync(j => j.JobPostingId == jobPostingDto.JobPostingId);

            if (existing == null)
                return null; // or throw new Exception("Job posting not found");

            // ✅ Manually map allowed fields
            existing.Title = jobPostingDto.Title;
            existing.Description = jobPostingDto.Description;
            existing.Location = jobPostingDto.Location;
            existing.EmploymentType = jobPostingDto.EmploymentType;
            existing.SalaryFrom = jobPostingDto.SalaryFrom;
            existing.SalaryTo = jobPostingDto.SalaryTo;
            existing.JobRequirements = jobPostingDto.JobRequirements;
            existing.IsDeleted = jobPostingDto.IsDeleted;
            existing.HumanResourceId = 1;
            existing.PostedDate = DateTime.Now;
            existing.ExpiredDate = jobPostingDto.ExpiredDate ?? DateTime.Now;

        
            _context.JobPosting.Update(existing);
            await _context.SaveChangesAsync();

            return new JobPostingDtos
            {
                JobPostingId = existing.JobPostingId,
                Title = existing.Title,
                Description = existing.Description,
                Location = existing.Location,
                EmploymentType = existing.EmploymentType,
                SalaryFrom = existing.SalaryFrom,
                SalaryTo = existing.SalaryTo,
                JobRequirements = existing.JobRequirements,
                IsDeleted = existing.IsDeleted,
                HumanResourceId = 1,
                PostedDate = existing.PostedDate,
                ExpiredDate = existing.ExpiredDate
            };
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

