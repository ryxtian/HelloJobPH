////using HelloJobPH.Server.Data;
////using HelloJobPH.Shared.Enums;
////using HelloJobPH.Shared.Model;
////using Microsoft.EntityFrameworkCore;

////public class Seeder
////{
////    private readonly ApplicationDbContext _context;

////    public Seeder(ApplicationDbContext context)
////    {
////        _context = context;
////    }

////    public async Task SeedAsync()
////    {
////        if (await _context.UserAccount.AnyAsync()) return;

////        var hashedPassword = BCrypt.Net.BCrypt.HashPassword("test123");

////        // 1️⃣ USER ACCOUNTS
////        var users = new List<UserAccount>
////        {
////            new() { Email = "admin@jobportal.com", Password = hashedPassword, Role = "Admin" },
////            new() { Email = "hr@techcorp.com", Password = hashedPassword, Role = "Employer" },
////            new() { Email = "hr@innovate.com", Password = hashedPassword, Role = "Employer" },
////            new() { Email = "hr@globalfinance.com", Password = hashedPassword, Role = "Employer" },
////            new() { Email = "john.hr@techcorp.com", Password = hashedPassword, Role = "HR" },
////            new() { Email = "sarah.hr@innovate.com", Password = hashedPassword, Role = "HR" },
////            new() { Email = "mike.hr@globalfinance.com", Password = hashedPassword, Role = "HR" },
////            new() { Email = "juan.delacruz@email.com", Password = hashedPassword, Role = "Applicant" },
////            new() { Email = "maria.santos@email.com", Password = hashedPassword, Role = "Applicant" },
////            new() { Email = "pedro.reyes@email.com", Password = hashedPassword, Role = "Applicant" },
////            new() { Email = "ana.garcia@email.com", Password = hashedPassword, Role = "Applicant" },
////            new() { Email = "carlos.mendoza@email.com", Password = hashedPassword, Role = "Applicant" }
////        };

////        await _context.UserAccount.AddRangeAsync(users);
////        await _context.SaveChangesAsync();

////        // 2️⃣ EMPLOYERS
////        var employers = new List<Employer>
////        {
////            new()
////            {
////                CompanyName = "TechCorp Solutions",
////                Industry = "Information Technology",
////                Description = "Leading software development company specializing in enterprise solutions",
////                CompanyAddress = "5th Floor, Ortigas Center Building",
////                City = "Pasig City",
////                Province = "Metro Manila",
////                ZipCode = "1605",
////                ContactEmail = "hr@techcorp.com",
////                ContactNumber = "+63 2 8123 4567",
////                Website = "https://www.techcorp.com",
////                UserAccountId = users[1].UserAccountId,
////                DateRegistered = DateTime.UtcNow.AddMonths(-6),
////                IsVerified = true,
////                IsActive = true,
////                Status = "Active"
////            },
////            new()
////            {
////                CompanyName = "Innovate Digital Marketing",
////                Industry = "Marketing & Advertising",
////                Description = "Full-service digital marketing agency helping brands grow online",
////                CompanyAddress = "BGC Corporate Center",
////                City = "Taguig City",
////                Province = "Metro Manila",
////                ZipCode = "1634",
////                ContactEmail = "hr@innovate.com",
////                ContactNumber = "+63 2 8234 5678",
////                Website = "https://www.innovate.com",
////                UserAccountId = users[2].UserAccountId,
////                DateRegistered = DateTime.UtcNow.AddMonths(-4),
////                IsVerified = true,
////                IsActive = true,
////                Status = "Active"
////            },
////            new()
////            {
////                CompanyName = "Global Finance Corporation",
////                Industry = "Banking & Finance",
////                Description = "Premier financial services provider with nationwide coverage",
////                CompanyAddress = "Makati Business District",
////                City = "Makati City",
////                Province = "Metro Manila",
////                ZipCode = "1200",
////                ContactEmail = "hr@globalfinance.com",
////                ContactNumber = "+63 2 8345 6789",
////                Website = "https://www.globalfinance.com",
////                UserAccountId = users[3].UserAccountId,
////                DateRegistered = DateTime.UtcNow.AddMonths(-8),
////                IsVerified = true,
////                IsActive = true,
////                Status = "Active"
////            }
////        };

////        await _context.Employer.AddRangeAsync(employers);
////        await _context.SaveChangesAsync();

////        // 3️⃣ HUMAN RESOURCES
////        var hrPersonnel = new List<HumanResources>
////        {
////            new()
////            {
////                Firstname = "John",
////                Lastname = "Smith",
////                PhoneNumber = "+63 917 123 4567",
////                ProfilePhotoUrl = "/images/hr/john-smith.jpg",
////                JobTitle = "Senior HR Manager",
////                UserAccountId = users[4].UserAccountId,
////                EmployerId = employers[0].EmployerId
////            },
////            new()
////            {
////                Firstname = "Sarah",
////                Lastname = "Johnson",
////                PhoneNumber = "+63 917 234 5678",
////                ProfilePhotoUrl = "/images/hr/sarah-johnson.jpg",
////                JobTitle = "HR Specialist",
////                UserAccountId = users[5].UserAccountId,
////                EmployerId = employers[1].EmployerId
////            },
////            new()
////            {
////                Firstname = "Michael",
////                Lastname = "Brown",
////                PhoneNumber = "+63 917 345 6789",
////                ProfilePhotoUrl = "/images/hr/michael-brown.jpg",
////                JobTitle = "Recruitment Manager",
////                UserAccountId = users[6].UserAccountId,
////                EmployerId = employers[2].EmployerId
////            }
////        };

////        await _context.HumanResource.AddRangeAsync(hrPersonnel);
////        await _context.SaveChangesAsync();

