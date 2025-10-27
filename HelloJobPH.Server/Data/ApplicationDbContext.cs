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
        public DbSet<HumanResources> HumanResource { get; set; }
        public DbSet<Application> Application { get; set; }
        public DbSet<Interview> Interview { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<JobPosting>().
        //        Property(u => u.EmploymentType).HasConversion<string>();
        //}
    }
}
