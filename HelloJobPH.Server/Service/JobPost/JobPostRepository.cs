using HelloJobPH.Server.Data;
using HelloJobPH.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace HelloJobPH.Server.Service.JobPost
{
    public class JobPostRepository : IJobPostRepository
    {
        private readonly ApplicationDbContext _context;
        public JobPostRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(JobPosting entity)
        {
            await _context.JobPosting.AddAsync(entity);
        }

        public async Task<JobPosting?> GetByIdAsync(int id)
        {
            return _context.JobPosting.Find(id);
        }

        public async Task<List<JobPosting>> RetrieveAllAsync()
        {
            return await _context.JobPosting.ToListAsync();
        }

        public void Update(JobPosting entity)
        {
            _context.JobPosting.Update(entity);
        }
    }
}