////        // 4️⃣ APPLICANTS
////        var applicants = new List<Applicant>
////        {
////            new() { Firstname = "Juan", Middlename = "Pedro", Surname = "Dela Cruz", Address = "123 Rizal Street, Brgy. San Antonio", Phone = "+63 917 111 2222", Birthday = new DateTime(1995, 5, 15), UserAccountId = users[7].UserAccountId },
////            new() { Firstname = "Maria", Middlename = "Clara", Surname = "Santos", Address = "456 Bonifacio Avenue, Brgy. Poblacion", Phone = "+63 917 222 3333", Birthday = new DateTime(1998, 8, 20), UserAccountId = users[8].UserAccountId },
////            new() { Firstname = "Pedro", Middlename = "Miguel", Surname = "Reyes", Address = "789 Aguinaldo Street, Brgy. Centro", Phone = "+63 917 333 4444", Birthday = new DateTime(1992, 3, 10), UserAccountId = users[9].UserAccountId },
////            new() { Firstname = "Ana", Middlename = "Luisa", Surname = "Garcia", Address = "321 Mabini Road, Brgy. Santo Niño", Phone = "+63 917 444 5555", Birthday = new DateTime(1996, 11, 25), UserAccountId = users[10].UserAccountId },
////            new() { Firstname = "Carlos", Middlename = "Antonio", Surname = "Mendoza", Address = "654 Luna Street, Brgy. San Jose", Phone = "+63 917 555 6666", Birthday = new DateTime(1994, 7, 8), UserAccountId = users[11].UserAccountId }
////        };

////        await _context.Applicant.AddRangeAsync(applicants);
////        await _context.SaveChangesAsync();

////        // 5️⃣ JOB POSTINGS
////        var jobPostings = new List<JobPosting>
////        {
////            new()
////            {
////                Title = "Senior Software Engineer",
////                Description = "Experienced Senior Software Engineer with .NET Core expertise. Responsible for designing, developing, and maintaining high-quality enterprise applications.",
////                JobRequirements = "• Bachelor's degree in Computer Science or related field\n• 5+ years of experience in software development using C#/.NET Core\n• Strong understanding of REST APIs, Entity Framework, and SQL Server\n• Familiarity with frontend technologies (React, Blazor, or Angular)\n• Excellent problem-solving and communication skills",
////                Location = "Pasig City",
////                EmploymentType = EmploymentType.FullTime,
////                SalaryFrom = 80000,
////                SalaryTo = 120000,
////                JobCategory = JobCategory.InformationAndCommunicationTechnology,
////                HumanResourceId = hrPersonnel[0].HumanResourceId,
////                EmployerId = employers[0].EmployerId,
////                PostedDate = DateTime.UtcNow.AddDays(-15),
////                IsActive = 1
////            },
////            new()
////            {
////                Title = "Digital Marketing Specialist",
////                Description = "Creative digital marketer responsible for planning, executing, and optimizing online marketing campaigns to increase brand visibility and conversions.",
////                JobRequirements = "• Bachelor's degree in Marketing, Communications, or related field\n• At least 2 years of experience in digital marketing\n• Proficient in SEO, SEM, Google Ads, and social media management\n• Strong analytical skills and data-driven mindset\n• Excellent writing and communication skills",
////                Location = "Taguig City",
////                EmploymentType = EmploymentType.FullTime,
////                SalaryFrom = 35000,
////                SalaryTo = 50000,
////                JobCategory = JobCategory.MarketingAndCommunications,
////                HumanResourceId = hrPersonnel[1].HumanResourceId,
////                EmployerId = employers[1].EmployerId,
////                PostedDate = DateTime.UtcNow.AddDays(-10),
////                IsActive = 1
////            },
////            new()
////            {
////                Title = "Financial Analyst",
////                Description = "Finance professional responsible for preparing financial reports, analyzing data, and supporting budgeting and forecasting activities.",
////                JobRequirements = "• Bachelor's degree in Finance, Accounting, or Business Administration\n• Minimum of 3 years of experience in financial analysis or reporting\n• Proficient in Excel and financial modeling\n• Strong analytical and problem-solving skills\n• Knowledge of financial software (QuickBooks, SAP, or similar) is a plus",
////                Location = "Makati City",
////                EmploymentType = EmploymentType.FullTime,
////                SalaryFrom = 45000,
////                SalaryTo = 65000,
////                JobCategory = JobCategory.BankingAndFinancialServices,
////                HumanResourceId = hrPersonnel[2].HumanResourceId,
////                EmployerId = employers[2].EmployerId,
////                PostedDate = DateTime.UtcNow.AddDays(-7),
////                IsActive = 1
////            }
////        };

////        await _context.JobPosting.AddRangeAsync(jobPostings);
////        await _context.SaveChangesAsync();

////        // 6️⃣ RESUMES
////        var resumes = new List<Resume>
////        {
////            new() { ResumeFileData = null, ApplicantId = applicants[0].ApplicantId },
////            new() { ResumeFileData = null, ApplicantId = applicants[1].ApplicantId },
////            new() { ResumeFileData = null, ApplicantId = applicants[2].ApplicantId }
////        };

////        await _context.Resume.AddRangeAsync(resumes);
////        await _context.SaveChangesAsync();

////        // 7️⃣ APPLICATIONS
////        var applications = new List<Application>
////        {
////            new()
////            {
////                ApplicantId = applicants[0].ApplicantId,
////                JobPostingId = jobPostings[0].JobPostingId,
////                ResumeId = resumes[0].ResumeId,
////                ApplicationStatus = ApplicationStatus.Pending,
////                HumanResourcesId = hrPersonnel[0].HumanResourceId,
////                DateApply = DateTime.UtcNow.AddDays(-12),
////                CoverLetter = "I am writing to express my strong interest in the position.",
////                EmployerId = jobPostings[0].EmployerId
////            },
////            new()
////            {
////                ApplicantId = applicants[1].ApplicantId,
////                JobPostingId = jobPostings[1].JobPostingId,
////                ResumeId = resumes[1].ResumeId,
////                ApplicationStatus = ApplicationStatus.Pending,
////                HumanResourcesId = hrPersonnel[1].HumanResourceId,
////                DateApply = DateTime.UtcNow.AddDays(-8),
////                CoverLetter = "Excited to apply for the marketing position.",
////                EmployerId = jobPostings[1].EmployerId
////            },
////            new()
////            {
////                ApplicantId = applicants[2].ApplicantId,
////                JobPostingId = jobPostings[2].JobPostingId,
////                ResumeId = resumes[2].ResumeId,
////                ApplicationStatus = ApplicationStatus.Initial,
////                HumanResourcesId = hrPersonnel[2].HumanResourceId,
////                DateApply = DateTime.UtcNow.AddDays(-6),
////                CoverLetter = "With strong finance background, ready to contribute.",
////                EmployerId = jobPostings[2].EmployerId
////            }
////        };

