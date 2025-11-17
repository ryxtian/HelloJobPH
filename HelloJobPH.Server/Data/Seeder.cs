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
        // Check if data already exists
        if (await _context.UserAccount.AnyAsync()) return;

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword("password123");

        // ===== 1. USER ACCOUNTS =====
        var users = new List<UserAccount>
        {
            // Applicants (IDs 1-5)
            new UserAccount { Email = "john.doe@email.com", Password = hashedPassword, Role = "Applicant" },
            new UserAccount { Email = "jane.smith@email.com", Password = hashedPassword, Role = "Applicant" },
            new UserAccount { Email = "mike.wilson@email.com", Password = hashedPassword, Role = "Applicant" },
            new UserAccount { Email = "sarah.jones@email.com", Password = hashedPassword, Role = "Applicant" },
            new UserAccount { Email = "david.brown@email.com", Password = hashedPassword, Role = "Applicant" },
            
            // HR Personnel (IDs 6-10)
            new UserAccount { Email = "hr@techcorp.com", Password = hashedPassword, Role = "HR" },
            new UserAccount { Email = "hr@globalinc.com", Password = hashedPassword, Role = "HR" },
            new UserAccount { Email = "hr@innovatesoft.com", Password = hashedPassword, Role = "HR" },
            new UserAccount { Email = "hr@marketpro.com", Password = hashedPassword, Role = "HR" },
            new UserAccount { Email = "hr@healthplus.com", Password = hashedPassword, Role = "HR" },
            
            // Employers (IDs 11-15)
            new UserAccount { Email = "admin@techcorp.com", Password = hashedPassword, Role = "Employer" },
            new UserAccount { Email = "admin@globalinc.com", Password = hashedPassword, Role = "Employer" },
            new UserAccount { Email = "admin@innovatesoft.com", Password = hashedPassword, Role = "Employer" },
            new UserAccount { Email = "admin@marketpro.com", Password = hashedPassword, Role = "Employer" },
            new UserAccount { Email = "admin@healthplus.com", Password = hashedPassword, Role = "Employer" }
        };

        await _context.UserAccount.AddRangeAsync(users);
        await _context.SaveChangesAsync();

        // ===== 2. EMPLOYERS =====
        var employers = new List<Employer>
        {
            new Employer
            {
                CompanyName = "TechCorp Solutions",
                Industry = "Information Technology",
                Description = "Leading software development company specializing in enterprise solutions",
                CompanyAddress = "123 Tech Street",
                City = "Makati",
                Province = "Metro Manila",
                ZipCode = "1200",
                ContactEmail = "contact@techcorp.com",
                ContactNumber = "02-1234-5678",
                Website = "https://www.techcorp.com",
                UserAccountId = users[10].UserAccountId,
                Status = "Approved",
                DateRegistered = DateTime.UtcNow.AddMonths(-6),
                IsVerified = true,
                IsActive = true,
                IsDeleted = 0
            },
            new Employer
            {
                CompanyName = "Global Inc.",
                Industry = "Business Process Outsourcing",
                Description = "International BPO company with offices worldwide",
                CompanyAddress = "456 Business Ave",
                City = "Quezon City",
                Province = "Metro Manila",
                ZipCode = "1100",
                ContactEmail = "contact@globalinc.com",
                ContactNumber = "02-9876-5432",
                Website = "https://www.globalinc.com",
                UserAccountId = users[11].UserAccountId,
                Status = "Approved",
                DateRegistered = DateTime.UtcNow.AddMonths(-5),
                IsVerified = true,
                IsActive = true,
                IsDeleted = 0
            },
            new Employer
            {
                CompanyName = "Innovate Software Inc.",
                Industry = "Software Development",
                Description = "Innovative mobile and web application development company",
                CompanyAddress = "789 Innovation Drive",
                City = "Taguig",
                Province = "Metro Manila",
                ZipCode = "1630",
                ContactEmail = "contact@innovatesoft.com",
                ContactNumber = "02-5555-1234",
                Website = "https://www.innovatesoft.com",
                UserAccountId = users[12].UserAccountId,
                Status = "Approved",
                DateRegistered = DateTime.UtcNow.AddMonths(-4),
                IsVerified = true,
                IsActive = true,
                IsDeleted = 0
            },
            new Employer
            {
                CompanyName = "MarketPro Agency",
                Industry = "Marketing and Advertising",
                Description = "Full-service digital marketing agency",
                CompanyAddress = "321 Marketing Plaza",
                City = "Pasig",
                Province = "Metro Manila",
                ZipCode = "1600",
                ContactEmail = "contact@marketpro.com",
                ContactNumber = "02-7777-8888",
                Website = "https://www.marketpro.com",
                UserAccountId = users[13].UserAccountId,
                Status = "Approved",
                DateRegistered = DateTime.UtcNow.AddMonths(-3),
                IsVerified = true,
                IsActive = true,
                IsDeleted = 0
            },
            new Employer
            {
                CompanyName = "HealthPlus Medical Center",
                Industry = "Healthcare",
                Description = "Modern medical facility providing comprehensive healthcare services",
                CompanyAddress = "555 Health Boulevard",
                City = "Manila",
                Province = "Metro Manila",
                ZipCode = "1000",
                ContactEmail = "contact@healthplus.com",
                ContactNumber = "02-3333-4444",
                Website = "https://www.healthplus.com",
                UserAccountId = users[14].UserAccountId,
                Status = "Approved",
                DateRegistered = DateTime.UtcNow.AddMonths(-2),
                IsVerified = true,
                IsActive = true,
                IsDeleted = 0
            }
        };

        await _context.Employer.AddRangeAsync(employers);
        await _context.SaveChangesAsync();

        // ===== 3. APPLICANTS =====
        var applicants = new List<Applicant>
        {
            new Applicant
            {
                Firstname = "John",
                Middlename = "Michael",
                Surname = "Doe",
                Address = "123 Main Street, Quezon City",
                Phone = "0917-111-2222",
                Birthday = new DateTime(1995, 5, 15),
                UserAccountId = users[0].UserAccountId
            },
            new Applicant
            {
                Firstname = "Jane",
                Middlename = "Marie",
                Surname = "Smith",
                Address = "456 Oak Avenue, Makati City",
                Phone = "0918-222-3333",
                Birthday = new DateTime(1998, 8, 20),
                UserAccountId = users[1].UserAccountId
            },
            new Applicant
            {
                Firstname = "Mike",
                Middlename = "James",
                Surname = "Wilson",
                Address = "789 Pine Road, Pasig City",
                Phone = "0919-333-4444",
                Birthday = new DateTime(1993, 3, 10),
                UserAccountId = users[2].UserAccountId
            },
            new Applicant
            {
                Firstname = "Sarah",
                Middlename = "Lee",
                Surname = "Jones",
                Address = "321 Elm Street, Taguig City",
                Phone = "0920-444-5555",
                Birthday = new DateTime(1996, 11, 25),
                UserAccountId = users[3].UserAccountId
            },
            new Applicant
            {
                Firstname = "David",
                Middlename = "Paul",
                Surname = "Brown",
                Address = "654 Maple Drive, Manila",
                Phone = "0921-555-6666",
                Birthday = new DateTime(1994, 7, 30),
                UserAccountId = users[4].UserAccountId
            }
        };

        await _context.Applicant.AddRangeAsync(applicants);
        await _context.SaveChangesAsync();

        // ===== 4. HUMAN RESOURCES =====
        var humanResources = new List<HumanResources>
        {
            new HumanResources
            {
                Firstname = "Maria",
                Lastname = "Santos",
                PhoneNumber = "0917-123-4567",
                JobTitle = "HR Manager",
                UserAccountId = users[5].UserAccountId,
                EmployerId = employers[0].EmployerId,
                IsDeleted = 0
            },
            new HumanResources
            {
                Firstname = "Carlos",
                Lastname = "Reyes",
                PhoneNumber = "0918-234-5678",
                JobTitle = "Recruitment Specialist",
                UserAccountId = users[6].UserAccountId,
                EmployerId = employers[1].EmployerId,
                IsDeleted = 0
            },
            new HumanResources
            {
                Firstname = "Ana",
                Lastname = "Cruz",
                PhoneNumber = "0919-345-6789",
                JobTitle = "Talent Acquisition Lead",
                UserAccountId = users[7].UserAccountId,
                EmployerId = employers[2].EmployerId,
                IsDeleted = 0
            },
            new HumanResources
            {
                Firstname = "Roberto",
                Lastname = "Garcia",
                PhoneNumber = "0920-456-7890",
                JobTitle = "HR Director",
                UserAccountId = users[8].UserAccountId,
                EmployerId = employers[3].EmployerId,
                IsDeleted = 0
            },
            new HumanResources
            {
                Firstname = "Elena",
                Lastname = "Mendoza",
                PhoneNumber = "0921-567-8901",
                JobTitle = "HR Coordinator",
                UserAccountId = users[9].UserAccountId,
                EmployerId = employers[4].EmployerId,
                IsDeleted = 0
            }
        };

        await _context.HumanResource.AddRangeAsync(humanResources);
        await _context.SaveChangesAsync();

        // ===== 5. EDUCATIONAL ATTAINMENTS =====
        var educationalAttainments = new List<EducationalAttainment>
        {
            // John Doe's Education
            new EducationalAttainment
            {
                SchoolName = "University of the Philippines",
                Degree = "Bachelor of Science in Computer Science",
                YearStarted = new DateTime(2013, 6, 1),
                YearEnded = new DateTime(2017, 4, 30),
                Level = "College",
                IsGraduated = true,
                ApplicantId = applicants[0].ApplicantId,
                IsDeleted = 0
            },
            new EducationalAttainment
            {
                SchoolName = "Manila Science High School",
                Degree = null,
                YearStarted = new DateTime(2009, 6, 1),
                YearEnded = new DateTime(2013, 3, 31),
                Level = "High School",
                IsGraduated = true,
                ApplicantId = applicants[0].ApplicantId,
                IsDeleted = 0
            },
            // Jane Smith's Education
            new EducationalAttainment
            {
                SchoolName = "De La Salle University",
                Degree = "Bachelor of Science in Business Administration",
                YearStarted = new DateTime(2016, 6, 1),
                YearEnded = new DateTime(2020, 4, 30),
                Level = "College",
                IsGraduated = true,
                ApplicantId = applicants[1].ApplicantId,
                IsDeleted = 0
            },
            // Mike Wilson's Education
            new EducationalAttainment
            {
                SchoolName = "Ateneo de Manila University",
                Degree = "Bachelor of Arts in Communication",
                YearStarted = new DateTime(2011, 6, 1),
                YearEnded = new DateTime(2015, 4, 30),
                Level = "College",
                IsGraduated = true,
                ApplicantId = applicants[2].ApplicantId,
                IsDeleted = 0
            },
            new EducationalAttainment
            {
                SchoolName = "Ateneo de Manila University",
                Degree = "Master of Business Administration",
                YearStarted = new DateTime(2016, 6, 1),
                YearEnded = new DateTime(2018, 4, 30),
                Level = "Master's",
                IsGraduated = true,
                ApplicantId = applicants[2].ApplicantId,
                IsDeleted = 0
            },
            // Sarah Jones's Education
            new EducationalAttainment
            {
                SchoolName = "Polytechnic University of the Philippines",
                Degree = "Bachelor of Science in Information Technology",
                YearStarted = new DateTime(2014, 6, 1),
                YearEnded = new DateTime(2018, 4, 30),
                Level = "College",
                IsGraduated = true,
                ApplicantId = applicants[3].ApplicantId,
                IsDeleted = 0
            },
            // David Brown's Education
            new EducationalAttainment
            {
                SchoolName = "Far Eastern University",
                Degree = "Bachelor of Science in Nursing",
                YearStarted = new DateTime(2012, 6, 1),
                YearEnded = new DateTime(2016, 4, 30),
                Level = "College",
                IsGraduated = true,
                ApplicantId = applicants[4].ApplicantId,
                IsDeleted = 0
            }
        };

        await _context.EducationalAttainment.AddRangeAsync(educationalAttainments);
        await _context.SaveChangesAsync();

        // ===== 6. WORK EXPERIENCES =====
        var workExperiences = new List<WorkExperience>
        {
            // John Doe's Work Experience
            new WorkExperience
            {
                CompanyName = "Tech Solutions Inc.",
                PositionTitle = "Junior Software Developer",
                Department = "Development",
                StartDate = new DateTime(2017, 6, 1),
                EndDate = new DateTime(2019, 12, 31),
                IsPresent = false,
                Responsibilities = "Developed web applications using React and Node.js, participated in code reviews, and collaborated with team members on various projects.",
                CompanyAddress = "BGC, Taguig City",
                ApplicantId = applicants[0].ApplicantId,
                IsDeleted = 0
            },
            new WorkExperience
            {
                CompanyName = "Digital Innovations Corp.",
                PositionTitle = "Software Developer",
                Department = "Engineering",
                StartDate = new DateTime(2020, 1, 15),
                EndDate = null,
                IsPresent = true,
                Responsibilities = "Lead developer for mobile applications, mentored junior developers, and implemented CI/CD pipelines.",
                CompanyAddress = "Makati City",
                ApplicantId = applicants[0].ApplicantId,
                IsDeleted = 0
            },
            // Jane Smith's Work Experience
            new WorkExperience
            {
                CompanyName = "Business Consultants Ltd.",
                PositionTitle = "Business Analyst",
                Department = "Operations",
                StartDate = new DateTime(2020, 7, 1),
                EndDate = null,
                IsPresent = true,
                Responsibilities = "Analyzed business processes, created detailed reports, and provided recommendations for operational improvements.",
                CompanyAddress = "Ortigas, Pasig City",
                ApplicantId = applicants[1].ApplicantId,
                IsDeleted = 0
            },
            // Mike Wilson's Work Experience
            new WorkExperience
            {
                CompanyName = "Creative Marketing Agency",
                PositionTitle = "Marketing Coordinator",
                Department = "Marketing",
                StartDate = new DateTime(2015, 8, 1),
                EndDate = new DateTime(2018, 6, 30),
                IsPresent = false,
                Responsibilities = "Coordinated marketing campaigns, managed social media accounts, and analyzed campaign performance metrics.",
                CompanyAddress = "Quezon City",
                ApplicantId = applicants[2].ApplicantId,
                IsDeleted = 0
            },
            new WorkExperience
            {
                CompanyName = "Global Brands Inc.",
                PositionTitle = "Senior Marketing Manager",
                Department = "Marketing",
                StartDate = new DateTime(2018, 7, 15),
                EndDate = null,
                IsPresent = true,
                Responsibilities = "Managed marketing team, developed brand strategies, and oversaw multi-million peso advertising campaigns.",
                CompanyAddress = "Makati City",
                ApplicantId = applicants[2].ApplicantId,
                IsDeleted = 0
            },
            // Sarah Jones's Work Experience
            new WorkExperience
            {
                CompanyName = "IT Support Services",
                PositionTitle = "Technical Support Specialist",
                Department = "IT Support",
                StartDate = new DateTime(2018, 6, 1),
                EndDate = new DateTime(2021, 12, 31),
                IsPresent = false,
                Responsibilities = "Provided technical support to clients, troubleshot software and hardware issues, and maintained IT documentation.",
                CompanyAddress = "Manila",
                ApplicantId = applicants[3].ApplicantId,
                IsDeleted = 0
            },
            // David Brown's Work Experience
            new WorkExperience
            {
                CompanyName = "City General Hospital",
                PositionTitle = "Staff Nurse",
                Department = "Emergency Room",
                StartDate = new DateTime(2016, 7, 1),
                EndDate = null,
                IsPresent = true,
                Responsibilities = "Provided direct patient care, administered medications, and assisted physicians during emergency procedures.",
                CompanyAddress = "Manila",
                ApplicantId = applicants[4].ApplicantId,
                IsDeleted = 0
            }
        };

        await _context.WorkExperience.AddRangeAsync(workExperiences);
        await _context.SaveChangesAsync();

        // ===== 7. JOB POSTINGS =====
        var jobPostings = new List<JobPosting>
        {
            new JobPosting
            {
                Title = "Senior Software Developer",
                Description = "We are looking for an experienced software developer to join our team. Must have 3+ years of experience in web development.",
                Location = "Makati City",
                EmploymentType = EmploymentType.FullTime,
                SalaryFrom = 60000,
                SalaryTo = 90000,
                JobCategory = JobCategory.InformationAndCommunicationTechnology,
                JobRequirements = "Bachelor's degree in Computer Science, 3+ years experience in React/Node.js, strong problem-solving skills",
                HumanResourceId = humanResources[0].HumanResourceId,
                EmployerId = employers[0].EmployerId,
                PostedDate = DateTime.UtcNow.AddDays(-30),
                IsDeleted = 0,
                IsActive = 1
            },
            new JobPosting
            {
                Title = "Customer Service Representative",
                Description = "Join our growing BPO team as a customer service representative. Training provided.",
                Location = "Quezon City",
                EmploymentType = EmploymentType.FullTime,
                SalaryFrom = 25000,
                SalaryTo = 35000,
                JobCategory = JobCategory.CustomerServiceAndSupport,
                JobRequirements = "College graduate, excellent communication skills, willing to work on shifting schedules",
                HumanResourceId = humanResources[1].HumanResourceId,
                EmployerId = employers[1].EmployerId,
                PostedDate = DateTime.UtcNow.AddDays(-25),
                IsDeleted = 0,
                IsActive = 1
            },
            new JobPosting
            {
                Title = "Mobile App Developer",
                Description = "Seeking talented mobile app developer with experience in iOS and Android development.",
                Location = "Taguig City",
                EmploymentType = EmploymentType.FullTime,
                SalaryFrom = 50000,
                SalaryTo = 80000,
                JobCategory = JobCategory.InformationAndCommunicationTechnology,
                JobRequirements = "Experience with Flutter or React Native, portfolio of published apps, 2+ years experience",
                HumanResourceId = humanResources[2].HumanResourceId,
                EmployerId = employers[2].EmployerId,
                PostedDate = DateTime.UtcNow.AddDays(-20),
                IsDeleted = 0,
                IsActive = 1
            },
            new JobPosting
            {
                Title = "Digital Marketing Specialist",
                Description = "Dynamic marketing agency seeks creative digital marketing specialist to manage client campaigns.",
                Location = "Pasig City",
                EmploymentType = EmploymentType.FullTime,
                SalaryFrom = 35000,
                SalaryTo = 55000,
                JobCategory = JobCategory.MarketingAndCommunications,
                JobRequirements = "Bachelor's degree in Marketing or related field, experience with SEO/SEM, social media management skills",
                HumanResourceId = humanResources[3].HumanResourceId,
                EmployerId = employers[3].EmployerId,
                PostedDate = DateTime.UtcNow.AddDays(-15),
                IsDeleted = 0,
                IsActive = 1
            },
            new JobPosting
            {
                Title = "Registered Nurse",
                Description = "Modern medical center seeks compassionate registered nurses for various departments.",
                Location = "Manila",
                EmploymentType = EmploymentType.FullTime,
                SalaryFrom = 30000,
                SalaryTo = 45000,
                JobCategory = JobCategory.HealthcareAndMedical,
                JobRequirements = "Valid PRC license, BSN graduate, at least 1 year hospital experience preferred",
                HumanResourceId = humanResources[4].HumanResourceId,
                EmployerId = employers[4].EmployerId,
                PostedDate = DateTime.UtcNow.AddDays(-10),
                IsDeleted = 0,
                IsActive = 1
            }
        };

        await _context.JobPosting.AddRangeAsync(jobPostings);
        await _context.SaveChangesAsync();

        // ===== 8. RESUMES =====
        var resumes = new List<Resume>
        {
            new Resume
            {
                ResumeFileName = "JohnDoe_Resume.pdf",
                ResumeFileData = new byte[] { 0x25, 0x50, 0x44, 0x46 }, // Dummy PDF bytes
                ApplicantId = applicants[0].ApplicantId,
                IsDeleted = 0
            },
            new Resume
            {
                ResumeFileName = "JaneSmith_Resume.pdf",
                ResumeFileData = new byte[] { 0x25, 0x50, 0x44, 0x46 },
                ApplicantId = applicants[1].ApplicantId,
                IsDeleted = 0
            },
            new Resume
            {
                ResumeFileName = "MikeWilson_Resume.pdf",
                ResumeFileData = new byte[] { 0x25, 0x50, 0x44, 0x46 },
                ApplicantId = applicants[2].ApplicantId,
                IsDeleted = 0
            },
            new Resume
            {
                ResumeFileName = "SarahJones_Resume.pdf",
                ResumeFileData = new byte[] { 0x25, 0x50, 0x44, 0x46 },
                ApplicantId = applicants[3].ApplicantId,
                IsDeleted = 0
            },
            new Resume
            {
                ResumeFileName = "DavidBrown_Resume.pdf",
                ResumeFileData = new byte[] { 0x25, 0x50, 0x44, 0x46 },
                ApplicantId = applicants[4].ApplicantId,
                IsDeleted = 0
            }
        };

        await _context.Resume.AddRangeAsync(resumes);
        await _context.SaveChangesAsync();

        // ===== 9. APPLICATIONS =====
        var applications = new List<Application>
        {
            new Application
            {
                ResumeId = resumes[0].ResumeId,
                DateApply = DateTime.UtcNow.AddDays(-28),
                ApplicationStatus = ApplicationStatus.Viewed,
                JobPostingId = jobPostings[0].JobPostingId,
                ApplicantId = applicants[0].ApplicantId,
                EmployerId = employers[0].EmployerId,
                HumanResourcesId = humanResources[0].HumanResourceId,
                CoverLetter = "I am excited to apply for the Senior Software Developer position. With over 3 years of experience in web development and a proven track record of delivering high-quality solutions, I am confident I can contribute to your team.",
                IsDeleted = 0,
                MarkAsCompleted = 0
            },
            new Application
            {
                ResumeId = resumes[1].ResumeId,
                DateApply = DateTime.UtcNow.AddDays(-23),
                ApplicationStatus = ApplicationStatus.Pending,
                JobPostingId = jobPostings[1].JobPostingId,
                ApplicantId = applicants[1].ApplicantId,
                EmployerId = employers[1].EmployerId,
                HumanResourcesId = humanResources[1].HumanResourceId,
                CoverLetter = "I am writing to express my strong interest in the Customer Service Representative position. My excellent communication skills and customer-focused approach make me an ideal candidate.",
                IsDeleted = 0,
                MarkAsCompleted = 0
            },
            new Application
            {
                ResumeId = resumes[2].ResumeId,
                DateApply = DateTime.UtcNow.AddDays(-18),
                ApplicationStatus = ApplicationStatus.Initial,
                JobPostingId = jobPostings[3].JobPostingId,
                ApplicantId = applicants[2].ApplicantId,
                EmployerId = employers[3].EmployerId,
                HumanResourcesId = humanResources[3].HumanResourceId,
                CoverLetter = "With my extensive marketing experience and proven success in managing campaigns, I am confident I can bring value to your digital marketing team.",
                IsDeleted = 0,
                MarkAsCompleted = 0
            },
            new Application
            {
                ResumeId = resumes[3].ResumeId,
                DateApply = DateTime.UtcNow.AddDays(-17),
                ApplicationStatus = ApplicationStatus.Pending,
                JobPostingId = jobPostings[2].JobPostingId,
                ApplicantId = applicants[3].ApplicantId,
                EmployerId = employers[2].EmployerId,
                HumanResourcesId = humanResources[2].HumanResourceId,
                CoverLetter = "I am very interested in the Mobile App Developer position. My technical skills and passion for creating user-friendly applications align well with your requirements.",
                IsDeleted = 0,
                MarkAsCompleted = 0
            },
            new Application
            {
                ResumeId = resumes[4].ResumeId,
                DateApply = DateTime.UtcNow.AddDays(-8),
                ApplicationStatus = ApplicationStatus.Accepted,
                JobPostingId = jobPostings[4].JobPostingId,
                ApplicantId = applicants[4].ApplicantId,
                EmployerId = employers[4].EmployerId,
                HumanResourcesId = humanResources[4].HumanResourceId,
                CoverLetter = "As a dedicated registered nurse with emergency room experience, I am eager to join your medical center and provide excellent patient care.",
                IsDeleted = 0,
                MarkAsCompleted = 0
            }
        };

        await _context.Application.AddRangeAsync(applications);
        await _context.SaveChangesAsync();

        // ===== 10. INTERVIEWERS =====
        var interviewers = new List<Interviewer>
        {
            new Interviewer { Name = "Dr. Patricia Lopez" },
            new Interviewer { Name = "Mr. Ramon Diaz" },
            new Interviewer { Name = "Ms. Linda Tan" },
            new Interviewer { Name = "Mr. Jose Fernandez" },
            new Interviewer { Name = "Dr. Maria Gonzales" }
        };

        await _context.Interviewer.AddRangeAsync(interviewers);
        await _context.SaveChangesAsync();

        // ===== 11. INTERVIEWS =====
        var interviews = new List<Interview>
        {
            new Interview
            {
                ScheduledDate = DateTime.UtcNow.AddDays(5),
                ScheduledTime = new TimeSpan(10, 0, 0),
                Mode = "Virtual",
                ApplicationId = applications[2].ApplicationId,
                HumanResourceId = humanResources[3].HumanResourceId,
                InterviewerId = interviewers[3].InterviewerId
            },
            new Interview
            {
                ScheduledDate = DateTime.UtcNow.AddDays(3),
                ScheduledTime = new TimeSpan(14, 0, 0),
                Mode = "On-site",
                ApplicationId = applications[4].ApplicationId,
                HumanResourceId = humanResources[4].HumanResourceId,
                InterviewerId = interviewers[4].InterviewerId
            }
        };

        await _context.Interview.AddRangeAsync(interviews);
        await _context.SaveChangesAsync();

        // ===== 12. INTERVIEW HISTORY =====
        var interviewHistories = new List<InterviewHistory>
        {
            new InterviewHistory
            {
                ApplicationId = applications[0].ApplicationId,
                CandidateName = "John Michael Doe",
                Stage = "Initial Screening",
                Status = "Completed",
                InterviewerId = interviewers[0].InterviewerId,
                ScheduledDate = DateTime.UtcNow.AddDays(-10),
                CreatedAt = DateTime.UtcNow.AddDays(-10)
            },
            new InterviewHistory
            {
                ApplicationId = applications[0].ApplicationId,
                CandidateName = "John Michael Doe",
                Stage = "Technical Interview",
                Status = "Scheduled",
                InterviewerId = interviewers[0].InterviewerId,
                ScheduledDate = DateTime.UtcNow.AddDays(-10),
                CreatedAt = DateTime.UtcNow.AddDays(-10)
                        },
            new InterviewHistory
            {
                ApplicationId = applications[1].ApplicationId,
                CandidateName = "Jane Marie Smith",
                Stage = "Initial Screening",
                Status = "Scheduled",
                InterviewerId = interviewers[1].InterviewerId,
                ScheduledDate = DateTime.UtcNow.AddDays(-7),
                CreatedAt = DateTime.UtcNow.AddDays(-7)
            },
            new InterviewHistory
            {
                ApplicationId = applications[2].ApplicationId,
                CandidateName = "Mike James Wilson",
                Stage = "Initial Screening",
                Status = "Completed",
                InterviewerId = interviewers[2].InterviewerId,
                ScheduledDate = DateTime.UtcNow.AddDays(-5),
                CreatedAt = DateTime.UtcNow.AddDays(-5)
            },
            new InterviewHistory
            {
                ApplicationId = applications[2].ApplicationId,
                CandidateName = "Mike James Wilson",
                Stage = "HR Interview",
                Status = "Completed",
                InterviewerId = interviewers[3].InterviewerId,
                ScheduledDate = DateTime.UtcNow.AddDays(-3),
                CreatedAt = DateTime.UtcNow.AddDays(-3)
            }
        };

        await _context.InterviewHistory.AddRangeAsync(interviewHistories);
        await _context.SaveChangesAsync();

        // ===== 13. AUDIT LOGS =====
        var auditLogs = new List<AuditLog>
        {
            new AuditLog
            {
                Action = "Applied",
                Details = "Application submitted successfully for Senior Software Developer",
                Timestamp = DateTime.UtcNow.AddDays(-28),
                ApplicationId = applications[0].ApplicationId,
                ApplicantId = applicants[0].ApplicantId,
                JobPostingId = jobPostings[0].JobPostingId,
                HumanResourcesId = humanResources[0].HumanResourceId
            },
            new AuditLog
            {
                Action = "Shortlisted",
                Details = "Applicant shortlisted for interview",
                Timestamp = DateTime.UtcNow.AddDays(-20),
                ApplicationId = applications[1].ApplicationId,
                ApplicantId = applicants[1].ApplicantId,
                JobPostingId = jobPostings[1].JobPostingId,
                HumanResourcesId = humanResources[1].HumanResourceId
            },
            new AuditLog
            {
                Action = "Interview Scheduled",
                Details = "Interview scheduled for Digital Marketing Specialist",
                Timestamp = DateTime.UtcNow.AddDays(-15),
                ApplicationId = applications[2].ApplicationId,
                ApplicantId = applicants[2].ApplicantId,
                JobPostingId = jobPostings[3].JobPostingId,
                HumanResourcesId = humanResources[3].HumanResourceId
            },
            new AuditLog
            {
                Action = "Applied",
                Details = "Application submitted for Mobile App Developer",
                Timestamp = DateTime.UtcNow.AddDays(-17),
                ApplicationId = applications[3].ApplicationId,
                ApplicantId = applicants[3].ApplicantId,
                JobPostingId = jobPostings[2].JobPostingId,
                HumanResourcesId = humanResources[2].HumanResourceId
            },
            new AuditLog
            {
                Action = "Accepted",
                Details = "Application accepted - offer extended",
                Timestamp = DateTime.UtcNow.AddDays(-2),
                ApplicationId = applications[4].ApplicationId,
                ApplicantId = applicants[4].ApplicantId,
                JobPostingId = jobPostings[4].JobPostingId,
                HumanResourcesId = humanResources[4].HumanResourceId
            }
        };

        await _context.AuditLog.AddRangeAsync(auditLogs);
        await _context.SaveChangesAsync();
    }
}


