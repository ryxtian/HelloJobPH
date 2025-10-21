using HelloJobPH.Shared.Model;
using Microsoft.EntityFrameworkCore;

namespace HelloJobPH.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) :base(options) { }

        public DbSet<UserAccount> UserAccount { get; set; }
        public DbSet<Applicant> Applicant { get; set; }
        public DbSet<JobPosting> JobPosting { get; set; }
    }
}
