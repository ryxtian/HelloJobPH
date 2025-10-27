using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.Model
{
    public class EducationalAttainment
    {
        [Key]
        public int EducationId { get; set; }
        public string SchoolName { get; set; }
        public string? Degree { get; set; }
        public int? YearStarted { get; set; }
        public int? YearEnded { get; set; }
        public string? Level { get; set; } // e.g., "High School", "College", "Master’s"
        public bool? IsGraduated { get; set; }
        public int? ApplicantId { get; set; }
        public Applicant? Applicant { get; set; }

    }
}