////        await _context.Application.AddRangeAsync(applications);
////        await _context.SaveChangesAsync();

////        // 8️⃣ INTERVIEWS
////        var interviews = new List<Interview>
////        {
////            new() { ApplicationId = applications[1].ApplicationId, ScheduledDate = DateTime.UtcNow.AddDays(3), ScheduledTime = new TimeSpan(10, 0, 0), Mode = "Virtual - Google Meet", HumanResourceId = hrPersonnel[1].HumanResourceId },
////            new() { ApplicationId = applications[2].ApplicationId, ScheduledDate = DateTime.UtcNow.AddDays(-2), ScheduledTime = new TimeSpan(14, 30, 0), Mode = "On-site", HumanResourceId = hrPersonnel[2].HumanResourceId }
////        };

////        await _context.Interview.AddRangeAsync(interviews);
////        await _context.SaveChangesAsync();

////        // 9️⃣ INTERVIEW HISTORY
////        var interviewHistories = new List<InterviewHistory>
////        {
////            new()
////            {
////                ApplicationId = applications[0].ApplicationId,
////                CandidateName = "Juan Pedro Dela Cruz",
////                Stage = "Initial Screening",
////                Status = "Completed",
////                InterviewBy = "John Smith",
////                ScheduledDate = DateTime.UtcNow.AddDays(-10),
////                CreatedAt = DateTime.UtcNow.AddDays(-10)
////            },
////            new()
////            {
////                ApplicationId = applications[0].ApplicationId,
////                CandidateName = "Juan Pedro Dela Cruz",
////                Stage = "Technical Interview",
////                Status = "Scheduled",
////                InterviewBy = "John Smith",
////                ScheduledDate = DateTime.UtcNow.AddDays(2),
////                CreatedAt = DateTime.UtcNow.AddDays(-8)
////            },
////            new()
////            {
////                ApplicationId = applications[1].ApplicationId,
////                CandidateName = "Maria Clara Santos",
////                Stage = "Initial Screening",
////                Status = "Scheduled",
////                InterviewBy = "Sarah Johnson",
////                ScheduledDate = DateTime.UtcNow.AddDays(3),
////                CreatedAt = DateTime.UtcNow.AddDays(-7)
////            },
////            new()
////            {
////                ApplicationId = applications[2].ApplicationId,
////                CandidateName = "Pedro Miguel Reyes",
////                Stage = "Initial Screening",
////                Status = "Completed",
////                InterviewBy = "Michael Brown",
////                ScheduledDate = DateTime.UtcNow.AddDays(-2),
////                CreatedAt = DateTime.UtcNow.AddDays(-5)
////            },
////            new()
////            {
////                ApplicationId = applications[2].ApplicationId,
////                CandidateName = "Pedro Miguel Reyes",
////                Stage = "HR Interview",
////                Status = "Completed",
////                InterviewBy = "Michael Brown",
////                ScheduledDate = DateTime.UtcNow.AddDays(-1),
////                CreatedAt = DateTime.UtcNow.AddDays(-3)
////            }
////        };

////        await _context.InterviewHistory.AddRangeAsync(interviewHistories);
////        await _context.SaveChangesAsync();

////        // 🔟 AUDIT LOGS
////        var auditLogs = new List<AuditLog>
////        {
////            new()
////            {
////                ApplicationId = applications[0].ApplicationId,
////                ApplicantId = applicants[0].ApplicantId,
////                JobPostingId = jobPostings[0].JobPostingId,
////                HumanResourcesId = hrPersonnel[0].HumanResourceId,
////                Action = "Applied",
////                Details = "Applicant Juan Dela Cruz applied for Senior Software Engineer position.",
////                Timestamp = DateTime.UtcNow.AddDays(-11),
////                EmployerId = hrPersonnel[0].EmployerId
////            },
////            new()
////            {
////                ApplicationId = applications[1].ApplicationId,
////                ApplicantId = applicants[1].ApplicantId,
////                JobPostingId = jobPostings[1].JobPostingId,
////                HumanResourcesId = hrPersonnel[1].HumanResourceId,
////                Action = "Interview Scheduled",
////                Details = "Interview scheduled for Maria Santos on Google Meet.",
////                Timestamp = DateTime.UtcNow.AddDays(-7),
////                EmployerId = hrPersonnel[1].EmployerId
////            },
////            new()
////            {
////                ApplicationId = applications[2].ApplicationId,
////                ApplicantId = applicants[2].ApplicantId,
////                JobPostingId = jobPostings[2].JobPostingId,
////                HumanResourcesId = hrPersonnel[2].HumanResourceId,
////                Action = "Application Reviewed",
////                Details = "Finance HR reviewed Pedro Reyes' application.",
////                Timestamp = DateTime.UtcNow.AddDays(-5),
////                EmployerId = hrPersonnel[2].EmployerId
////            }
////        };

////        await _context.AuditLog.AddRangeAsync(auditLogs);
////        await _context.SaveChangesAsync();
////    }
////}





//using HelloJobPH.Server.Data;
//using HelloJobPH.Shared.Enums;
//using HelloJobPH.Shared.Model;
//using Microsoft.EntityFrameworkCore;

//public class Seeder
//{
//    private readonly ApplicationDbContext _context;

//    public Seeder(ApplicationDbContext context)
//    {
//        _context = context;
//    }

//    public async Task SeedAsync()
//    {
//        if (await _context.UserAccount.AnyAsync()) return;

//        var hashedPassword = BCrypt.Net.BCrypt.HashPassword("password123");

