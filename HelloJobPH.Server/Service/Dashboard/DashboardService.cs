
using HelloJobPH.Server.Data;
using HelloJobPH.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace HelloJobPH.Server.Service.Dashboard
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;
        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> GetActiveApplicationAsync()
        {
            return await _context.Application
      .CountAsync(a =>
          a.ApplicationStatus == ApplicationStatus.Pending ||
          a.ApplicationStatus == ApplicationStatus.Viewed ||
          a.ApplicationStatus == ApplicationStatus.Accepted ||
          a.ApplicationStatus == ApplicationStatus.Initial ||
          a.ApplicationStatus == ApplicationStatus.Technical ||
          a.ApplicationStatus == ApplicationStatus.Final ||
          a.ApplicationStatus == ApplicationStatus.JobOffer ||
          a.ApplicationStatus == ApplicationStatus.Reschedule);
        }

        public async Task<int> GetApprovedHiredAsync()
        {
            return await _context.Application
         .CountAsync(a => a.ApplicationStatus == ApplicationStatus.Hired);
        }

        public async Task<int> GetTotalJobPostAsync()
        {
            return await _context.JobPosting.CountAsync();
        }
    }
}
