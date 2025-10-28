using HelloJobPH.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.Model
{
    public class Application
    {
        [Key]
        public int ApplicationId { get; set; }
        public string ResumeUrl { get; set; } = string.Empty;
        public DateTime DateApply { get; set; }
        public ApplicationStatus ApplicationStatus { get; set; }
        public int? JobPostId { get; set; }
        public byte IsDeleted { get; set; } = 0;
        public JobPosting? JobPosting { get; set; }
        public Applicant? Applicant { get; set; }
        public Interview? Interview { get; set; }
        public int ApplicantId { get; set; }
    }
}
