using HelloJobPH.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace HelloJobPH.Server.Service.Resume
{
    public class ResumeService : IResumeService
    {
        private readonly ApplicationDbContext _context;

        public ResumeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<byte[]?> GetResumeBytesAsync(int applicationId)
        {
            // Example: Fetch resume from DB
            var resume = await _context.Application
                                       .Where(a => a.ApplicationId == applicationId)
                                       .Select(a => a.Resume)
                                       .FirstOrDefaultAsync();

            if (resume == null || resume.ResumeFileData == null)
                return null;

            return resume.ResumeFileData;
        }
    }
}
