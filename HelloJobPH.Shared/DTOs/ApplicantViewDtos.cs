using HelloJobPH.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.DTOs
{
    public class ApplicantViewDtos
    {
        public int ApplicantId { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Location { get; set; }



        public List<WorkExperienceDtos>? WorkExperiences { get; set; } = new();
        public List<EducationalAttainmentDtos>? EducationalAttainment { get; set; } = new();
    }
}