//        var users = new List<UserAccount>
//        {
//            new UserAccount { Email = "john.doe@email.com", Password = hashedPassword, Role = "Applicant" },
//            new UserAccount { Email = "jane.smith@email.com", Password = hashedPassword, Role = "Applicant" },
//            new UserAccount { Email = "mike.wilson@email.com", Password = hashedPassword, Role = "Applicant" },
//            new UserAccount { Email = "sarah.jones@email.com", Password = hashedPassword, Role = "Applicant" },
//            new UserAccount {  Email = "david.brown@email.com", Password = hashedPassword, Role = "Applicant" },
//            new UserAccount {  Email = "hr@techcorp.com", Password = hashedPassword, Role = "HR" },
//            new UserAccount { Email = "hr@globalinc.com", Password = hashedPassword, Role = "HR" },
//            new UserAccount {  Email = "hr@innovatesoft.com", Password = hashedPassword, Role = "HR" },
//            new UserAccount {  Email = "hr@marketpro.com", Password = hashedPassword, Role = "HR" },
//            new UserAccount { Email = "hr@healthplus.com", Password = hashedPassword, Role = "HR" },
//            new UserAccount { Email = "admin@techcorp.com", Password = hashedPassword, Role = "Employer" },
//            new UserAccount { Email = "admin@globalinc.com", Password = hashedPassword, Role = "Employer" },
//            new UserAccount {  Email = "admin@innovatesoft.com", Password = hashedPassword, Role = "Employer" },
//            new UserAccount {  Email = "admin@marketpro.com", Password = hashedPassword, Role = "Employer" },
//            new UserAccount { Email = "admin@healthplus.com", Password = hashedPassword, Role = "Employer" }
//        };

//        await _context.UserAccount.AddRangeAsync(users);
//        await _context.SaveChangesAsync();

//        // ===== EMPLOYERS =====
//        var employers = new List<Employer>
//        {
//            new Employer
//            {
//                CompanyName = "TechCorp Solutions",
//                Industry = "Information Technology",
//                Description = "Leading software development company specializing in enterprise solutions",
//                CompanyAddress = "123 Tech Street",
//                City = "Makati",
//                Province = "Metro Manila",
//                ZipCode = "1200",
//                ContactEmail = "contact@techcorp.com",
//                ContactNumber = "02-1234-5678",
//                Website = "https://www.techcorp.com",
//                UserAccountId = 11,
//                Status = "Approved",
//                DateRegistered = DateTime.UtcNow.AddMonths(-6),
//                IsVerified = true,
//                IsActive = true,
//                IsDeleted = 0
//            },
//            new Employer
//            {
//                CompanyName = "Global Inc.",
//                Industry = "Business Process Outsourcing",
//                Description = "International BPO company with offices worldwide",
//                CompanyAddress = "456 Business Ave",
//                City = "Quezon City",
//                Province = "Metro Manila",
//                ZipCode = "1100",
//                ContactEmail = "contact@globalinc.com",
//                ContactNumber = "02-9876-5432",
//                Website = "https://www.globalinc.com",
//                UserAccountId = 12,
//                Status = "Approved",
//                DateRegistered = DateTime.UtcNow.AddMonths(-5),
//                IsVerified = true,
//                IsActive = true,
//                IsDeleted = 0
//            },
//            new Employer
//            {
//                CompanyName = "Innovate Software Inc.",
//                Industry = "Software Development",
//                Description = "Innovative mobile and web application development company",
//                CompanyAddress = "789 Innovation Drive",
//                City = "Taguig",
//                Province = "Metro Manila",
//                ZipCode = "1630",
//                ContactEmail = "contact@innovatesoft.com",
//                ContactNumber = "02-5555-1234",
//                Website = "https://www.innovatesoft.com",
//                UserAccountId = 13,
//                Status = "Approved",
//                DateRegistered = DateTime.UtcNow.AddMonths(-4),
//                IsVerified = true,
//                IsActive = true,
//                IsDeleted = 0
//            },
//            new Employer
//            {
//                CompanyName = "MarketPro Agency",
//                Industry = "Marketing and Advertising",
//                Description = "Full-service digital marketing agency",
//                CompanyAddress = "321 Marketing Plaza",
//                City = "Pasig",
//                Province = "Metro Manila",
//                ZipCode = "1600",
//                ContactEmail = "contact@marketpro.com",
//                ContactNumber = "02-7777-8888",
//                Website = "https://www.marketpro.com",
//                UserAccountId = 14,
//                Status = "Approved",
//                DateRegistered = DateTime.UtcNow.AddMonths(-3),
//                IsVerified = true,
//                IsActive = true,
//                IsDeleted = 0
//            },
//            new Employer
//            {
//                CompanyName = "HealthPlus Medical Center",
//                Industry = "Healthcare",
//                Description = "Modern medical facility providing comprehensive healthcare services",
//                CompanyAddress = "555 Health Boulevard",
//                City = "Manila",
//                Province = "Metro Manila",
//                ZipCode = "1000",
//                ContactEmail = "contact@healthplus.com",
//                ContactNumber = "02-3333-4444",
//                Website = "https://www.healthplus.com",
//                UserAccountId = 15,
//                Status = "Approved",
//                DateRegistered = DateTime.UtcNow.AddMonths(-2),
//                IsVerified = true,
//                IsActive = true,
//                IsDeleted = 0
//            }
//        };

//        await _context.Employer.AddRangeAsync(employers);
//        await _context.SaveChangesAsync();

