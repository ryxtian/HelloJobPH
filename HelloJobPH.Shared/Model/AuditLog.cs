using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloJobPH.Shared.Model
{
    public class AuditLog
    {
        [Key]
        public int AuditLogId { get; set; }

        public string Action { get; set; } = string.Empty;   // e.g. "Applied", "Reviewed", "Approved"
        public string Details { get; set; } = string.Empty;  // e.g. "Application submitted successfully"

        public DateTime Timestamp { get; set; }
        public int ApplicationId { get; set; }
        public Application? Application { get; set; } = null!;


        public int? EmployerId { get; set; }
        public Employer? Employer { get; set; }
        public int? ApplicantId { get; set; }
        public Applicant? Applicant { get; set; }
        public int? JobPostingId { get; set; }
        public JobPosting? JobPosting { get; set; }
        public int? HumanResourcesId { get; set; }
        public HumanResources? HumanResources { get; set; }
    }
}
