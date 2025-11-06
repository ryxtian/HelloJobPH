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

        // User Accounts
        var UserAccount = new List<UserAccount>
        {
            new UserAccount { UserAccountId = 1, Email = "admin@jobportal.com", Password = hashedPassword, Role = "Admin" },
            new UserAccount { UserAccountId = 2, Email = "hr@techcorp.com", Password = hashedPassword, Role = "Employer" },
            new UserAccount { UserAccountId = 3, Email = "hr@innovate.com", Password = hashedPassword, Role = "Employer" },
            new UserAccount { UserAccountId = 4, Email = "hr@globalfinance.com", Password = hashedPassword, Role = "Employer" },
            new UserAccount { UserAccountId = 5, Email = "john.hr@techcorp.com", Password = hashedPassword, Role = "HR" },
            new UserAccount { UserAccountId = 6, Email = "sarah.hr@innovate.com", Password = hashedPassword, Role = "HR" },
            new UserAccount { UserAccountId = 7, Email = "mike.hr@globalfinance.com", Password = hashedPassword, Role = "HR" },
            new UserAccount { UserAccountId = 8, Email = "juan.delacruz@email.com", Password = hashedPassword, Role = "Applicant" },
            new UserAccount { UserAccountId = 9, Email = "maria.santos@email.com", Password = hashedPassword, Role = "Applicant" },
            new UserAccount { UserAccountId = 10, Email = "pedro.reyes@email.com", Password = hashedPassword, Role = "Applicant" },
            new UserAccount { UserAccountId = 11, Email = "ana.garcia@email.com", Password = hashedPassword, Role = "Applicant" },
            new UserAccount { UserAccountId = 12, Email = "carlos.mendoza@email.com", Password = hashedPassword, Role = "Applicant" }
        };
        await _context.UserAccount.AddRangeAsync(UserAccount);

        // Employers
        var employers = new List<Employers>
        {
            new Employers
            {
                EmployerId = 1,
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
                UserAccountId = 2,
                DateRegistered = DateTime.UtcNow.AddMonths(-6),
                IsVerified = true,
                IsActive = true,
                Status = "Active"
            },
            new Employers
            {
                EmployerId = 2,
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
                UserAccountId = 3,
                DateRegistered = DateTime.UtcNow.AddMonths(-4),
                IsVerified = true,
                IsActive = true,
                Status = "Active"
            },
            new Employers
            {
                EmployerId = 3,
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
                UserAccountId = 4,
                DateRegistered = DateTime.UtcNow.AddMonths(-8),
                IsVerified = true,
                IsActive = true,
                Status = "Active"
            }
        };
        await _context.Employers.AddRangeAsync(employers);

        // Human Resources
        var hrPersonnel = new List<HumanResources>
        {
            new HumanResources
            {
                HumanResourceId = 1,
                Firstname = "John",
                Lastname = "Smith",
                PhoneNumber = "+63 917 123 4567",
                ProfilePhotoUrl = "/images/hr/john-smith.jpg",
                JobTitle = "Senior HR Manager",
                UserAccountId = 5,
                EmployerId = 1
            },
            new HumanResources
            {
                HumanResourceId = 2,
                Firstname = "Sarah",
                Lastname = "Johnson",
                PhoneNumber = "+63 917 234 5678",
                ProfilePhotoUrl = "/images/hr/sarah-johnson.jpg",
                JobTitle = "HR Specialist",
                UserAccountId = 6,
                EmployerId = 2
            },
            new HumanResources
            {
                HumanResourceId = 3,
                Firstname = "Michael",
                Lastname = "Brown",
                PhoneNumber = "+63 917 345 6789",
                ProfilePhotoUrl = "/images/hr/michael-brown.jpg",
                JobTitle = "Recruitment Manager",
                UserAccountId = 7,
                EmployerId = 3
            }
        };
        await _context.HumanResource.AddRangeAsync(hrPersonnel);

        // Applicants
        var applicants = new List<Applicant>
        {
            new Applicant
            {
                ApplicantId = 1,
                Firstname = "Juan",
                Middlename = "Pedro",
                Surname = "Dela Cruz",
                Address = "123 Rizal Street, Brgy. San Antonio",
                Phone = "+63 917 111 2222",
                Birthday = new DateTime(1995, 5, 15),
                UserAccountId = 8,
                HumanResourceId = null
            },
            new Applicant
            {
                ApplicantId = 2,
                Firstname = "Maria",
                Middlename = "Clara",
                Surname = "Santos",
                Address = "456 Bonifacio Avenue, Brgy. Poblacion",
                Phone = "+63 917 222 3333",
                Birthday = new DateTime(1998, 8, 20),
                UserAccountId = 9,
                HumanResourceId = null
            },
            new Applicant
            {
                ApplicantId = 3,
                Firstname = "Pedro",
                Middlename = "Miguel",
                Surname = "Reyes",
                Address = "789 Aguinaldo Street, Brgy. Centro",
                Phone = "+63 917 333 4444",
                Birthday = new DateTime(1992, 3, 10),
                UserAccountId = 10,
                HumanResourceId = null
            },
            new Applicant
            {
                ApplicantId = 4,
                Firstname = "Ana",
                Middlename = "Luisa",
                Surname = "Garcia",
                Address = "321 Mabini Road, Brgy. Santo Niño",
                Phone = "+63 917 444 5555",
                Birthday = new DateTime(1996, 11, 25),
                UserAccountId = 11,
                HumanResourceId = null
            },
            new Applicant
            {
                ApplicantId = 5,
                Firstname = "Carlos",
                Middlename = "Antonio",
                Surname = "Mendoza",
                Address = "654 Luna Street, Brgy. San Jose",
                Phone = "+63 917 555 6666",
                Birthday = new DateTime(1994, 7, 8),
                UserAccountId = 12,
                HumanResourceId = null
            }
        };
        await _context.Applicant.AddRangeAsync(applicants);

        // Job Postings
        var jobPostings = new List<JobPosting>
        {
            new JobPosting
            {
                JobPostingId = 1,
                Title = "Senior Software Engineer",
                Description = "We are looking for an experienced Senior Software Engineer to join our development team. The ideal candidate will have strong experience in .NET Core, C#, and cloud technologies.",
                Location = "Pasig City, Metro Manila",
                EmploymentType = EmploymentType.FullTime,
                SalaryFrom = 80000,
                SalaryTo = 120000,
                JobCategory = JobCategory.InformationAndCommunicationTechnology,
                JobRequirements = "Bachelor's degree in Computer Science, 5+ years experience in software development, Strong knowledge of C# and .NET Core, Experience with Azure or AWS",
                HumanResourceId = 1,
                EmployerId = 1,
                PostedDate = DateTime.UtcNow.AddDays(-15),
                IsActive = 1
            },
            new JobPosting
            {
                JobPostingId = 2,
                Title = "Digital Marketing Specialist",
                Description = "Join our creative team as a Digital Marketing Specialist. You'll be responsible for developing and executing digital marketing campaigns across various platforms.",
                Location = "Taguig City, Metro Manila",
                EmploymentType = EmploymentType.FullTime,
                SalaryFrom = 35000,
                SalaryTo = 50000,
                JobCategory = JobCategory.MarketingAndCommunications,
                JobRequirements = "Bachelor's degree in Marketing or related field, 2+ years experience in digital marketing, Knowledge of SEO, SEM, and social media marketing, Experience with Google Analytics",
                HumanResourceId = 2,
                EmployerId = 2,
                PostedDate = DateTime.UtcNow.AddDays(-10),
                IsActive = 1
            },
            new JobPosting
            {
                JobPostingId = 3,
                Title = "Financial Analyst",
                Description = "We're seeking a detail-oriented Financial Analyst to support our finance team in budgeting, forecasting, and financial reporting.",
                Location = "Makati City, Metro Manila",
                EmploymentType = EmploymentType.FullTime,
                SalaryFrom = 45000,
                SalaryTo = 65000,
                JobCategory = JobCategory.BankingAndFinancialServices,
                JobRequirements = "Bachelor's degree in Finance, Accounting, or Economics, 3+ years experience in financial analysis, Strong Excel skills, CPA or CFA certification preferred",
                HumanResourceId = 3,
                EmployerId = 3,
                PostedDate = DateTime.UtcNow.AddDays(-7),
                IsActive = 1
            },
            new JobPosting
            {
                JobPostingId = 4,
                Title = "Junior Web Developer",
                Description = "Entry-level position for a passionate web developer to grow with our team. Training will be provided for the right candidate.",
                Location = "Pasig City, Metro Manila",
                EmploymentType = EmploymentType.FullTime,
                SalaryFrom = 25000,
                SalaryTo = 35000,
                JobCategory = JobCategory.InformationAndCommunicationTechnology,
                JobRequirements = "Bachelor's degree in Computer Science or related field, Knowledge of HTML, CSS, JavaScript, Familiarity with React or Vue.js is a plus, Fresh graduates welcome",
                HumanResourceId = 1,
                EmployerId = 1,
                PostedDate = DateTime.UtcNow.AddDays(-5),
                IsActive = 1
            },
            new JobPosting
            {
                JobPostingId = 5,
                Title = "Content Writer",
                Description = "Creative content writer needed to produce engaging content for our clients across various industries.",
                Location = "Taguig City, Metro Manila (Hybrid)",
                EmploymentType = EmploymentType.PartTime,
                SalaryFrom = 20000,
                SalaryTo = 30000,
                JobCategory = JobCategory.MarketingAndCommunications,
                JobRequirements = "Bachelor's degree in Communications, Journalism, or related field, 1-2 years writing experience, Excellent English writing skills, Portfolio required",
                HumanResourceId = 2,
                EmployerId = 2,
                PostedDate = DateTime.UtcNow.AddDays(-3),
                IsActive = 1
            }
        };
        await _context.JobPosting.AddRangeAsync(jobPostings);

        // Educational Attainments
        var educations = new List<EducationalAttainment>
        {
            new EducationalAttainment
            {
                EducationId = 1,
                SchoolName = "University of the Philippines",
                Degree = "Bachelor of Science in Computer Science",
                Level = "College",
                YearStarted = new DateTime(2013, 6, 1),
                YearEnded = new DateTime(2017, 4, 1),
                IsGraduated = true,
                ApplicantId = 1
            },
            new EducationalAttainment
            {
                EducationId = 2,
                SchoolName = "Ateneo de Manila University",
                Degree = "Bachelor of Arts in Communication",
                Level = "College",
                YearStarted = new DateTime(2016, 6, 1),
                YearEnded = new DateTime(2020, 4, 1),
                IsGraduated = true,
                ApplicantId = 2
            },
            new EducationalAttainment
            {
                EducationId = 3,
                SchoolName = "De La Salle University",
                Degree = "Bachelor of Science in Accountancy",
                Level = "College",
                YearStarted = new DateTime(2010, 6, 1),
                YearEnded = new DateTime(2014, 4, 1),
                IsGraduated = true,
                ApplicantId = 3
            },
            new EducationalAttainment
            {
                EducationId = 4,
                SchoolName = "Asian Institute of Management",
                Degree = "Master in Business Administration",
                Level = "Master's",
                YearStarted = new DateTime(2015, 6, 1),
                YearEnded = new DateTime(2017, 4, 1),
                IsGraduated = true,
                ApplicantId = 3
            },
            new EducationalAttainment
            {
                EducationId = 5,
                SchoolName = "Polytechnic University of the Philippines",
                Degree = "Bachelor of Science in Marketing Management",
                Level = "College",
                YearStarted = new DateTime(2014, 6, 1),
                YearEnded = new DateTime(2018, 4, 1),
                IsGraduated = true,
                ApplicantId = 4
            },
            new EducationalAttainment
            {
                EducationId = 6,
                SchoolName = "University of Santo Tomas",
                Degree = "Bachelor of Science in Information Technology",
                Level = "College",
                YearStarted = new DateTime(2012, 6, 1),
                YearEnded = new DateTime(2016, 4, 1),
                IsGraduated = true,
                ApplicantId = 5
            }
        };
        await _context.EducationalAttainment.AddRangeAsync(educations);

        // Work Experiences
        var workExperiences = new List<WorkExperience>
        {
            new WorkExperience
            {
                WorkExperienceId = 1,
                CompanyName = "Accenture Philippines",
                PositionTitle = "Software Developer",
                Department = "Technology Solutions",
                StartDate = new DateTime(2017, 7, 1),
                EndDate = new DateTime(2020, 12, 31),
                IsPresent = false,
                Responsibilities = "Developed web applications using .NET Core and Angular. Participated in agile development processes. Collaborated with cross-functional teams.",
                CompanyAddress = "Eastwood City, Quezon City",
                ApplicantId = 1
            },
            new WorkExperience
            {
                WorkExperienceId = 2,
                CompanyName = "Globe Telecom",
                PositionTitle = "Senior Developer",
                Department = "Digital Products",
                StartDate = new DateTime(2021, 1, 15),
                EndDate = null,
                IsPresent = true,
                Responsibilities = "Lead development of mobile applications. Mentor junior developers. Design system architecture for new projects.",
                CompanyAddress = "BGC, Taguig City",
                ApplicantId = 1
            },
            new WorkExperience
            {
                WorkExperienceId = 3,
                CompanyName = "TBWA Santiago Mangada Puno",
                PositionTitle = "Junior Content Creator",
                Department = "Creative Services",
                StartDate = new DateTime(2020, 8, 1),
                EndDate = new DateTime(2022, 6, 30),
                IsPresent = false,
                Responsibilities = "Created social media content for various clients. Managed content calendars. Collaborated with design team on campaign materials.",
                CompanyAddress = "Makati City",
                ApplicantId = 2
            },
            new WorkExperience
            {
                WorkExperienceId = 4,
                CompanyName = "DDB Philippines",
                PositionTitle = "Digital Marketing Associate",
                Department = "Digital Marketing",
                StartDate = new DateTime(2022, 7, 15),
                EndDate = null,
                IsPresent = true,
                Responsibilities = "Execute digital marketing campaigns. Analyze campaign performance metrics. Manage client social media accounts.",
                CompanyAddress = "Bonifacio Global City",
                ApplicantId = 2
            },
            new WorkExperience
            {
                WorkExperienceId = 5,
                CompanyName = "BDO Unibank",
                PositionTitle = "Junior Financial Analyst",
                Department = "Corporate Finance",
                StartDate = new DateTime(2014, 8, 1),
                EndDate = new DateTime(2018, 10, 31),
                IsPresent = false,
                Responsibilities = "Prepared financial reports and analysis. Assisted in budgeting and forecasting. Conducted market research and competitor analysis.",
                CompanyAddress = "Makati City",
                ApplicantId = 3
            },
            new WorkExperience
            {
                WorkExperienceId = 6,
                CompanyName = "SGV & Co.",
                PositionTitle = "Senior Financial Consultant",
                Department = "Advisory Services",
                StartDate = new DateTime(2018, 11, 15),
                EndDate = null,
                IsPresent = true,
                Responsibilities = "Provide financial advisory services to clients. Conduct due diligence for M&A transactions. Lead project teams on consulting engagements.",
                CompanyAddress = "Mandaluyong City",
                ApplicantId = 3
            },
            new WorkExperience
            {
                WorkExperienceId = 7,
                CompanyName = "Unilever Philippines",
                PositionTitle = "Marketing Assistant",
                Department = "Brand Marketing",
                StartDate = new DateTime(2018, 7, 1),
                EndDate = new DateTime(2021, 8, 31),
                IsPresent = false,
                Responsibilities = "Supported brand managers in campaign execution. Coordinated with agencies and suppliers. Monitored brand performance metrics.",
                CompanyAddress = "Bonifacio Global City",
                ApplicantId = 4
            },
            new WorkExperience
            {
                WorkExperienceId = 8,
                CompanyName = "Smart Communications",
                PositionTitle = "Web Developer",
                Department = "IT Development",
                StartDate = new DateTime(2016, 8, 1),
                EndDate = new DateTime(2019, 12, 31),
                IsPresent = false,
                Responsibilities = "Developed and maintained company websites. Implemented responsive web designs. Troubleshot technical issues.",
                CompanyAddress = "Makati City",
                ApplicantId = 5
            }
        };
        await _context.WorkExperience.AddRangeAsync(workExperiences);

        // Resumes
        var resumes = new List<Resume>
        {
            new Resume
            {
                ResumeId = 1,
                ResumeUrl = "/uploads/resumes/juan-delacruz-resume.pdf",
                ApplicantId = 1,
                IsActive = 1
            },
            new Resume
            {
                ResumeId = 2,
                ResumeUrl = "/uploads/resumes/maria-santos-resume.pdf",
                ApplicantId = 2,
                IsActive = 1
            },
            new Resume
            {
                ResumeId = 3,
                ResumeUrl = "/uploads/resumes/pedro-reyes-resume.pdf",
                ApplicantId = 3,
                IsActive = 1
            },
            new Resume
            {
                ResumeId = 4,
                ResumeUrl = "/uploads/resumes/ana-garcia-resume.pdf",
                ApplicantId = 4,
                IsActive = 1
            },
            new Resume
            {
                ResumeId = 5,
                ResumeUrl = "/uploads/resumes/carlos-mendoza-resume.pdf",
                ApplicantId = 5,
                IsActive = 1
            }
        };
        await _context.Resume.AddRangeAsync(resumes);

        // Applications
        var applications = new List<Application>
        {
            new Application
            {
                ApplicationId = 1,
                ApplicantId = 1,
                JobPostId = 1,
                ResumeId = 1,
                DateApply = DateTime.UtcNow.AddDays(-12),
                ApplicationStatus = ApplicationStatus.Pending,
                CoverLetter = "I am writing to express my strong interest in the Senior Software Engineer position. With over 5 years of experience in software development and a proven track record of delivering high-quality solutions, I am confident in my ability to contribute to your team.",
                HumanResourceId = 1
            },
            new Application
            {
                ApplicationId = 2,
                ApplicantId = 2,
                JobPostId = 2,
                ResumeId = 2,
                DateApply = DateTime.UtcNow.AddDays(-8),
                ApplicationStatus = ApplicationStatus.Pending,
                CoverLetter = "I am excited to apply for the Digital Marketing Specialist position. My experience in content creation and social media management aligns perfectly with your requirements.",
                HumanResourceId = 2
            },
            new Application
            {
                ApplicationId = 3,
                ApplicantId = 3,
                JobPostId = 3,
                ResumeId = 3,
                DateApply = DateTime.UtcNow.AddDays(-6),
                ApplicationStatus = ApplicationStatus.Initial,
                CoverLetter = "With my extensive background in financial analysis and consulting, I am well-prepared to excel as a Financial Analyst at your organization.",
                HumanResourceId = 3
            },
            new Application
            {
                ApplicationId = 4,
                ApplicantId = 4,
                JobPostId = 5,
                ResumeId = 4,
                DateApply = DateTime.UtcNow.AddDays(-2),
                ApplicationStatus = ApplicationStatus.Pending,
                CoverLetter = "I am passionate about creating compelling content that engages audiences. I would love to bring my creativity and writing skills to your team.",
                HumanResourceId = 2
            },
            new Application
            {
                ApplicationId = 5,
                ApplicantId = 5,
                JobPostId = 4,
                ResumeId = 5,
                DateApply = DateTime.UtcNow.AddDays(-4),
                ApplicationStatus = ApplicationStatus.Initial,
                CoverLetter = "As a web developer with experience in modern frameworks, I am eager to join your development team and contribute to exciting projects.",
                HumanResourceId = 1
            }
        };
        await _context.Application.AddRangeAsync(applications);

        // Interviews
        var interviews = new List<Interview>
        {
            new Interview
            {
                InterviewId = 1,
                ApplicationId = 2,
                ScheduledDate = DateTime.UtcNow.AddDays(3),
                ScheduledTime = new TimeSpan(10, 0, 0),
                Mode = "Virtual - Google Meet",
                HumanResourceId = 2
            },
            new Interview
            {
                InterviewId = 2,
                ApplicationId = 3,
                ScheduledDate = DateTime.UtcNow.AddDays(-2),
                ScheduledTime = new TimeSpan(14, 30, 0),
                Mode = "On-site",
                HumanResourceId = 3
            }
        };
        await _context.Interview.AddRangeAsync(interviews);

        await _context.SaveChangesAsync();
    }
}

// Usage in Program.cs:
// using (var scope = app.Services.CreateScope())
// {
//     var context = scope.ServiceProvider.GetRequiredService<YourDbContext>();
//     var seeder = new DatabaseSeeder(context);
//     await seeder.SeedAsync();
// }