//        var educationalAttainments = new List<EducationalAttainment>
//        {
//            // John Doe's Education
//            new EducationalAttainment
//            {
//                SchoolName = "University of the Philippines",
//                Degree = "Bachelor of Science in Computer Science",
//                YearStarted = new DateTime(2013, 6, 1),
//                YearEnded = new DateTime(2017, 4, 30),
//                Level = "College",
//                IsGraduated = true,
//                ApplicantId = 1,
//                IsDeleted = 0
//            },
//            new EducationalAttainment
//            {
//                SchoolName = "Manila Science High School",
//                Degree = null,
//                YearStarted = new DateTime(2009, 6, 1),
//                YearEnded = new DateTime(2013, 3, 31),
//                Level = "High School",
//                IsGraduated = true,
//                ApplicantId = 1,
//                IsDeleted = 0
//            },
//            // Jane Smith's Education
//            new EducationalAttainment
//            {
//                SchoolName = "De La Salle University",
//                Degree = "Bachelor of Science in Business Administration",
//                YearStarted = new DateTime(2016, 6, 1),
//                YearEnded = new DateTime(2020, 4, 30),
//                Level = "College",
//                IsGraduated = true,
//                ApplicantId = 2,
//                IsDeleted = 0
//            },
//            // Mike Wilson's Education
//            new EducationalAttainment
//            {
//                SchoolName = "Ateneo de Manila University",
//                Degree = "Bachelor of Arts in Communication",
//                YearStarted = new DateTime(2011, 6, 1),
//                YearEnded = new DateTime(2015, 4, 30),
//                Level = "College",
//                IsGraduated = true,
//                ApplicantId = 3,
//                IsDeleted = 0
//            },
//            new EducationalAttainment
//            {
//                SchoolName = "Ateneo de Manila University",
//                Degree = "Master of Business Administration",
//                YearStarted = new DateTime(2016, 6, 1),
//                YearEnded = new DateTime(2018, 4, 30),
//                Level = "Master's",
//                IsGraduated = true,
//                ApplicantId = 3,
//                IsDeleted = 0
//            },
//            // Sarah Jones's Education
//            new EducationalAttainment
//            {
//                SchoolName = "Polytechnic University of the Philippines",
//                Degree = "Bachelor of Science in Information Technology",
//                YearStarted = new DateTime(2014, 6, 1),
//                YearEnded = new DateTime(2018, 4, 30),
//                Level = "College",
//                IsGraduated = true,
//                ApplicantId = 4,
//                IsDeleted = 0
//            },
//            // David Brown's Education
//            new EducationalAttainment
//            {
//                SchoolName = "Far Eastern University",
//                Degree = "Bachelor of Science in Nursing",
//                YearStarted = new DateTime(2012, 6, 1),
//                YearEnded = new DateTime(2016, 4, 30),
//                Level = "College",
//                IsGraduated = true,
//                ApplicantId = 5,
//                IsDeleted = 0
//            }
//        };

//        await _context.EducationalAttainment.AddRangeAsync(educationalAttainments);
//        await _context.SaveChangesAsync();


//        // ===== WORK EXPERIENCES =====
//        var workExperiences = new List<WorkExperience>
//        {
//            // John Doe's Work Experience
//            new WorkExperience
//            {
//                CompanyName = "Tech Solutions Inc.",
//                PositionTitle = "Junior Software Developer",
//                Department = "Development",
//                StartDate = new DateTime(2017, 6, 1),
//                EndDate = new DateTime(2019, 12, 31),
//                IsPresent = false,
//                Responsibilities = "Developed web applications using React and Node.js, participated in code reviews, and collaborated with team members on various projects.",
//                CompanyAddress = "BGC, Taguig City",
//                ApplicantId = 1,
//                IsDeleted = 0
//            },
//            new WorkExperience
//            {
//                CompanyName = "Digital Innovations Corp.",
//                PositionTitle = "Software Developer",
//                Department = "Engineering",
//                StartDate = new DateTime(2020, 1, 15),
//                EndDate = null,
//                IsPresent = true,
//                Responsibilities = "Lead developer for mobile applications, mentored junior developers, and implemented CI/CD pipelines.",
//                CompanyAddress = "Makati City",
//                ApplicantId = 1,
//                IsDeleted = 0
//            },
//            // Jane Smith's Work Experience
//            new WorkExperience
//            {
//                CompanyName = "Business Consultants Ltd.",
//                PositionTitle = "Business Analyst",
//                Department = "Operations",
//                StartDate = new DateTime(2020, 7, 1),
//                EndDate = null,
//                IsPresent = true,
//                Responsibilities = "Analyzed business processes, created detailed reports, and provided recommendations for operational improvements.",
//                CompanyAddress = "Ortigas, Pasig City",
//                ApplicantId = 2,
//                IsDeleted = 0
//            },
//            // Mike Wilson's Work Experience
//            new WorkExperience
//            {
//                CompanyName = "Creative Marketing Agency",
//                PositionTitle = "Marketing Coordinator",
//                Department = "Marketing",
//                StartDate = new DateTime(2015, 8, 1),
//                EndDate = new DateTime(2018, 6, 30),
//                IsPresent = false,
//                Responsibilities = "Coordinated marketing campaigns, managed social media accounts, and analyzed campaign performance metrics.",
//                CompanyAddress = "Quezon City",
//                ApplicantId = 3,
//                IsDeleted = 0
//            },
//            new WorkExperience
//            {
//                CompanyName = "Global Brands Inc.",
//                PositionTitle = "Senior Marketing Manager",
//                Department = "Marketing",
//                StartDate = new DateTime(2018, 7, 15),
//                EndDate = null,
//                IsPresent = true,
//                Responsibilities = "Managed marketing team, developed brand strategies, and oversaw multi-million peso advertising campaigns.",
//                CompanyAddress = "Makati City",
//                ApplicantId = 3,
//                IsDeleted = 0
//            },
//            // Sarah Jones's Work Experience
//            new WorkExperience
//            {
//                CompanyName = "IT Support Services",
//                PositionTitle = "Technical Support Specialist",
//                Department = "IT Support",
//                StartDate = new DateTime(2018, 6, 1),
//                EndDate = new DateTime(2021, 12, 31),
//                IsPresent = false,
//                Responsibilities = "Provided technical support to clients, troubleshot software and hardware issues, and maintained IT documentation.",
//                CompanyAddress = "Manila",
//                ApplicantId = 4,
//                IsDeleted = 0
//            },
//            // David Brown's Work Experience
//            new WorkExperience
//            {
//                CompanyName = "City General Hospital",
//                PositionTitle = "Staff Nurse",
//                Department = "Emergency Room",
//                StartDate = new DateTime(2016, 7, 1),
//                EndDate = null,
//                IsPresent = true,
//                Responsibilities = "Provided direct patient care, administered medications, and assisted physicians during emergency procedures.",
//                CompanyAddress = "Manila",
//                ApplicantId = 5,
//                IsDeleted = 0
//            }
//        };

