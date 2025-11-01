using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.Model
{
    public class WorkExperience
    {
        [Key]
        public int WorkExperienceId { get; set; }
        public string CompanyName { get; set; }
        public string PositionTitle { get; set; }
        public string Department { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsPresent { get; set; }
        public string? Responsibilities { get; set; }
        public string? CompanyAddress { get; set; }
        public int? ApplicantId { get; set; }
        public byte IsDeleted { get; set; } = 0;
        public Applicant? Applicant { get; set; }
    }
}
