using HelloJobPH.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.DTOs
{
    public class OverviewDtos
    {
        public int ApplicantId { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Location { get; set; }



        public string? JobTitle { get; set; }
        public string? JobRequirement { get; set; }
        public string? LocationOfjob { get; set; }
        public decimal? SalaryFrom { get; set; }
        public decimal? SalaryTo { get; set; }
        public EmploymentType EmployementType { get; set; }
        public string? JobPostedDate { get; set; }
        public string? JobDescription { get; set; }

        public List<WorkExperienceDtos>? WorkExperiences { get; set; } = new();
        public List<EducationalAttainmentDtos>? EducationalAttainment { get; set; } = new();

    }
    public class WorkExperienceDtos
    {
        public string? CompanyName { get; set; }
        public string? PositionTitle { get; set; }
        public string? Department { get; set; }
        public int? StartDate { get; set; }
        public int? EndDate { get; set; }
        public bool IsPresent { get; set; }
        public string? Responsibilities { get; set; }
        public string? CompanyAddress { get; set; }
    }
    public class EducationalAttainmentDtos
    {
        public string SchoolName { get; set; }
        public string? Degree { get; set; }
        public int? YearStarted { get; set; }
        public int? YearEnded { get; set; }
        public string? Level { get; set; } // e.g., "High School", "College", "Master’s"
        public bool? IsGraduated { get; set; }
    }
}
