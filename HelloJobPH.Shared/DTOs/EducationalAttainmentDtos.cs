using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.DTOs
{
    public class EducationalAttainmentDtos
    {
        public string SchoolName { get; set; }
        public string? Degree { get; set; }
        public DateTime? YearStarted { get; set; }
        public DateTime? YearEnded { get; set; }
        public string? Level { get; set; } // e.g., "High School", "College", "Master’s"
        public bool? IsGraduated { get; set; }
    }
}