//        await _context.WorkExperience.AddRangeAsync(workExperiences);
//        await _context.SaveChangesAsync();
//        // 3️⃣ HUMAN RESOURCES
//        var hrPersonnel = new List<HumanResources>
//        {
//            new()
//            {
//                Firstname = "John",
//                Lastname = "Smith",
//                PhoneNumber = "+63 917 123 4567",

//                JobTitle = "Senior HR Manager",
//                UserAccountId = users[4].UserAccountId,
//                EmployerId = employers[0].EmployerId
//            },
//            new()
//            {
//                Firstname = "Sarah",
//                Lastname = "Johnson",
//                PhoneNumber = "+63 917 234 5678",

//                JobTitle = "HR Specialist",
//                UserAccountId = users[5].UserAccountId,
//                EmployerId = employers[1].EmployerId
//            },
//            new()
//            {
//                Firstname = "Michael",
//                Lastname = "Brown",
//                PhoneNumber = "+63 917 345 6789",

//                JobTitle = "Recruitment Manager",
//                UserAccountId = users[6].UserAccountId,
//                EmployerId = employers[2].EmployerId
//            }
//        };

//        await _context.HumanResource.AddRangeAsync(hrPersonnel);
//        await _context.SaveChangesAsync();

//        var humanResources = new List<HumanResources>
//        {
//            new HumanResources
//            {
//                Firstname = "Maria",
//                Lastname = "Santos",
//                PhoneNumber = "0917-123-4567",
//                JobTitle = "HR Manager",
//                UserAccountId = 6,
//                EmployerId = 1,
//                IsDeleted = 0
//            },
//            new HumanResources
//            {
//                Firstname = "Carlos",
//                Lastname = "Reyes",
//                PhoneNumber = "0918-234-5678",
//                JobTitle = "Recruitment Specialist",
//                UserAccountId = 7,
//                EmployerId = 2,
//                IsDeleted = 0
//            },
//            new HumanResources
//            {
//                Firstname = "Ana",
//                Lastname = "Cruz",
//                PhoneNumber = "0919-345-6789",
//                JobTitle = "Talent Acquisition Lead",
//                UserAccountId = 8,
//                EmployerId = 3,
//                IsDeleted = 0
//            },
//            new HumanResources
//            {
//                Firstname = "Roberto",
//                Lastname = "Garcia",
//                PhoneNumber = "0920-456-7890",
//                JobTitle = "HR Director",
//                UserAccountId = 9,
//                EmployerId = 4,
//                IsDeleted = 0
//            },
//            new HumanResources
//            {
//                Firstname = "Elena",
//                Lastname = "Mendoza",
//                PhoneNumber = "0921-567-8901",
//                JobTitle = "HR Coordinator",
//                UserAccountId = 10,
//                EmployerId = 5,
//                IsDeleted = 0
//            }
//        };

//        // ===== APPLICANTS =====
//        var applicants = new List<Applicant>
//        {
//            new Applicant
//            {
//                Firstname = "John",
//                Middlename = "Michael",
//                Surname = "Doe",
//                Address = "123 Main Street, Quezon City",
//                Phone = "0917-111-2222",
//                Birthday = new DateTime(1995, 5, 15),
//                UserAccountId = 1
//            },
//            new Applicant
//            {
//                Firstname = "Jane",
//                Middlename = "Marie",
//                Surname = "Smith",
//                Address = "456 Oak Avenue, Makati City",
//                Phone = "0918-222-3333",
//                Birthday = new DateTime(1998, 8, 20),
//                UserAccountId = 2
//            },
//            new Applicant
//            {
//                Firstname = "Mike",
//                Middlename = "James",
//                Surname = "Wilson",
//                Address = "789 Pine Road, Pasig City",
//                Phone = "0919-333-4444",
//                Birthday = new DateTime(1993, 3, 10),
//                UserAccountId = 3
//            },
//            new Applicant
//            {
//                Firstname = "Sarah",
//                Middlename = "Lee",
//                Surname = "Jones",
//                Address = "321 Elm Street, Taguig City",
//                Phone = "0920-444-5555",
//                Birthday = new DateTime(1996, 11, 25),
//                UserAccountId = 4
//            },
//            new Applicant
//            {
//                Firstname = "David",
//                Middlename = "Paul",
//                Surname = "Brown",
//                Address = "654 Maple Drive, Manila",
//                Phone = "0921-555-6666",
//                Birthday = new DateTime(1994, 7, 30),
//                UserAccountId = 5
//            }
//        };

