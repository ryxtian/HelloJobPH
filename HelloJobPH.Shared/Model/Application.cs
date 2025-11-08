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
        public int? ResumeId { get; set; }
        public DateTime DateApply { get; set; }
        public ApplicationStatus ApplicationStatus { get; set; }
        public int? JobPostingId { get; set; }
        public byte IsDeleted { get; set; } = 0;
        public int? HumanResourcesId { get; set; }//xxxx
        public HumanResources? HumanResources { get; set; }//xxx
        public JobPosting? JobPosting { get; set; }
        public Applicant? Applicant { get; set; }
        public Interview? Interview { get; set; }
        public Employer? Employer { get; set; }
        public int ApplicantId { get; set; }
        public int? EmployerId { get; set; }
        public string CoverLetter { get; set; } = string.Empty;
        public List<AuditLog>? AuditLogs { get; set; }
    }
}
