
using HelloJobPH.Server.Data;
using HelloJobPH.Server.Utility;
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
            var userId = Utilities.GetUserId();

            var hr = await _context.HumanResource
                .FirstOrDefaultAsync(i => i.UserAccountId == userId);

            if (hr is null)
            {
                throw new Exception("Human resource not found.");
            }

            try
            {

                return await _context.Application
                    .CountAsync(a =>
                        a.JobPosting.EmployerId == hr.EmployerId && (
                            a.ApplicationStatus == ApplicationStatus.Pending ||
                            a.ApplicationStatus == ApplicationStatus.Viewed ||
                            a.ApplicationStatus == ApplicationStatus.Accepted ||
                            a.ApplicationStatus == ApplicationStatus.Initial ||
                            a.ApplicationStatus == ApplicationStatus.Technical ||
                            a.ApplicationStatus == ApplicationStatus.Final ||
                            a.ApplicationStatus == ApplicationStatus.JobOffer ||
                            a.ApplicationStatus == ApplicationStatus.Reschedule
                        ));
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<int> GetApprovedHiredAsync()
        {
            var userId = Utilities.GetUserId();

            var hr = await _context.HumanResource
                .FirstOrDefaultAsync(i => i.UserAccountId == userId);

            if (hr is null)
            {
                throw new Exception("Human resource not found.");
            }

            try
            {
                return await _context.Application
                    .CountAsync(a => a.JobPosting.EmployerId == hr.EmployerId &&
                                     a.ApplicationStatus == ApplicationStatus.Hired);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<int> GetTotalJobPostAsync()
        {
            var userId = Utilities.GetUserId();

            var hr = await _context.HumanResource
                .FirstOrDefaultAsync(i => i.UserAccountId == userId);

            if (hr is null)
            {
                throw new Exception("Human resource not found.");
            }

            try
            {
 
                return await _context.JobPosting
                    .CountAsync(j => j.EmployerId == hr.EmployerId && j.IsDeleted == 0);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