//        // 5️⃣ JOB POSTINGS
//        var jobPostings = new List<JobPosting>
//        {
//            new JobPosting
//            {
//                Title = "Senior Software Developer",
//                Description = "We are looking for an experienced software developer to join our team. Must have 3+ years of experience in web development.",
//                Location = "Makati City",
//                EmploymentType = EmploymentType.FullTime,
//                SalaryFrom = 60000,
//                SalaryTo = 90000,
//                JobCategory = JobCategory.InformationAndCommunicationTechnology,
//                JobRequirements = "Bachelor's degree in Computer Science, 3+ years experience in React/Node.js, strong problem-solving skills",
//                HumanResourceId = 1,
//                EmployerId = 1,
//                PostedDate = DateTime.UtcNow.AddDays(-30),
//                IsDeleted = 0,
//                IsActive = 1
//            },
//            new JobPosting
//            {
//                Title = "Customer Service Representative",
//                Description = "Join our growing BPO team as a customer service representative. Training provided.",
//                Location = "Quezon City",
//                EmploymentType = EmploymentType.FullTime,
//                SalaryFrom = 25000,
//                SalaryTo = 35000,
//                JobCategory = JobCategory.CustomerServiceAndSupport,
//                JobRequirements = "College graduate, excellent communication skills, willing to work on shifting schedules",
//                HumanResourceId = 2,
//                EmployerId = 2,
//                PostedDate = DateTime.UtcNow.AddDays(-25),
//                IsDeleted = 0,
//                IsActive = 1
//            },
//            new JobPosting
//            {
//                Title = "Mobile App Developer",
//                Description = "Seeking talented mobile app developer with experience in iOS and Android development.",
//                Location = "Taguig City",
//                EmploymentType = EmploymentType.FullTime,
//                SalaryFrom = 50000,
//                SalaryTo = 80000,
//                JobCategory = JobCategory.InformationAndCommunicationTechnology,
//                JobRequirements = "Experience with Flutter or React Native, portfolio of published apps, 2+ years experience",
//                HumanResourceId = 3,
//                EmployerId = 3,
//                PostedDate = DateTime.UtcNow.AddDays(-20),
//                IsDeleted = 0,
//                IsActive = 1
//            },
//            new JobPosting
//            {
//                Title = "Digital Marketing Specialist",
//                Description = "Dynamic marketing agency seeks creative digital marketing specialist to manage client campaigns.",
//                Location = "Pasig City",
//                EmploymentType = EmploymentType.FullTime,
//                SalaryFrom = 35000,
//                SalaryTo = 55000,
//                JobCategory = JobCategory.MarketingAndCommunications,
//                JobRequirements = "Bachelor's degree in Marketing or related field, experience with SEO/SEM, social media management skills",
//                HumanResourceId = 4,
//                EmployerId = 4,
//                PostedDate = DateTime.UtcNow.AddDays(-15),
//                IsDeleted = 0,
//                IsActive = 1
//            },
//            new JobPosting
//            {
//                Title = "Registered Nurse",
//                Description = "Modern medical center seeks compassionate registered nurses for various departments.",
//                Location = "Manila",
//                EmploymentType = EmploymentType.FullTime,
//                SalaryFrom = 30000,
//                SalaryTo = 45000,
//                JobCategory = JobCategory.HealthcareAndMedical,
//                JobRequirements = "Valid PRC license, BSN graduate, at least 1 year hospital experience preferred",
//                HumanResourceId = 5,
//                EmployerId = 5,
//                PostedDate = DateTime.UtcNow.AddDays(-10),
//                IsDeleted = 0,
//                IsActive = 1
//            }
//        };

//        // ===== RESUMES =====
//        var resumes = new List<Resume>
//        {
//            new Resume
//            {
//                ResumeFileName = "JohnDoe_Resume.pdf",
//                ResumeFileData = new byte[] { 0x25, 0x50, 0x44, 0x46 }, // Dummy PDF bytes
//                ApplicantId = 1,
//                IsDeleted = 0
//            },
//            new Resume
//            {
//                ResumeFileName = "JaneSmith_Resume.pdf",
//                ResumeFileData = new byte[] { 0x25, 0x50, 0x44, 0x46 },
//                ApplicantId = 2,
//                IsDeleted = 0
//            },
//            new Resume
//            {
//                ResumeFileName = "MikeWilson_Resume.pdf",
//                ResumeFileData = new byte[] { 0x25, 0x50, 0x44, 0x46 },
//                ApplicantId = 3,
//                IsDeleted = 0
//            },
//            new Resume
//            {
//                ResumeFileName = "SarahJones_Resume.pdf",
//                ResumeFileData = new byte[] { 0x25, 0x50, 0x44, 0x46 },
//                ApplicantId = 4,
//                IsDeleted = 0
//            },
//            new Resume
//            {
//                ResumeFileName = "DavidBrown_Resume.pdf",
//                ResumeFileData = new byte[] { 0x25, 0x50, 0x44, 0x46 },
//                ApplicantId = 5,
//                IsDeleted = 0
//            }
//        };

//        // ===== APPLICATIONS =====
//        var applications = new List<Application>
//        {
//            new Application
//            {
//                ResumeId = 1,
//                DateApply = DateTime.UtcNow.AddDays(-28),
//                ApplicationStatus = ApplicationStatus.Viewed,
//                JobPostingId = 1,
//                ApplicantId = 1,
//                EmployerId = 1,
//                HumanResourcesId = 1,
//                CoverLetter = "I am excited to apply for the Senior Software Developer position. With over 3 years of experience in web development and a proven track record of delivering high-quality solutions, I am confident I can contribute to your team.",
//                IsDeleted = 0,
//                MarkAsCompleted = 0
//            },
//            new Application
//            {
//                ResumeId = 2,
//                DateApply = DateTime.UtcNow.AddDays(-23),
//                ApplicationStatus = ApplicationStatus.Pending,
//                JobPostingId = 2,
//                ApplicantId = 2,
//                EmployerId = 2,
//                HumanResourcesId = 2,
//                CoverLetter = "I am writing to express my strong interest in the Customer Service Representative position. My excellent communication skills and customer-focused approach make me an ideal candidate.",
//                IsDeleted = 0,
//                MarkAsCompleted = 0
//            },
//            new Application
//            {
//                ResumeId = 3,
//                DateApply = DateTime.UtcNow.AddDays(-18),
//                ApplicationStatus = ApplicationStatus.Initial,
//                JobPostingId = 4,
//                ApplicantId = 3,
//                EmployerId = 4,
//                HumanResourcesId = 4,
//                CoverLetter = "With my extensive marketing experience and proven success in managing campaigns, I am confident I can bring value to your digital marketing team.",
//                IsDeleted = 0,
//                MarkAsCompleted = 0
//            },
//            new Application
//            {
//                ResumeId = 4,
//                DateApply = DateTime.UtcNow.AddDays(-17),
//                ApplicationStatus = ApplicationStatus.Pending,
//                JobPostingId = 3,
//                ApplicantId = 4,
//                EmployerId = 3,
//                HumanResourcesId = 3,
//                CoverLetter = "I am very interested in the Mobile App Developer position. My technical skills and passion for creating user-friendly applications align well with your requirements.",
//                IsDeleted = 0,
//                MarkAsCompleted = 0
//            },
//            new Application
//            {
//                ResumeId = 5,
//                DateApply = DateTime.UtcNow.AddDays(-8),
//                ApplicationStatus = ApplicationStatus.Accepted,
//                JobPostingId = 5,
//                ApplicantId = 5,
//                EmployerId = 5,
//                HumanResourcesId = 5,
//                CoverLetter = "As a dedicated registered nurse with emergency room experience, I am eager to join your medical center and provide excellent patient care.",
//                IsDeleted = 0,
//                MarkAsCompleted = 0
//            }
//        };
//        await _context.JobPosting.AddRangeAsync(jobPostings);
//        await _context.SaveChangesAsync();



