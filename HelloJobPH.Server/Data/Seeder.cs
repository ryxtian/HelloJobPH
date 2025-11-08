using HelloJobPH.Server.Data;
using HelloJobPH.Shared.Enums;
using HelloJobPH.Shared.Model;
using Microsoft.EntityFrameworkCore;

public class Seeder
{
    private readonly ApplicationDbContext _context;

    public Seeder(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        if (await _context.UserAccount.AnyAsync()) return;

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword("test123");

        // 1️⃣ USER ACCOUNTS
        var users = new List<UserAccount>
        {
            new() { Email = "admin@jobportal.com", Password = hashedPassword, Role = "Admin" },
            new() { Email = "hr@techcorp.com", Password = hashedPassword, Role = "Employer" },
            new() { Email = "hr@innovate.com", Password = hashedPassword, Role = "Employer" },
            new() { Email = "hr@globalfinance.com", Password = hashedPassword, Role = "Employer" },
            new() { Email = "john.hr@techcorp.com", Password = hashedPassword, Role = "HR" },
            new() { Email = "sarah.hr@innovate.com", Password = hashedPassword, Role = "HR" },
            new() { Email = "mike.hr@globalfinance.com", Password = hashedPassword, Role = "HR" },
            new() { Email = "juan.delacruz@email.com", Password = hashedPassword, Role = "Applicant" },
            new() { Email = "maria.santos@email.com", Password = hashedPassword, Role = "Applicant" },
            new() { Email = "pedro.reyes@email.com", Password = hashedPassword, Role = "Applicant" },
            new() { Email = "ana.garcia@email.com", Password = hashedPassword, Role = "Applicant" },
            new() { Email = "carlos.mendoza@email.com", Password = hashedPassword, Role = "Applicant" }
        };

        await _context.UserAccount.AddRangeAsync(users);
        await _context.SaveChangesAsync();

        // 2️⃣ EMPLOYERS
        var employers = new List<Employer>
        {
            new()
            {
                CompanyName = "TechCorp Solutions",
                Industry = "Information Technology",
                Description = "Leading software development company specializing in enterprise solutions",
                CompanyAddress = "5th Floor, Ortigas Center Building",
                City = "Pasig City",
                Province = "Metro Manila",
                ZipCode = "1605",
                ContactEmail = "hr@techcorp.com",
                ContactNumber = "+63 2 8123 4567",
                Website = "https://www.techcorp.com",
                UserAccountId = users[1].UserAccountId,
                DateRegistered = DateTime.UtcNow.AddMonths(-6),
                IsVerified = true,
                IsActive = true,
                Status = "Active"
            },
            new()
            {
                CompanyName = "Innovate Digital Marketing",
                Industry = "Marketing & Advertising",
                Description = "Full-service digital marketing agency helping brands grow online",
                CompanyAddress = "BGC Corporate Center",
                City = "Taguig City",
                Province = "Metro Manila",
                ZipCode = "1634",
                ContactEmail = "hr@innovate.com",
                ContactNumber = "+63 2 8234 5678",
                Website = "https://www.innovate.com",
                UserAccountId = users[2].UserAccountId,
                DateRegistered = DateTime.UtcNow.AddMonths(-4),
                IsVerified = true,
                IsActive = true,
                Status = "Active"
            },
            new()
            {
                CompanyName = "Global Finance Corporation",
                Industry = "Banking & Finance",
                Description = "Premier financial services provider with nationwide coverage",
                CompanyAddress = "Makati Business District",
                City = "Makati City",
                Province = "Metro Manila",
                ZipCode = "1200",
                ContactEmail = "hr@globalfinance.com",
                ContactNumber = "+63 2 8345 6789",
                Website = "https://www.globalfinance.com",
                UserAccountId = users[3].UserAccountId,
                DateRegistered = DateTime.UtcNow.AddMonths(-8),
                IsVerified = true,
                IsActive = true,
                Status = "Active"
            }
        };

        await _context.Employer.AddRangeAsync(employers);
        await _context.SaveChangesAsync();

        // 3️⃣ HUMAN RESOURCES
        var hrPersonnel = new List<HumanResources>
        {
            new()
            {
                Firstname = "John",
                Lastname = "Smith",
                PhoneNumber = "+63 917 123 4567",
                ProfilePhotoUrl = "/images/hr/john-smith.jpg",
                JobTitle = "Senior HR Manager",
                UserAccountId = users[4].UserAccountId,
                EmployerId = employers[0].EmployerId
            },
            new()
            {
                Firstname = "Sarah",
                Lastname = "Johnson",
                PhoneNumber = "+63 917 234 5678",
                ProfilePhotoUrl = "/images/hr/sarah-johnson.jpg",
                JobTitle = "HR Specialist",
                UserAccountId = users[5].UserAccountId,
                EmployerId = employers[1].EmployerId
            },
            new()
            {
                Firstname = "Michael",
                Lastname = "Brown",
                PhoneNumber = "+63 917 345 6789",
                ProfilePhotoUrl = "/images/hr/michael-brown.jpg",
                JobTitle = "Recruitment Manager",
                UserAccountId = users[6].UserAccountId,
                EmployerId = employers[2].EmployerId
            }
        };

        await _context.HumanResource.AddRangeAsync(hrPersonnel);
        await _context.SaveChangesAsync();

        // 4️⃣ APPLICANTS
        var applicants = new List<Applicant>
        {
            new() { Firstname = "Juan", Middlename = "Pedro", Surname = "Dela Cruz", Address = "123 Rizal Street, Brgy. San Antonio", Phone = "+63 917 111 2222", Birthday = new DateTime(1995, 5, 15), UserAccountId = users[7].UserAccountId },
            new() { Firstname = "Maria", Middlename = "Clara", Surname = "Santos", Address = "456 Bonifacio Avenue, Brgy. Poblacion", Phone = "+63 917 222 3333", Birthday = new DateTime(1998, 8, 20), UserAccountId = users[8].UserAccountId },
            new() { Firstname = "Pedro", Middlename = "Miguel", Surname = "Reyes", Address = "789 Aguinaldo Street, Brgy. Centro", Phone = "+63 917 333 4444", Birthday = new DateTime(1992, 3, 10), UserAccountId = users[9].UserAccountId },
            new() { Firstname = "Ana", Middlename = "Luisa", Surname = "Garcia", Address = "321 Mabini Road, Brgy. Santo Niño", Phone = "+63 917 444 5555", Birthday = new DateTime(1996, 11, 25), UserAccountId = users[10].UserAccountId },
            new() { Firstname = "Carlos", Middlename = "Antonio", Surname = "Mendoza", Address = "654 Luna Street, Brgy. San Jose", Phone = "+63 917 555 6666", Birthday = new DateTime(1994, 7, 8), UserAccountId = users[11].UserAccountId }
        };

        await _context.Applicant.AddRangeAsync(applicants);
        await _context.SaveChangesAsync();

        // 5️⃣ JOB POSTINGS
        var jobPostings = new List<JobPosting>
        {
            new()
            {
                Title = "Senior Software Engineer",
                Description = "Experienced Senior Software Engineer with .NET Core expertise. Responsible for designing, developing, and maintaining high-quality enterprise applications.",
                JobRequirements = "• Bachelor's degree in Computer Science or related field\n• 5+ years of experience in software development using C#/.NET Core\n• Strong understanding of REST APIs, Entity Framework, and SQL Server\n• Familiarity with frontend technologies (React, Blazor, or Angular)\n• Excellent problem-solving and communication skills",
                Location = "Pasig City",
                EmploymentType = EmploymentType.FullTime,
                SalaryFrom = 80000,
                SalaryTo = 120000,
                JobCategory = JobCategory.InformationAndCommunicationTechnology,
                HumanResourceId = hrPersonnel[0].HumanResourceId,
                EmployerId = employers[0].EmployerId,
                PostedDate = DateTime.UtcNow.AddDays(-15),
                IsActive = 1
            },
            new()
            {
                Title = "Digital Marketing Specialist",
                Description = "Creative digital marketer responsible for planning, executing, and optimizing online marketing campaigns to increase brand visibility and conversions.",
                JobRequirements = "• Bachelor's degree in Marketing, Communications, or related field\n• At least 2 years of experience in digital marketing\n• Proficient in SEO, SEM, Google Ads, and social media management\n• Strong analytical skills and data-driven mindset\n• Excellent writing and communication skills",
                Location = "Taguig City",
                EmploymentType = EmploymentType.FullTime,
                SalaryFrom = 35000,
                SalaryTo = 50000,
                JobCategory = JobCategory.MarketingAndCommunications,
                HumanResourceId = hrPersonnel[1].HumanResourceId,
                EmployerId = employers[1].EmployerId,
                PostedDate = DateTime.UtcNow.AddDays(-10),
                IsActive = 1
            },
            new()
            {
                Title = "Financial Analyst",
                Description = "Finance professional responsible for preparing financial reports, analyzing data, and supporting budgeting and forecasting activities.",
                JobRequirements = "• Bachelor's degree in Finance, Accounting, or Business Administration\n• Minimum of 3 years of experience in financial analysis or reporting\n• Proficient in Excel and financial modeling\n• Strong analytical and problem-solving skills\n• Knowledge of financial software (QuickBooks, SAP, or similar) is a plus",
                Location = "Makati City",
                EmploymentType = EmploymentType.FullTime,
                SalaryFrom = 45000,
                SalaryTo = 65000,
                JobCategory = JobCategory.BankingAndFinancialServices,
                HumanResourceId = hrPersonnel[2].HumanResourceId,
                EmployerId = employers[2].EmployerId,
                PostedDate = DateTime.UtcNow.AddDays(-7),
                IsActive = 1
            }
        };

        await _context.JobPosting.AddRangeAsync(jobPostings);
        await _context.SaveChangesAsync();

        // 6️⃣ RESUMES
        var resumes = new List<Resume>
        {
            new() { ResumeFileData = null, ApplicantId = applicants[0].ApplicantId, IsActive = 1 },
            new() { ResumeFileData = null, ApplicantId = applicants[1].ApplicantId, IsActive = 1 },
            new() { ResumeFileData = null, ApplicantId = applicants[2].ApplicantId, IsActive = 1 }
        };

        await _context.Resume.AddRangeAsync(resumes);
        await _context.SaveChangesAsync();

        // 7️⃣ APPLICATIONS
        var applications = new List<Application>
        {
            new()
            {
                ApplicantId = applicants[0].ApplicantId,
                JobPostingId = jobPostings[0].JobPostingId,
                ResumeId = resumes[0].ResumeId,
                ApplicationStatus = ApplicationStatus.Pending,
                HumanResourcesId = hrPersonnel[0].HumanResourceId,
                DateApply = DateTime.UtcNow.AddDays(-12),
                CoverLetter = "I am writing to express my strong interest in the position.",
                EmployerId = jobPostings[0].EmployerId
            },
            new()
            {
                ApplicantId = applicants[1].ApplicantId,
                JobPostingId = jobPostings[1].JobPostingId,
                ResumeId = resumes[1].ResumeId,
                ApplicationStatus = ApplicationStatus.Pending,
                HumanResourcesId = hrPersonnel[1].HumanResourceId,
                DateApply = DateTime.UtcNow.AddDays(-8),
                CoverLetter = "Excited to apply for the marketing position.",
                EmployerId = jobPostings[1].EmployerId
            },
            new()
            {
                ApplicantId = applicants[2].ApplicantId,
                JobPostingId = jobPostings[2].JobPostingId,
                ResumeId = resumes[2].ResumeId,
                ApplicationStatus = ApplicationStatus.Initial,
                HumanResourcesId = hrPersonnel[2].HumanResourceId,
                DateApply = DateTime.UtcNow.AddDays(-6),
                CoverLetter = "With strong finance background, ready to contribute.",
                EmployerId = jobPostings[2].EmployerId
            }
        };

        await _context.Application.AddRangeAsync(applications);
        await _context.SaveChangesAsync();

        // 8️⃣ INTERVIEWS
        var interviews = new List<Interview>
        {
            new() { ApplicationId = applications[1].ApplicationId, ScheduledDate = DateTime.UtcNow.AddDays(3), ScheduledTime = new TimeSpan(10, 0, 0), Mode = "Virtual - Google Meet", HumanResourceId = hrPersonnel[1].HumanResourceId },
            new() { ApplicationId = applications[2].ApplicationId, ScheduledDate = DateTime.UtcNow.AddDays(-2), ScheduledTime = new TimeSpan(14, 30, 0), Mode = "On-site", HumanResourceId = hrPersonnel[2].HumanResourceId }
        };

        await _context.Interview.AddRangeAsync(interviews);
        await _context.SaveChangesAsync();

        // 9️⃣ INTERVIEW HISTORY
        var interviewHistories = new List<InterviewHistory>
        {
            new()
            {
                ApplicationId = applications[0].ApplicationId,
                CandidateName = "Juan Pedro Dela Cruz",
                Stage = "Initial Screening",
                Status = "Completed",
                InterviewBy = "John Smith",
                ScheduledDate = DateTime.UtcNow.AddDays(-10),
                CreatedAt = DateTime.UtcNow.AddDays(-10)
            },
            new()
            {
                ApplicationId = applications[0].ApplicationId,
                CandidateName = "Juan Pedro Dela Cruz",
                Stage = "Technical Interview",
                Status = "Scheduled",
                InterviewBy = "John Smith",
                ScheduledDate = DateTime.UtcNow.AddDays(2),
                CreatedAt = DateTime.UtcNow.AddDays(-8)
            },
            new()
            {
                ApplicationId = applications[1].ApplicationId,
                CandidateName = "Maria Clara Santos",
                Stage = "Initial Screening",
                Status = "Scheduled",
                InterviewBy = "Sarah Johnson",
                ScheduledDate = DateTime.UtcNow.AddDays(3),
                CreatedAt = DateTime.UtcNow.AddDays(-7)
            },
            new()
            {
                ApplicationId = applications[2].ApplicationId,
                CandidateName = "Pedro Miguel Reyes",
                Stage = "Initial Screening",
                Status = "Completed",
                InterviewBy = "Michael Brown",
                ScheduledDate = DateTime.UtcNow.AddDays(-2),
                CreatedAt = DateTime.UtcNow.AddDays(-5)
            },
            new()
            {
                ApplicationId = applications[2].ApplicationId,
                CandidateName = "Pedro Miguel Reyes",
                Stage = "HR Interview",
                Status = "Completed",
                InterviewBy = "Michael Brown",
                ScheduledDate = DateTime.UtcNow.AddDays(-1),
                CreatedAt = DateTime.UtcNow.AddDays(-3)
            }
        };

        await _context.InterviewHistory.AddRangeAsync(interviewHistories);
        await _context.SaveChangesAsync();

        // 🔟 AUDIT LOGS
        var auditLogs = new List<AuditLog>
        {
            new()
            {
                ApplicationId = applications[0].ApplicationId,
                ApplicantId = applicants[0].ApplicantId,
                JobPostingId = jobPostings[0].JobPostingId,
                HumanResourcesId = hrPersonnel[0].HumanResourceId,
                Action = "Applied",
                Details = "Applicant Juan Dela Cruz applied for Senior Software Engineer position.",
                Timestamp = DateTime.UtcNow.AddDays(-11),
                EmployerId = hrPersonnel[0].EmployerId
            },
            new()
            {
                ApplicationId = applications[1].ApplicationId,
                ApplicantId = applicants[1].ApplicantId,
                JobPostingId = jobPostings[1].JobPostingId,
                HumanResourcesId = hrPersonnel[1].HumanResourceId,
                Action = "Interview Scheduled",
                Details = "Interview scheduled for Maria Santos on Google Meet.",
                Timestamp = DateTime.UtcNow.AddDays(-7),
                EmployerId = hrPersonnel[1].EmployerId
            },
            new()
            {
                ApplicationId = applications[2].ApplicationId,
                ApplicantId = applicants[2].ApplicantId,
                JobPostingId = jobPostings[2].JobPostingId,
                HumanResourcesId = hrPersonnel[2].HumanResourceId,
                Action = "Application Reviewed",
                Details = "Finance HR reviewed Pedro Reyes' application.",
                Timestamp = DateTime.UtcNow.AddDays(-5),
                EmployerId = hrPersonnel[2].EmployerId
            }
        };

        await _context.AuditLog.AddRangeAsync(auditLogs);
        await _context.SaveChangesAsync();
    }
}