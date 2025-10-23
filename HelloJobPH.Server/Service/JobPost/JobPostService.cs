using HelloJobPH.Shared.DTOs;
using HelloJobPH.Shared.Model;  // Assuming JobPosting entity is here
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using HelloJobPH.Server.Data;

namespace HelloJobPH.Server.Service.JobPost
{
    public class JobPostService : IJobPostService
    {
        private readonly JobPostRepository _repository;
        private readonly IMapper _mapper;

        public JobPostService(JobPostRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<JobPostingDtos>> RetrieveAllAsync()
        {
            var jobEntities = await _repository.RetrieveAllAsync();
            return _mapper.Map<List<JobPostingDtos>>(jobEntities);
        }

        public async Task<JobPostingDtos> GetByIdAsync(int id)
        {
            var jobEntity = await _repository.GetByIdAsync(id);
            return _mapper.Map<JobPostingDtos>(jobEntity);
        }

        public async Task<JobPostingDtos> AddAsync(JobPostingDtos jobPostingDto)
        {
            var entity = _mapper.Map<JobPosting>(jobPostingDto);
            var createdEntity = await _repository.AddAsync(entity);
            return _mapper.Map<JobPostingDtos>(createdEntity);
        }

        public async Task<JobPostingDtos> UpdateAsync(JobPostingDtos jobPostingDto)
        {
            var entity = _mapper.Map<JobPosting>(jobPostingDto);
            var updatedEntity = await _repository.UpdateAsync(entity);
            return _mapper.Map<JobPostingDtos>(updatedEntity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}