//        var interviewers = new List<Interviewer>
//        {
//            new Interviewer {  Name = "Dr. Patricia Lopez" },
//            new Interviewer {  Name = "Mr. Ramon Diaz" },
//            new Interviewer { Name = "Ms. Linda Tan" },
//            new Interviewer { Name = "Mr. Jose Fernandez" },
//            new Interviewer {  Name = "Dr. Maria Gonzales" }
//        };

//        await _context.Interviewer.AddRangeAsync(interviewers);
//        await _context.SaveChangesAsync();

//        // ===== INTERVIEWS =====
//        var interviews = new List<Interview>
//        {
//            new Interview
//            {
//                ScheduledDate = DateTime.UtcNow.AddDays(5),
//                ScheduledTime = new TimeSpan(10, 0, 0),
//                Mode = "Virtual",
//                ApplicationId = 3,
//                HumanResourceId = 4,
//                InterviewerId = 4
//            },
//            new Interview
//            {
//                ScheduledDate = DateTime.UtcNow.AddDays(3),
//                ScheduledTime = new TimeSpan(14, 0, 0),
//                Mode = "On-site",
//                ApplicationId = 5,
//                HumanResourceId = 5,
//                InterviewerId = 5
//            }
//        };
//        await _context.Interview.AddRangeAsync(interviews);
//        await _context.SaveChangesAsync();

//        // 9️⃣ INTERVIEW HISTORY
//        var interviewHistories = new List<InterviewHistory>
//        {
//            new()
//            {
//                ApplicationId = applications[0].ApplicationId,
//                CandidateName = "Juan Pedro Dela Cruz",
//                Stage = "Initial Screening",
//                Status = "Completed",
//                InterviewerId = 2,
//                ScheduledDate = DateTime.UtcNow.AddDays(-10),
//                CreatedAt = DateTime.UtcNow.AddDays(-10)
//            },
//            new()
//            {
//                ApplicationId = applications[0].ApplicationId,
//                CandidateName = "Juan Pedro Dela Cruz",
//                Stage = "Technical Interview",
//                Status = "Scheduled",
//                InterviewerId = 2,
//                ScheduledDate = DateTime.UtcNow.AddDays(2),
//                CreatedAt = DateTime.UtcNow.AddDays(-8)
//            },
//            new()
//            {
//                ApplicationId = applications[1].ApplicationId,
//                CandidateName = "Maria Clara Santos",
//                Stage = "Initial Screening",
//                Status = "Scheduled",
//                InterviewerId = 3,
//                ScheduledDate = DateTime.UtcNow.AddDays(3),
//                CreatedAt = DateTime.UtcNow.AddDays(-7)
//            },
//            new()
//            {
//                ApplicationId = applications[2].ApplicationId,
//                CandidateName = "Pedro Miguel Reyes",
//                Stage = "Initial Screening",
//                Status = "Completed",
//                InterviewerId = 4,
//                ScheduledDate = DateTime.UtcNow.AddDays(-2),
//                CreatedAt = DateTime.UtcNow.AddDays(-5)
//            },
//            new()
//            {
//                ApplicationId = applications[2].ApplicationId,
//                CandidateName = "Pedro Miguel Reyes",
//                Stage = "HR Interview",
//                Status = "Completed",
//                InterviewerId = 5,
//                ScheduledDate = DateTime.UtcNow.AddDays(-1),
//                CreatedAt = DateTime.UtcNow.AddDays(-3)
//            }
//        };

//        await _context.InterviewHistory.AddRangeAsync(interviewHistories);
//        await _context.SaveChangesAsync();

//        // 🔟 AUDIT LOGS
//        // ===== AUDIT LOGS =====
//        var auditLogs = new List<AuditLog>
//        {
//            new AuditLog
//            {
//                Action = "Applied",
//                Details = "Application submitted successfully for Senior Software Developer position",
//                Timestamp = DateTime.UtcNow.AddDays(-28),
//                ApplicationId = 1,
//                ApplicantId = 1,
//                JobPostingId = 1
//            },
//            new AuditLog
//            {
//                Action = "Shortlisted",
//                Details = "Applicant shortlisted for interview",
//                Timestamp = DateTime.UtcNow.AddDays(-20),
//                ApplicationId = 2,
//                HumanResourcesId = 2,
//                JobPostingId = 2
//            },
//            new AuditLog
//            {
//                Action = "Interview Scheduled",
//                Details = "Interview scheduled for Digital Marketing Specialist position",
//                Timestamp = DateTime.UtcNow.AddDays(-15),
//                ApplicationId = 3,
//                HumanResourcesId = 4,
//                ApplicantId = 3,
//                JobPostingId = 4
//            },
//            new AuditLog
//            {
//                Action = "Applied",
//                Details = "Application submitted for Mobile App Developer position",
//                Timestamp = DateTime.UtcNow.AddDays(-17),
//                ApplicationId = 4,
//                ApplicantId = 4,
//                JobPostingId = 3
//            },
//            new AuditLog
//            {
//                Action = "Accepted",
//                Details = "Application accepted - offer extended",
//                Timestamp = DateTime.UtcNow.AddDays(-2),
//                ApplicationId = 5,
//                HumanResourcesId = 5,
//                ApplicantId = 5,
//                JobPostingId = 5
//            }
//        };

//        await _context.AuditLog.AddRangeAsync(auditLogs);
//        await _context.SaveChangesAsync();
//    }
//}