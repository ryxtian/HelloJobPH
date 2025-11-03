using HelloJobPH.Shared.Enums;
using System;
using System.Collections.Generic;


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


}
